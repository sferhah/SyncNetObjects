using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.NetModels
{
    public class QuotationItem
    {
        public String Name { get; set; }

        public Quotation Quotation { get; set; }        

        public override string ToString()
        {
            return nameof(QuotationItem) + " " + Name + " " + Quotation.ToString();
        }
    }
}
