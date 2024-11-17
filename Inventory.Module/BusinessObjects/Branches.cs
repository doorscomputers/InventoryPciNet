using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;

namespace Inventory.Module.BusinessObjects
{
    public class Branches : XPObject
    {
        //public Branches() : base()
        //{
        //    // This constructor is used when an object is loaded from a persistent storage.
        //    // Do not place any code here.
        //}

        public Branches(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }



        string branchName;
        int branchId;

        public int BranchId
        {
            get => branchId;
            set => SetPropertyValue(nameof(BranchId), ref branchId, value);
        }

        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string BranchName
        {
            get => branchName;
            set => SetPropertyValue(nameof(BranchName), ref branchName, value);
        }

        //[Association("Branches-TransfersTo")]
        //public XPCollection<Transfer> TransfersTo
        //{
        //    get
        //    {
        //        return GetCollection<Transfer>(nameof(TransfersTo));
        //    }
        //}






    }

}