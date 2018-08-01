	'use strict';
	
	var AWS = require('aws-sdk');
	var s3 = new AWS.S3();
	
       	
	         var params = { Bucket: 'burt.ast.sample', MaxKeys: 999 };
	         s3.listObjectsV2(params, function(err, data) {
	           if (err) {
	              console.log('Tallahassee a problem: ' + JSON.stringify(err)); 
	           }
	           else {
	              console.log('Tally information number of file in directory: ' + JSON.stringify(data.KeyCount));
	           }
	           console.log('I am LAST!');
	         });
	
