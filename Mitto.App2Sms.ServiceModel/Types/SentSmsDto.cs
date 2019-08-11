using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.App2Sms.ServiceModel.Types
{
    public class SentSmsDto
    {
        public string DateTime { get; set; }
        public string MCC { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public decimal Price { get; set; }

        public State State { get; set; }

    }
}
