namespace LipitaSystems.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LipitaSystems.Data.Common.Models;

    public class Order : BaseDeletableModel<int>
    {
        public Order()
        {
            this.Products = new HashSet<ProductOrder>();
        }

        [Required]
        public string FullName { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber { get; set; }

        public decimal TotalPrice { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string DeliveryType { get; set; }

        [MaxLength(500)]
        public string DeliveryNotes { get; set; }

        public int? DeliveryOfficeId { get; set; }

        public virtual DeliveryOffice DeliveryOffice { get; set; }

        public int? DiscountCodeId { get; set; }

        public DiscountCode DiscountCode { get; set; }

        public virtual ICollection<ProductOrder> Products { get; set; }
    }
}
