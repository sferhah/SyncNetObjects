using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;

namespace Ferhah.SyncNetObjects.Test.EfModels
{
    
    public class CompanyComplexType
    {
        public String Expression { get; set; }
        public String Toto { get; set; }
    }

    public class Company
    {
        public int ID { get; set; }

        public String Name { get; set; }
        public String Address { get; set; }

        public CompanyComplexType CompanyComplexType { get; set; } = new CompanyComplexType();    

        public override string ToString()
        {
            return nameof(Company) + " " + Name + " " + Address;
        }
    }

   
}
