using System;
using System.Collections.Generic;
using System.Text;

namespace SaveToDB
{
    class Item
    {
        public int PriceValueId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string CatalogEntryCode { get; set; }
        public string MarketId { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string UnitPrice { get; set; }
    }
}
