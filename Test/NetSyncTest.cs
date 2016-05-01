using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ferhah.SyncNetObjects.Test.NetSyncConfigs;
using Ferhah.SyncNetObjects;
using System.Collections;
using System.Linq;
using Ferhah.SyncNetObjects.Test.NetModels;
using System.Reflection;
using System.Text;
using KellermanSoftware.CompareNetObjects;
using System.Collections.Generic;

namespace Ferhah.SyncNetObjects.Test
{
    [TestClass]
    public class NetSyncTest
    {

        [TestMethod]
        public void NetTest1()
        {
            Sync(GetIntialBusinessSet(), GetNewestBusinessSet());
        }


        static void Sync(BusinessSet original_businesSet, BusinessSet newest_businessSet)
        {


            var syncConfigSet = new SyncConfigSet<BusinessSet>(
                                                        new ObjectWithCompositeKeySyncConfig(),
                                                        new CompanySyncConfig(),
                                                        new ContactSyncConfig(),
                                                        new QuotationSyncConfig(),
                                                        new QuotationItemSyncConfig(),
                                                        new QuotationTypeSyncConfig(),
                                                        new CompositeObjectPartOneSyncConfig());

         
            Syncer<BusinessSet> syncer = new Syncer<BusinessSet>(original_businesSet, newest_businessSet, syncConfigSet);

            SyncComparisonResult<BusinessSet> result = syncer.CompareSet();
            syncer.SyncSet();

            SyncHelper.AssertNoDiff(original_businesSet, newest_businessSet, false);

        }

      

        static BusinessSet GetIntialBusinessSet()
        {
            BusinessSet businesSet = new BusinessSet();

            businesSet.ObjectWithCompositeKeyCollection.Add(new ObjectWithCompositeKey {  Key_1 = "x", Key_2 ="xy", Key_3 = new Company { Name = "Microsoft" } });
            businesSet.ObjectWithCompositeKeyCollection.Add(new ObjectWithCompositeKey { Key_1 = "xx", Key_2 = "y", Key_3 = new Company { Name = "Microsoft" } });        

            businesSet.Companies.Add(new Company { Name = "Microsoft", Address = "248 Mars street" });
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
         

            return businesSet;
        }

        static BusinessSet GetNewestBusinessSet()
        {
            BusinessSet newest_businessSet_1 = new BusinessSet();

            newest_businessSet_1.Companies.Add(new Company { Name = "Microsoft", Address = "newest 248 Mars street" });
            newest_businessSet_1.Companies.Add(new Company { Name = "Apple", Address = "85 Captain Bd" });

            newest_businessSet_1.Contacts.Add(new Contact { Name = "Dalton", FirstName = "John", Address = "6th Avenue", Company = new Company { Name = "Microsoft" } });
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
