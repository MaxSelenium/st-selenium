using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSeleniumTest
{
    public class Product
    {
        public string Name { get; internal set; }
        public string Price { get; internal set; }
        public string SalePrice { get; internal set; }
        public Front PriceFront { get; internal set; }
        public Front SalePriceFront { get; internal set; }
    }
    public class Front
    {
        public string Color { get; internal set; }
        public decimal Size { get; internal set; }
        public bool through { get; internal set; }
        public bool bold { get; internal set; }

    }
}
