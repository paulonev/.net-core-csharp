using Microsoft.AspNetCore.Http;

namespace SportsStore.Infrastructure
{
    /// <summary>
    /// This is preparation class for adding button 'Add to Cart' to every product in ProductSummary.cshtml
    /// <see cref="ProductSummary.cshtml"/>
    /// </summary>
    public static class UrlExtensions
    {
        /// <summary>
        /// This extension method generates URL which browser will be using to return
        /// after updating the Cart 
        /// </summary>
        /// <param name="request">Processing new URL</param>
        /// <returns></returns>
        public static string PathAndQuery(this HttpRequest request) =>
            request.QueryString.HasValue
                ? $"{request.Path}{request.QueryString}"
                : request.Path.ToString();
    }
}