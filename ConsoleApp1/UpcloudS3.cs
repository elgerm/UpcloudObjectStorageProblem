using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class UpcloudS3
    {
        const string BUCKETNAME = "test";
        const string SERVICE_URL = "https://soichat.sg-sin1.upcloudobjects.com";

        const string ACCESSKEY = "";
        const string ACCESSSECRET = "";

        public async Task GetObject()
        {
            var client = GetClient();

            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = BUCKETNAME,
                Key = "tenor.gif"
            };

            try
            {
                string responseBody = "";
                using (GetObjectResponse response = await client.GetObjectAsync(request))
                using (Stream responseStream = response.ResponseStream)
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string title = response.Metadata["x-amz-meta-title"]; // Assume you have "title" as medata added to the object.
                    string contentType = response.Headers["Content-Type"];
                    Console.WriteLine("Object metadata, Title: {0}", title);
                    Console.WriteLine("Content type: {0}", contentType);

                    responseBody = reader.ReadToEnd(); // Now you process the response body.
                }

                Console.WriteLine(responseBody);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

       public async Task ListBuckets()
        {
            var client = GetClient();

            ListBucketsResponse response = await client.ListBucketsAsync();

            // View response data
            Console.WriteLine("Buckets owner - {0}", response.Owner.DisplayName);
            foreach (S3Bucket bucket in response.Buckets)
            {
                Console.WriteLine("Bucket {0}, Created on {1}", bucket.BucketName, bucket.CreationDate);
            }

        }

        AmazonS3Client GetClient()
        {
            var config = new AmazonS3Config()
            {
                ServiceURL = SERVICE_URL
            };                                                             

            var cred = new BasicAWSCredentials(ACCESSKEY, ACCESSSECRET);

            return new AmazonS3Client(cred, config);
        }
    }

}
