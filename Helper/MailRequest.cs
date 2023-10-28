using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLib
{
    public class MailRequest
    {
        public string toMailAdress { get; set; }
        public string messageBody { get; set; }
        public string messageSubject { get; set; }
    }
}
