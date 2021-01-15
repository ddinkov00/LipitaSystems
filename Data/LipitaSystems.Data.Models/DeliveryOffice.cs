namespace LipitaSystems.Data.Models
{
    using System.Collections.Generic;

    using LipitaSystems.Data.Common.Models;

    public class DeliveryOffice : BaseDeletableModel<int>
    {
        public DeliveryOffice()
        {
            this.Orders = new HashSet<Order>();
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
