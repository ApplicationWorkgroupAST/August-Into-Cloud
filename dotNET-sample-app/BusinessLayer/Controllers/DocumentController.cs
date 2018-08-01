using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using BusinessLayer.Helpers;
using DataAccessLayer.AccessLayer;
using System.IO;
using System.Text;
using Microsoft.Net.Http.Headers;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessLayer.Controllers
{

    //REST INTERFACE (API)
    [Route("document/")]
    public class DocumentController : Controller
    {

        //Not really part of the REST API (Single Page Application) SPA
        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            /*
            var path = "ok.html";
            var response = new HttpResponseMessage();
            response.Content = "Mine"; //new StringContent(System.IO.File.ReadAllText(path));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
            */
            var k = new StringContent(System.IO.File.ReadAllText("SinglePageApplication.html"));
            var d =  await k.ReadAsByteArrayAsync();
            return new FileContentResult(d, new MediaTypeHeaderValue("text/html"));
        }
        



        //Processes (stores through the DataAccessObject) a file that is sent through
        //the Single Page Application interface
        [HttpPost("create")]
        public async Task<IActionResult> CreateDocument()
         {
            var t = await Request.ReadFormAsync();

            //get file
            var stream = new MemoryStream();

            if (t.Files == null || t.Files.Count < 1) {
                return Ok();
            }

            //async put the file into the stream
            await t.Files[0].CopyToAsync(stream);

            //get the file from the stream 
            var fileContents = Encoding.UTF8.GetString(stream.ToArray());

            //get the file name which was a form field in the SPA
            Microsoft.Extensions.Primitives.StringValues fileName;
            string trueFileName = "";
            if (Request.Form.TryGetValue("fileName", out fileName)) {
                trueFileName = fileName.First<string>();
            }

            //store the file through the dataAccessObject (IN OUR CASE A DICTIONARY)
            dataAccessObject.PutFile(trueFileName, fileContents);
            return Ok();

         }

        //gets a list of all files stored in the DICTIONARY
        [HttpGet("listall")] 
        public IActionResult ListDocuments() {
            var obj = dataAccessObject.GetInfoOnFiles();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return Ok(json);
        }


        //this could be injected or DataAccessObject could be a singleton in the future
        private static IDataAccessObject dataAccessObject = new DataAccessObject();

        //uses a helper to get word count (distribution) in the indicated file
        [HttpGet("getstats/{fileName}")]

        //NOT
        //public async Task<IActionResult> GetDocumentStats(string fileName)
        public IActionResult GetDocumentStats(string fileName)
        {

            //get the indicated file from the DICTIONARY using the DAO      
            var file = dataAccessObject.GetFileAsString(fileName);

            //get the word count with the helper
            FileSpecs fileSpecs = new FileSpecs(fileName);

            //send the data back to the web page (SPA) as an Http response
            var obj = fileSpecs.GetSpecsFromStringFile(file);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            return Ok(json);
        }
    }
}
