using Ferhah.SyncNetObjects.Test.NetModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.NetSyncConfigs
{
    public class QuotationItemSyncConfig : SyncConfiguration<QuotationItem>
    {
        public QuotationItemSyncConfig()
        {
            Key(x => x.Name);
            KeyNavigationProperty(x => x.Quotation);
        }
    }

 
}
