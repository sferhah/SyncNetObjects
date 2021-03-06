﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.EfModels
{
    public class BusinessSet
    {
        public List<Company> Companies { get; set; } = new List<Company>();
        public List<Contact> Contacts { get; set; } = new List<Contact>();       


        public List<Quotation> Quotations { get; set; } = new List<Quotation>();
        public List<QuotationItem> QuotationItems { get; set; } = new List<QuotationItem>();
        public List<QuotationType> QuotationTypes { get; set; } = new List<QuotationType>();


        public List<CompositeObjectPartOne> CompositeObjects { get; set; } = new List<CompositeObjectPartOne>();

        public List<CircularCompositeObjectPartOne> CircularCompositeObjects { get; set; } = new List<CircularCompositeObjectPartOne>();
        public List<CircularChild> CircularChildren { get; set; } = new List<CircularChild>();

    }
}
