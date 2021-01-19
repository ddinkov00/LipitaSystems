namespace LipitaSystems.Web.ViewModels.ViewModels.Orders
{
    public class AdminAreaOrderViewModel
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string TotalPrice { get; set; }

        public string Address { get; set; }

        public string DeliveryType { get; set; }

        public string DeliveryNotes { get; set; }

        public bool IsDeleted { get; set; }

        public string DeletedOn { get; set; }

        public string CreatedOn { get; set; }

        public string ModifiedOn { get; set; }
    }
}
