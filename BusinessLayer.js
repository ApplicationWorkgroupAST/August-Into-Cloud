i'use strict';

var AWS = require('aws-sdk');
var s3 = new AWS.S3();
var comprehend = new AWS.Comprehend();

//S3 API https://docs.aws.amazon.com/AWSJavaScriptSDK/latest/AWS/S3.html
//Comprehend API https://docs.aws.amazon.com/AWSJavaScriptSDK/latest/AWS/Comprehend.html

function createResponse(statusCode, body, returnType) {
    
    if ( (returnType == null || returnType == undefined) ) {
        returnType = "application/json";
    }
    
    return {
        statusCode: statusCode,
        headers: {
            "Access-Control-Allow-Origin": "*",
            "Content-Type" : returnType
        },
        body: body
    };
}
exports.handler = (event, context, callback) => {

    const s3Bucket = 'burt.ast.sample';
    const getFileStatsMethod = '/getstats';
    const listAllMethod = '/listall';

    
    switch (event.httpMethod) {

        case 'GET':

            if (event.path.indexOf(getFileStatsMethod) >= 0) {

               let fileName = event.path.substring(event.path.indexOf(getFileStatsMethod) + getFileStatsMethod.length + 1);
               let params = { Bucket: s3Bucket, Key: fileName };
               s3.getObject(params, function(err, data) {
                   if (err) {
                     callback(null, createResponse(404, JSON.stringify(err)));
                   }
                   else {

                      //convert the string from a binary array back to a string
                      var fileContents = '';
                      data.Body.forEach(function(x) { fileContents = fileContents + String.fromCharCode(x); });
                      console.log(fileContents);


                      //call comprehend on the file contents (there is a limit to 5000 characters
                      var resultMessage = '';
                      let params = { LanguageCode: 'en' , Text : fileContents };
                      comprehend.detectEntities(params, function(err, data) {

                          resultMessage += JSON.stringify(data);

                          let params = { LanguageCode: 'en' , Text : fileContents };
                          comprehend.detectSentiment(params, function(err, data) {

                              resultMessage += JSON.stringify(data);
                              callback(null, createResponse(200, JSON.stringify(resultMessage)));
                              
                          });
                      });
                    }
                });
              }
              else if (event.path.indexOf(listAllMethod) >= 0) {

                var params = {
                    Bucket: s3Bucket,
                    MaxKeys: 999
                };
                s3.listObjects(params, function(err, data) {
                   if (err) {
                    callback(null, createResponse(502, JSON.stringify(err)));
                     
                   }
                   else {

                    let fileArray = [];
                    if (data.Contents) {
                      data.Contents.forEach(function(fileInformation) {
                         fileArray.push({"fileName" : fileInformation.Key});
                      });
                    }
                 
         
                    callback(null, createResponse(200, JSON.stringify(fileArray)));
                    

                   }
                });
            }
            else {

                //The default GET call just echoes back the request
                callback(null, createResponse(200, JSON.stringify(event)));
                
            }

            break;

         case 'POST' :

            //exercise for reader
            //USE https://www.npmjs.com/package/aws-lambda-multipart-parser
             callback(null, { 'statusCode' : 200, 'body' : event.body.Content});
            break;

         default:
            callback(null, createResponse(200, 'No DICE~!'));
    }


}
