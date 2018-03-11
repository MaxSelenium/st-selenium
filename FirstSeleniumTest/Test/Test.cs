using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSeleniumTest.Test
{
    [TestFixture]
    public class Test :TestBase
    {
        string site = "http://localhost/LiteCart2/";

        [Test]
        public void Task19()
        {
            app.Open(site);
            app.AddInCardFirstProduct(3);
            app.RemoveAllProduct();
        }

    }
}
