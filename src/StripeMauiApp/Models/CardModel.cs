using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeMauiApp.Models
{
    public class CardModel
    {
        public string Number { get; set; }
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }
        public string Cvc { get; set; }
        public string Name { get; set; }
        public string AddressCity { get; set; }
        public string AddressZip { get; set; }
        public string Currency { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressCountry { get; set; }

    }
}
