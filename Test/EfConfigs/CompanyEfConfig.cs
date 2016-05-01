using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferhah.SyncNetObjects.Test.EfModels;
using System.Data.Entity.ModelConfiguration;

namespace Ferhah.SyncNetObjects.Test.EfConfigs
{
    
    public class ConditionEfConfig : ComplexTypeConfiguration<CompanyComplexType>
    {
        public ConditionEfConfig()
        {   
            Property(t => t.Expression).HasColumnName("Expression");
            Ignore(t => t.Toto);
        }
    }

    public class CompanyEfConfig : EntityTypeConfiguration<Company>
    {
        public CompanyEfConfig()
        {
            HasKey(x => x.ID);
            Property(e => e.ID).HasColumnName("ID");
            Property(e => e.Name).HasColumnName("Name");
            Property(e => e.Address).HasColumnName("Address");

          //  HasOptional(quote => quote.LocalizedObject)
          //.WithRequired(order => ((CompanyLabel)order).Company);


            ToTable("Company");
        }
    }
}
