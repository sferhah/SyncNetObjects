using Ferhah.SyncNetObjects.Test.EfModels;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.EfConfigs
{
    public class BusinessContext : DbContext
    {
        private const string ConnectionStringName = "SyncDB";

        public BusinessContext()
            : base(ConnectionStringName) { }

        public BusinessContext(DbConnection connection)
            : base(connection, true) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }        

        public DbSet<Quotation> Quotations { get; set; } 
        public DbSet<QuotationItem> QuotationItems { get; set; }
        public DbSet<QuotationType> QuotationTypes { get; set; }
        public DbSet<CompositeObjectPartOne> CompositeObjects { get; set; }
        public DbSet<CompositeObjectPartTwo> CompositePartTwoObjects { get; set; }

        public DbSet<CircularCompositeObjectPartOne> CircularCompositeObjects { get; set; }
        public DbSet<CircularCompositeObjectPartTwo> CircularCompositePartTwoObjects { get; set; }
        public DbSet<CircularChild> CircularChildren { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new CompanyEfConfig());
            modelBuilder.Configurations.Add(new ContactEfConfig());            
            modelBuilder.Configurations.Add(new QuotationEfConfig());
            modelBuilder.Configurations.Add(new QuotationItemEfConfig());
            modelBuilder.Configurations.Add(new QuotationTypeEfConfig());
            modelBuilder.Configurations.Add(new CompositeObjectPartOneEfConfig());
            modelBuilder.Configurations.Add(new CompositeObjectPartTwoEfConfig());

            modelBuilder.Configurations.Add(new CircularCompositeObjectPartOneEfConfig());
            modelBuilder.Configurations.Add(new CircularCompositeObjectPartTwoEfConfig());
            modelBuilder.Configurations.Add(new CircularChildEfConfig());
            modelBuilder.Configurations.Add(new ConditionEfConfig());


        }
    }
}
