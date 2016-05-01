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

namespace Ferhah.SyncNetObjects.Test
{
    [TestClass]
    public class ConfigExceptionTest
    {   
        public class Company
        {
            public String Name { get; set; }
            public String Address { get; set; }
            public CompanyType CompanyType { get; set; }
            public int Number;
        }

        public class CompanyType
        {
            public String Name { get; set; }
        }

        public class CompanySyncConfigNotPrimitive : SyncConfiguration<Company>
        {
            public CompanySyncConfigNotPrimitive()
            {
                Key(x => x.Name);
                PrimitiveProperty(x => x.CompanyType);
            }
        }

        public class CompanySyncConfigNotNavi : SyncConfiguration<Company>
        {
            public CompanySyncConfigNotNavi()
            {   
                NavigationProperty(x => x.Address);
            }
        }


        public class CompanySyncConfigNotProp : SyncConfiguration<Company>
        {
            public CompanySyncConfigNotProp()
            {
                PrimitiveProperty(x => x.Number);
            }
        }

        public class CompanyTypeConfigCannotHaveKeys : SyncConfiguration<CompanyType>
        {
            public CompanyTypeConfigCannotHaveKeys()
            {
                Key(x => x.Name);
            }
        }


        public class CompanySyncConfigWithInvalidComplexType : SyncConfiguration<Company>
        {
            public CompanySyncConfigWithInvalidComplexType()
            {
                ComplexType(x => x.CompanyType, new CompanyTypeConfigCannotHaveKeys());
            }
        }


        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(SyncConfigException), "source : Company message: CompanyType Not primitive key")]
        public void ConfigExceptionTest1()
        {
            new CompanySyncConfigNotPrimitive();
        }

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(SyncConfigException), "source : Company message: Address Not a navigation property")]
        public void ConfigExceptionTest2()
        {
            new CompanySyncConfigNotNavi();
        }
        
        
        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(SyncConfigException), "source : Company message: Number is not a property")]
        public void ConfigExceptionTest3()
        {
            new CompanySyncConfigNotProp();
        }

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(SyncConfigException), "source : Company message: CompanyType ComplexTypes cannot have keys")]
        public void ConfigExceptionTest4()
        {
            new CompanySyncConfigWithInvalidComplexType();
        }
        
    }

}
