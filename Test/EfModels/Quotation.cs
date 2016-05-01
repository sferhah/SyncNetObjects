using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.EfModels
{
    public class Quotation
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public int? QuotationTypeID { get; set; }

        public String Name { get; set; }
        public String Description { get; set; }

        public Company Company { get; set; }
        public QuotationType Type { get; set; }

        public override string ToString()
        {
            return nameof(Quotation) + " " + Name;
        }
    }
}
