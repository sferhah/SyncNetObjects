using Ferhah.SyncNetObjects.Test.NetModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.NetSyncConfigs
{
    public class QuotationTypeSyncConfig : SyncConfiguration<QuotationType>
    {
        public QuotationTypeSyncConfig()
        {
            Key(x => x.Name);
        }
    }
}
