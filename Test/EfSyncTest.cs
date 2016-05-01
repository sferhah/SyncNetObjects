using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ferhah.SyncNetObjects.Test.EfConfigs;
using Ferhah.SyncNetObjects;
using Ferhah.SyncNetObjects.Test.EfSyncConfigs;
using System.Data.Entity;
using System.Collections;
using System.Linq;
using Ferhah.SyncNetObjects.Test.EfModels;
using System.Text;
using KellermanSoftware.CompareNetObjects;
using System.Collections.Generic;

namespace Ferhah.SyncNetObjects.Test
{
    [TestClass]
    public class EfSyncTest
    { 

        [TestMethod]
        public void EfTest1()
        {
            //Updates/Deletes/inserts data in database
            using (BusinessContext context = new BusinessContext())
            {
                Sync(context, GetDatabaseSet(context), GetNewestBusinessSet());
            }
        }

        [TestInitialize]
        public void Init()
        {
            using (BusinessContext context = new BusinessContext())
            {             
                EmptyDatabase(context);
            }

            using (BusinessContext context = new BusinessContext())
            {
                Sync(context, GetDatabaseSet(context), GetIntialBusinessSet());
            }
        }

        [TestCleanup]
        public void Clean()
        {
            //using (BusinessContext context = new BusinessContext())
            //{
            //    EmptyDatabase(context);
            //}
        }


        static void Sync(BusinessContext context, BusinessSet original_businesSet, BusinessSet newest_businessSet)
        {


            var syncConfigSet = new SyncConfigSet<BusinessSet>(
                                                        new CompanySyncConfig(),
                                                        new ContactSyncConfig(),                                                        
                                                        new QuotationSyncConfig(),
                                                        new QuotationItemSyncConfig(),
                                                        new QuotationTypeSyncConfig(),
                                                        new CompositeObjectPartOneSyncConfig(),
                                                        new CircularCompositeObjectPartOneSyncConfig(),
                                                        new CircularChildSyncConfig()
                                                        );
           
            EfSyncer<BusinessSet> syncer = new EfSyncer<BusinessSet>(context, original_businesSet, newest_businessSet, syncConfigSet);

            SyncComparisonResult<BusinessSet> result = syncer.CompareSet();
            syncer.SyncSet();


            SyncHelper.AssertNoDiff(original_businesSet, newest_businessSet, true);
        }

     

        public static void EmptyDatabase(DbContext context)
        {

            var context_dbsets = context.GetType().GetProperties()
                                                                .Where(p => typeof(IEnumerable).IsAssignableFrom(p.PropertyType)
                                                                && p.PropertyType.GenericTypeArguments.Any()).ToList();

            foreach (var p_dbset in context_dbsets)
            {
                Type t = p_dbset.PropertyType.GenericTypeArguments.First();
                DbSet dbset = context.Set(t);

                foreach (var item in dbset)
                {
                    dbset.Remove(item);
                }

            }

            context.SaveChanges();

        }

        static BusinessSet GetDatabaseSet(BusinessContext context)
        {

            BusinessSet businesSet = new BusinessSet();

            businesSet.QuotationTypes = context.QuotationTypes.ToList();            
            businesSet.Contacts = context.Contacts.ToList();
            businesSet.QuotationItems = context.QuotationItems.ToList();
            businesSet.Quotations = context.Quotations.ToList();
            businesSet.Companies = context.Companies.ToList();
            businesSet.CompositeObjects = context.CompositeObjects.Include(x => x.CompositeObjectPartTwo).ToList();

            businesSet.CircularCompositeObjects = context.CircularCompositeObjects.Include(x => x.CircularCompositeObjectPartTwo).ToList();
            businesSet.CircularChildren = context.CircularChildren.ToList();

            return businesSet;

        }


