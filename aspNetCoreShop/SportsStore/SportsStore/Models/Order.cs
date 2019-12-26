using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SportsStore.Models
{
    /// <summary>
    /// Model of user's order 
    /// </summary>
    public class Order
    {
        [BindNever] public int OrderID { get; set; }
        
        /// <summary>
        /// BindNever ?
        /// Ordered products
        /// </summary>
        [BindNever] public ICollection<CartLine> Lines { get; set; }
        
        /// <summary>
        /// Administration tool unique element that can be used to mark what Order are on their way to customers
        /// </summary>
        [BindNever] public bool Shipped { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Required attr checks a validity of inputted data in the field of form
        /// </summary>
        [Required(ErrorMessage = "Please enter the first address line")]
        public string AddrLine1 { get; set; }

        public string AddrLine2 { get; set; }
        public string AddrLine3 { get; set; }
        
        [Required(ErrorMessage = "Please enter the city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter the region")]
        public string State { get; set; }

        public string Zip { get; set; }
        [Required(ErrorMessage = "Please enter a country name")]
        public string Country { get; set; }
        public bool GiftWrap { get; set; }

    }
}