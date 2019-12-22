using System;

namespace SportsStore.Models.ViewModels
{
    /// <summary>
    /// Data which will be passed to View in the Action Method of ProductController
    /// Containing static data: totalItems in catalog, ItemsPerPage property,
    /// CurrentPage number
    /// Class of view models. Used by View  "LIST"
    /// </summary>
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages =>
            (int) Math.Ceiling((decimal) TotalItems / ItemsPerPage);
    }
}