        static BusinessSet GetIntialBusinessSet()
        {
            BusinessSet businesSet = new BusinessSet();

            var c1 = new Company { Name = "Microsoft", Address = "newest 248 Mars street" };
            c1.CompanyComplexType.Expression = "toto == titi && titi ==2";

            businesSet.Companies.Add(c1);           


           
            businesSet.Contacts.Add(new Contact { Name = "Dalton", FirstName = "John", Address = "6th Avenue", Company = new Company { Name = "Microsoft" } });
            businesSet.Contacts.Add(new Contact { Name = "McJo", FirstName = "Max", Address = "67 Great wall", Company = new Company { Name = "Microsoft" } });
            


            businesSet.Quotations.Add(new Quotation { Name = "589", Description = "Old", Type = new QuotationType { Name = "Lost" }, Company = new Company { Name = "Microsoft" } });
            businesSet.QuotationItems.Add(new QuotationItem { Name = "Line 1", Quotation = new Quotation { Name = "589" } });

            businesSet.QuotationTypes.Add(new QuotationType { Name = "Lost" });
            businesSet.QuotationTypes.Add(new QuotationType { Name = "Won" });

            businesSet.CompositeObjects.Add(new CompositeObjectPartOne
            {
                Name = "Composite",
                IsNice = false,
                CompositeObjectPartTwo = new CompositeObjectPartTwo
                {
                    Description = "CompositeHelper",
                    CompositeObjectPartOne = new CompositeObjectPartOne { Name = "Composite" },
                    Company = new Company { Name = "Microsoft" }
                }
            });

            businesSet.CircularCompositeObjects.Add(new CircularCompositeObjectPartOne
            {
                Name = "CircularComposite 1",
                IsNice = false,
                CircularCompositeObjectPartTwo = new CircularCompositeObjectPartTwo
                {
                    Description = "CircularCompositeHelper",
                    CircularCompositeObjectPartOne = new CircularCompositeObjectPartOne { Name = "CircularComposite 1" },
                    CircularChild = new CircularChild { Name = "Child" }
                }
            });

            businesSet.CircularCompositeObjects.Add(new CircularCompositeObjectPartOne
            {
                Name = "CircularComposite 2",
                IsNice = false,
            });

            businesSet.CircularChildren.Add(new CircularChild
            {
                Name = "Child",
                CircularParent = new CircularCompositeObjectPartOne { Name = "CircularComposite 1" },
                CircularParent2 = new CircularCompositeObjectPartOne { Name = "CircularComposite 2" }
            });

            return businesSet;
        }

        static BusinessSet GetNewestBusinessSet()
        {
            BusinessSet newest_businessSet_1 = new BusinessSet();


            var c1 = new Company { Name = "Microsoft", Address = "newest 248 Mars street" };
            var c2 = new Company { Name = "Apple", Address = "85 Captain Bd" };

            c1.CompanyComplexType.Expression = "updated";         



            newest_businessSet_1.Companies.Add(c1);
            newest_businessSet_1.Companies.Add(c2);
            

            var cc1 = new Contact { Name = "Dalton", FirstName = "John", Address = "6th Avenue", Company = new Company { Name = "Microsoft" } };
            cc1.Condition.Expression = "new";

            newest_businessSet_1.Contacts.Add(cc1);           
            newest_businessSet_1.Contacts.Add(new Contact { Name = "Mohenberg", FirstName = "Nick", Address = "67 Great wall", Company = new Company { Name = "Microsoft" } });          


            newest_businessSet_1.Quotations.Add(new Quotation { Name = "589", Description = "New", Type = new QuotationType { Name = "Won" }, Company = new Company { Name = "Microsoft" } });
            newest_businessSet_1.QuotationItems.Add(new QuotationItem { Name = "Line 2", Quotation = new Quotation { Name = "589" } });
            newest_businessSet_1.QuotationItems.Add(new QuotationItem { Name = "Line 3", Quotation = new Quotation { Name = "589" } });


            newest_businessSet_1.QuotationTypes.Add(new QuotationType { Name = "Lost" });
            newest_businessSet_1.QuotationTypes.Add(new QuotationType { Name = "Won" });


            newest_businessSet_1.CompositeObjects.Add(new CompositeObjectPartOne
            {
                Name = "Composite",
                IsNice = false,
                CompositeObjectPartTwo = new CompositeObjectPartTwo
                {
                    Description = "CompositeHelper Updated",
                    CompositeObjectPartOne = new CompositeObjectPartOne { Name = "Composite" },
                    Company = new Company { Name = "Apple" }
                }
            });


            return newest_businessSet_1;
        }
    }
}
