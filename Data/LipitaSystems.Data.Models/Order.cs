﻿namespace LipitaSystems.Data.Models
{
    using System.Collections.Generic;

    using LipitaSystems.Data.Common.Models;

    public class Order : BaseDeletableModel<int>
    {
        public Order()
        {
            this.Products = new HashSet<ProductOrder>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; private set; }

        public string PhoneNumber { get; set; }

        public decimal TotalPrice { get; set; }

        public string Address { get; set; }

        public string DeliveryType { get; set; }

        public string DeliveryNotes { get; set; }

        public virtual ICollection<ProductOrder> Products { get; set; }
    }
}
