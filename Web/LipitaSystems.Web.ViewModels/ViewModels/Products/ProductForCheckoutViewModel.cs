namespace LipitaSystems.Web.ViewModels.ViewModels.Products
{
    public class ProductForCheckoutViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal SummedPrice =>
            this.DiscountPercentage != null ?
            (this.Quantity * this.OriginalPrice) - (((decimal)this.DiscountPercentage / 100) * (this.Quantity * this.OriginalPrice)) :
            this.Quantity * this.OriginalPrice;

        public decimal FinalPrice { get; set; }

        public int SecondaryCategoryId { get; set; }

        public string SecondaryCategoryName { get; set; }

        public int MainCategoryId { get; set; }

        public int? DiscountPercentage { get; set; }

        public bool IsDiscounted => this.DiscountPercentage != null;
    }
}
