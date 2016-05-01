using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferhah.SyncNetObjects.Test.EfModels;
using System.Data.Entity.ModelConfiguration;

namespace Ferhah.SyncNetObjects.Test.EfConfigs
{
    public class QuotationEfConfig : EntityTypeConfiguration<Quotation>
    {
        public QuotationEfConfig()
        {
            HasKey(x => x.ID);
            Property(e => e.ID).HasColumnName("ID");            
            Property(e => e.Name).HasColumnName("Name");
            Property(e => e.Description).HasColumnName("Description");            

            Property(e => e.CompanyID).HasColumnName("CompanyID");
            Property(e => e.QuotationTypeID).HasColumnName("QuotationTypeID");

            HasOptional(e => e.Type).WithMany().HasForeignKey(e => e.QuotationTypeID);

            ToTable("Quotation");
        }
    }
}
