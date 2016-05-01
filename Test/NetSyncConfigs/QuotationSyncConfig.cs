using Ferhah.SyncNetObjects.Test.NetModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.NetSyncConfigs
{
    public class QuotationSyncConfig : SyncConfiguration<Quotation>
    {
        public QuotationSyncConfig()
        {
            Key(x => x.Name);
            PrimitiveProperty(x => x.Description);
            NavigationProperty(x => x.Company);
            NavigationProperty(x => x.Type);
        }
    }

   
}
