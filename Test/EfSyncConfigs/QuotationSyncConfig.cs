using Ferhah.SyncNetObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferhah.SyncNetObjects.Test.EfModels;


namespace Ferhah.SyncNetObjects.Test.EfSyncConfigs
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
