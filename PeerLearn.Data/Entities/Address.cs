using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerLearn.Data.Entities
{
    public class Address
    {
        virtual public string StreetAddress { get; set; }
        virtual public string StreetAddress2 { get; set; }
        virtual public string City { get; set; }
        virtual public string State { get; set; }
        virtual public string PostalCode { get; set; }
    }
}
