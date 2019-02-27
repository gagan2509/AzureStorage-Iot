namespace AzureBlobStorage.Interface
{
    /// <summary>
    /// Getting Data from Blob Storage
    /// </summary>
    public interface IBlobOperations
    {
        /// <summary>
        /// Get Data from blob using Url
        /// </summary>
        string GetBlobData(string url);
    }
}
