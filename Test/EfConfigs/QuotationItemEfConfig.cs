using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferhah.SyncNetObjects.Test.EfModels;
using System.Data.Entity.ModelConfiguration;

namespace Ferhah.SyncNetObjects.Test.EfConfigs
{
    public class QuotationItemEfConfig : EntityTypeConfiguration<QuotationItem>
    {
        public QuotationItemEfConfig()
        {
            HasKey(x => x.ID);
            Property(e => e.ID).HasColumnName("ID");            
            Property(e => e.Name).HasColumnName("Name");

            Property(e => e.QuotationID).HasColumnName("QuotationID");


            HasRequired(e => e.Quotation).WithMany().HasForeignKey(e => e.QuotationID);

            ToTable("QuotationItem");
        }
    }
}
