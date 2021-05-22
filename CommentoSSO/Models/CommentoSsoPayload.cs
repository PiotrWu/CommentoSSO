using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentoSSO.Models
{
    public class CommentoSsoPayload
    {
        public int token { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string photo { get; set; }

    }
}
