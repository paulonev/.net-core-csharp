namespace SportsStore.Models.ViewModels
{
    /// <summary>
    /// Stuff used in View for the Cart
    /// </summary>
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}