using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferhah.SyncNetObjects.Test.EfModels;
using System.Data.Entity.ModelConfiguration;

namespace Ferhah.SyncNetObjects.Test.EfConfigs
{
    public class ContactEfConfig : EntityTypeConfiguration<Contact>
    {
        public ContactEfConfig()
        {            
            HasKey(x => x.ID);
            Property(e => e.ID).HasColumnName("ID");
            Property(e => e.FirstName).HasColumnName("FirstName");
            Property(e => e.Name).HasColumnName("Name");
            Property(e => e.Address).HasColumnName("Address");
            Property(e => e.CompanyID).HasColumnName("CompanyID");


            HasRequired(e => e.Company).WithMany().HasForeignKey(e => e.CompanyID);
            ToTable("Contact");
        }
    }
}
