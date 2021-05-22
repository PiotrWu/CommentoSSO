namespace CommentoSSO.Models
{
    public class CommentoSsoRequest
    {
        public string Token { get; set; }
        public string HMAC { get; set; }

        public CommentoSsoRequest(string token, string hMAC)
        {
            Token = token;
            HMAC = hMAC;
        }
    }
}
