namespace LipitaSystems.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LipitaSystems.Data.Common.Models;

    public class DeliveryOffice : BaseDeletableModel<int>
    {
        public DeliveryOffice()
        {
            this.Orders = new HashSet<Order>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
