using AzureBlobStorage.Interface;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;

namespace BlobOperations
{
    public class BlobOperation : IBlobOperations
    {
        /// <summary>
        /// Getting Data from blob
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetBlobData(string url)
        {
            string rawData = string.Empty;
            var uri = new Uri(url);
            var blob = new CloudBlob(uri);

            using (var memoryStream = new MemoryStream())
            {
                blob.DownloadToStream(memoryStream);
                rawData = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            return rawData;
        }
    }
}
