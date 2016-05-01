using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.NetModels
{
    public class Quotation
    {
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
