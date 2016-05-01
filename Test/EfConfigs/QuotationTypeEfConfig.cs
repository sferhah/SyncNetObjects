using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferhah.SyncNetObjects.Test.EfModels;
using System.Data.Entity.ModelConfiguration;

namespace Ferhah.SyncNetObjects.Test.EfConfigs
{
    public class QuotationTypeEfConfig : EntityTypeConfiguration<QuotationType>
    {
        public QuotationTypeEfConfig()
        {
            HasKey(x => x.ID);
            Property(e => e.ID).HasColumnName("ID");            
            Property(e => e.Name).HasColumnName("Name");
            ToTable("QuotationType");
        }
    }
}
