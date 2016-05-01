using Ferhah.SyncNetObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.NetModels
{
    public class ObjectWithCompositeKey
    {
        public String Key_1 { get; set; }
        public String Key_2 { get; set; }
        public Company Key_3 { get; set; }
    }


    public class Company
    {
        public String Name { get; set; }
        public String Address { get; set; }

        public override string ToString()
        {
            return nameof(Company) + " " + Name + " " + Address;
        }
    }


}
