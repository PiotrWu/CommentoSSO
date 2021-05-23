using CommentoSSO.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommentoSSO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Sso")]
        public IActionResult Sso(string token, string hmac)
        {
            string key = "d743c678e26b725c6c1fb4941a02f8c124583dd72b54cb59c4c39f43826d75ea";
            string expectedHmac = HashHMACHex(key, token);
            if (expectedHmac!=hmac)
            {
                return View("Error", new ErrorViewModel { RequestId = token });
            }
            CommentoSsoRequest commentoSsoRequest = new CommentoSsoRequest(token, hmac);
            CommentoSsoPayload commentoSsoPayload = new CommentoSsoPayload()
            {
                token = token,
                name = "testName",
                email = "email@email.com",
                link = "someLink",
                photo = "url"
            };
            //hmac = hex - encode(hmac - sha256(payload - json, secret - key))
            string payloadHmac = HMAC(key, commentoSsoPayload.ToJson());
            var payloadHex = HexEncode(commentoSsoPayload.ToJson());
            return Redirect("https://commento.io/api/oauth/sso/callback?payload=" + payloadHex + "&hmac=" + payloadHmac);
            //return View(commentoSsoRequest);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static string FromHexString(string hexString)
        {
            byte[] dBytes = StringToByteArray(hexString);
            string utf8result = System.Text.Encoding.ASCII.GetString(dBytes);
            return utf8result;
        }

        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }


        public static string HashHMACHex(string keyHex, string message)
        {
            byte[] hash = HashHMAC(HexDecode(keyHex), HexDecode(message));
            return HashEncode(hash);
        }

        public static string HMAC(string keyHex, string message)
        {
            byte[] hash = HashHMAC(HexDecode(keyHex), Encoding.UTF8.GetBytes(message));
            return HashEncode(hash);
        }

        private static byte[] HexDecode(string hex)
        {
            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = byte.Parse(hex.Substring(i * 2, 2), NumberStyles.HexNumber);
            }
            return bytes;
        }

        private static string HexEncode(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            var hexString = BitConverter.ToString(bytes);
            hexString = hexString.Replace("-", "");
            return hexString;
        }
        private static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        private static byte[] HashHMAC(byte[] key, byte[] message)
        {
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(message);
        }
    }
}
