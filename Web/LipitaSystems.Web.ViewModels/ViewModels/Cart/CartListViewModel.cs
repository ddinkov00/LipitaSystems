namespace LipitaSystems.Web.ViewModels.ViewModels.Cart
{
    using System.Collections.Generic;

    public class CartListViewModel
    {
        public ICollection<CartViewModel> Products { get; set; }

        public bool? IsCodeValid { get; set; }
    }
}
