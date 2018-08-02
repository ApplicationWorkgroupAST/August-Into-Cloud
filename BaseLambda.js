 
var AWS = require('aws-sdk');
var s3 = new AWS.S3();

exports.handler = (event, context, callback) => {
   
    
    switch (event.httpMethod) {

        case 'GET':
            
            callback(null, { 'statusCode' : 200, 'body' : event.httpMethod + ':::' + event.path});
            break;
            
         case 'POST' :
             
            callback(null, { 'statusCode' : 200, 'body' : event.httpMethod + ':::' + event.path});
            break;
             
         default:
            callback(null, { 'statusCode' : 200, 'body' : 'No DICE! DEFAULT '});
    }
}

