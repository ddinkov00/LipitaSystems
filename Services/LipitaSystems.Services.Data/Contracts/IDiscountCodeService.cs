using System;
using System.Collections.Generic;
using System.Text;

namespace LipitaSystems.Services.Data.Contracts
{
    public interface IDiscountCodeService
    {
        decimal ApplyDiscount(decimal summedPrice, int categoryId, bool isDiscounted, string code);
    }
}
