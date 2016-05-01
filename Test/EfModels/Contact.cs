using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.EfModels
{
    public class Contact
    {

        public int ID { get; set; }
        public String FirstName { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public Company Company { get; set; }
        public int CompanyID { get; set; }

        public CompanyComplexType Condition { get; set; } = new CompanyComplexType();

        public override string ToString()
        {
            return nameof(Contact) + " ["+ Name + "."+  FirstName + "] " + Address;
        }
    }

  
}
