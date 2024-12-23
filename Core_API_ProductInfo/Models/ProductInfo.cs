using System;
using System.Collections.Generic;

namespace Core_API_ProductInfo.Models;

public partial class ProductInfo
{
    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int UnitPrice { get; set; }

    public int ProductRecordId { get; set; }
}
