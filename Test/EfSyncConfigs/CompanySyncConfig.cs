using Ferhah.SyncNetObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferhah.SyncNetObjects.Test.EfModels;


namespace Ferhah.SyncNetObjects.Test.EfSyncConfigs
{
        
    public class ConditionSyncConfig : SyncConfiguration<CompanyComplexType>
    {
        public ConditionSyncConfig()
        {
            PrimitiveProperty(x => x.Expression);
        }
    }

    public class CompanySyncConfig : SyncConfiguration<Company>
    {
        public CompanySyncConfig()
        {
            Key(x => x.Name);
            PrimitiveProperty(x => x.Address);
            ComplexType(x => x.CompanyComplexType, new ConditionSyncConfig());       
        }
    }
}
