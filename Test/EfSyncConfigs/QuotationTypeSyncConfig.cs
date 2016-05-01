using Ferhah.SyncNetObjects;
using Ferhah.SyncNetObjects.Test.EfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Ferhah.SyncNetObjects.Test.EfSyncConfigs
{
    public class QuotationTypeSyncConfig : SyncConfiguration<QuotationType>
    {
        public QuotationTypeSyncConfig()
        {
            Key(x => x.Name);
        }
    }
}
