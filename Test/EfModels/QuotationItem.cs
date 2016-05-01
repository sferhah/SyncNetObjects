using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.EfModels
{
    public class QuotationItem
    {
        public int ID { get; set; }        
        public int QuotationID { get; set; }

        public String Name { get; set; }
        public Quotation Quotation { get; set; }

        public override string ToString()
        {
            return nameof(QuotationItem) + " " + Name + " " + Quotation.ToString();
        }
    }
}
