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

        [Fact]
        public void ShouldReturnExpectedCalculatedHmac()
        {
            string message = "e815a338336b14cdcf734052310f8aba48faad3e80bb8e723453ac545b800531";
            string expectedHex = "b23cc27b9d5bdffbd9c09bf50be3edfd3d764edcb7885f8a7daec989219d1169";
            string key = "d743c678e26b725c6c1fb4941a02f8c124583dd72b54cb59c4c39f43826d75ea";
            string result = HomeController.HashHMACHex(key, message);

          

            Assert.Equal(expectedHex, result);

        }
    }
}
