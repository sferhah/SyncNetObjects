using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ferhah.SyncNetObjects;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;
using KellermanSoftware.CompareNetObjects;
using System.Collections.Generic;
using Ferhah.SyncNetObjects.Exceptions;
using Ferhah.SyncNetObjects.Test.NetModels;
using Ferhah.SyncNetObjects.Test.NetSyncConfigs;

namespace Ferhah.SyncNetObjects.Test
{
    [TestClass]
    public class SyncExceptionTest
    {
        [TestMethod]        
        [ExpectedExceptionWithMessage(typeof(NonUniqueObjectException), "source : Company message: MICROSOFT")]
        public void SyncExceptionTest1()
        {
            Sync(GetSetWithDuplicateObject(), new BusinessSet());
        }

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(FakeReferencedObjectException), "source : Contact message: Company Fake")]
        public void SyncExceptionTest2()
        {
            Sync(GetSetWithFakeRef(), new BusinessSet());            
        }

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(NullKeyException), "source : Company message: Name")]
        public void SyncExceptionTest3()
        {
            Sync(GetSetWithNullKeyObject(), new BusinessSet());            
        }

        static void Sync(BusinessSet original_businesSet, BusinessSet newest_businessSet)
        {


            var syncConfigSet = new SyncConfigSet<BusinessSet>(
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



        static BusinessSet GetSetWithDuplicateObject()
        {
            BusinessSet businesSet = new BusinessSet();

            businesSet.Companies.Add(new Company { Name = "Microsoft", Address = "248 Mars street" });
            businesSet.Companies.Add(new Company { Name = "Microsoft", Address = "78 Mars street" });

            return businesSet;

        }

        static BusinessSet GetSetWithFakeRef()
        {
            BusinessSet businesSet = new BusinessSet();

            businesSet.Companies.Add(new Company { Name = "Microsoft", Address = "248 Mars street" });
            businesSet.Contacts.Add(new Contact { Name = "Dalton", FirstName = "John", Address = "6th Avenue", Company = new Company { Name = "Fake" } });         

            return businesSet;

        }

        static BusinessSet GetSetWithNullKeyObject()
        {
            BusinessSet businesSet = new BusinessSet();

            businesSet.Companies.Add(new Company { Name = null, Address = "248 Mars street" });            

            return businesSet;

        }
    
    }

}
