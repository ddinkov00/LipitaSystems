namespace LipitaSystems.Web.ViewModels.ViewModels.Delivery_Addersses
{
    using System;

    public class DeliveryOfficeSelectListViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string NameAddress => $"{this.Name} -> Адрес: {this.Address}";
    }
}
