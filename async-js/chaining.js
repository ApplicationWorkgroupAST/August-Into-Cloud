	'use strict';
	
	var AWS = require('aws-sdk');
	var s3 = new AWS.S3();

        //GET FIRST FILE	
        var bucketName = 'burt.ast.sample';
 	
	         var params = { Bucket: bucketName, MaxKeys: 999 };
	         s3.listObjectsV2(params, function(err, data) {
	           if (err) {
	              console.log('Tallahassee a problem: ' + JSON.stringify(err)); 
	           }
	           else {

                         var fileName  = data.Contents[0].Key;

                         //CHAINING getObject is ASYNC also! 
		         var params = { Bucket: bucketName, Key: fileName };
			 s3.getObject(params, function(err, data) {
			   if (err) {
                              console.log('issue getting file ' + fileName);
			   }
			   else {     
			
			      //convert the string from a binary array back to a string
			      var fileContents = '';
			      data.Body.forEach(function(x) { fileContents = fileContents + String.fromCharCode(x); });
                              console.log('file contents!: '+ fileContents); 
			   }
			  });
	           }
	         });


/*

OUTPUT
	
file contents!: my stuff is here hhhhhhh

*/
