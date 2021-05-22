using CommentoSSO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommentoSSO.Tests
{
    public class CommentoSsoPayload_Tests
    {
        [Fact]
        public void ShouldReturnJson()
        {
            CommentoSsoPayload commentoSsoPayload = new CommentoSsoPayload()
            {
                token = "ABC", name = "test", email = "email@email.com", link = "someLink", photo = "url"
            };
            var actual = commentoSsoPayload.ToJson();
            string expected = "{\"token\":\"ABC\",\"email\":\"email@email.com\",\"name\":\"test\",\"link\":\"someLink\",\"photo\":\"url\"}";

            Assert.Equal(expected, actual);
        }
    }
}
