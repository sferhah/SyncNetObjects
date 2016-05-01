using Ferhah.SyncNetObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.NetModels
{
    public class Contact
    {
        public String FirstName { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public Company Company { get; set; }

        public override string ToString()
        {
            return nameof(Contact) + " ["+ Name + "."+  FirstName + "] " + Address;
        }
    }

  
}
