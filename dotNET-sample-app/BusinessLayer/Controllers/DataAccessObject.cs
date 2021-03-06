﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessLayer.Controllers
{
    [Route("api/[controller]")]
    public class DataAccessObject : Controller
    {
        // GET: api/documents
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        /*
        [HttpPost("getstats/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<HttpResponseMessage> GetDocumentStats(int id
        {
            var p = new HttpResponseMessage();
            p.StatusCode = System.Net.HttpStatusCode.OK;
            return p;
        }
        */
    }
}
