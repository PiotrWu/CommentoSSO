using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentoSSO.Models
{
    public class CommentoSsoPayload
    {
        public string token { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string photo { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
