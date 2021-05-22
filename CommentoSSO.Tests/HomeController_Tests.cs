using CommentoSSO.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommentoSSO.Tests
{
    public class HomeController_Tests
    {
        [Fact]
        public void ShouldReturnDecodedStringFromHex()
        {
            var hex = "48656C6C6F20576F726C64";
            var expected = "Hello World";
            var result = HomeController.FromHexString(hex);

            Assert.Equal(expected, result);
        }
    }
}
