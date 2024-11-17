using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
//using DevExpress.Printing.Core.PdfExport.Metafile;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Inventory.Module.BusinessObjects
{
    //[DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Bottom)]
    [VisibleInReports(true)]
    [RuleCriteria("ValidManualInvQty", DefaultContexts.Save, "Quantity >= 0", SkipNullOrEmptyValues = false, UsedProperties = "Quantity", CustomMessageTemplate = "Quantity must be equal or greater than Zero.")] //Ensure the quantity is not 0

    public class ManualInventoryDetail : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public ManualInventoryDetail(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Employee currentUser = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            if (currentUser != null)
            {
                this.Branch = currentUser.Branch;
                CreatedBy = currentUser;
            }
        }



        [Association("ManualInventory-ItemsCounted")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public ManualInventory ManualInventory
        {
            get => manualInventory;
            set => SetPropertyValue(nameof(ManualInventory), ref manualInventory, value);
        }

        [RuleRequiredField(DefaultContexts.Save)]
        public Stock Stock
        {
            get => stock;
            set => SetPropertyValue(nameof(Stock), ref stock, value);
        }


        
        public int Quantity
        {
            get => quantity;
            set => SetPropertyValue(nameof(Quantity), ref quantity, value);
        }

        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Remark
        {
            get => remark;
            set => SetPropertyValue(nameof(Remark), ref remark, value);
        }



        protected override void OnSaving()
        {
            LastModifiedDate = DateTime.Now;
            LastModifiedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            base.OnSaving();
        }

        protected override void OnDeleting()
        {
            DeletedDate = GetCurrentTime();
            DeletedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            base.OnDeleting();
        }

        Branch branch;
        string remark;
        int quantity;
        Stock stock;
        ManualInventory manualInventory;
        private DateTime _createdDate;

        [Browsable(false)]
        public DateTime CreatedDate
        {
            get => _createdDate;
            set => SetPropertyValue<DateTime>("CreatedDate", ref _createdDate, value);
        }

        private Employee _createdBy;

        [Browsable(false)]
        public Employee CreatedBy
        {
            get => _createdBy;
            set => SetPropertyValue<Employee>("CreatedBy", ref _createdBy, value);
        }

        private DateTime _lastModifiedDate;

        [Browsable(false)]
        public DateTime LastModifiedDate
        {
            get => _lastModifiedDate;
            set => SetPropertyValue<DateTime>("LastModifiedDate", ref _lastModifiedDate, value);
        }

        private Employee _lastModifiedBy;

        [Browsable(false)]
        public Employee LastModifiedBy
        {
            get => _lastModifiedBy;
            set => SetPropertyValue<Employee>("LastModifiedBy", ref _lastModifiedBy, value);
        }

        private DateTime _deletedDate;

        [Browsable(false)]
        public DateTime DeletedDate
        {
            get => _deletedDate;
            set => SetPropertyValue<DateTime>("DeletedDate", ref _deletedDate, value);
        }

        private Employee _deletedBy;

        [Browsable(false)]
        public Employee DeletedBy
        {
            get => _deletedBy;
            set => SetPropertyValue<Employee>("DeletedBy", ref _deletedBy, value);
        }


        protected DateTime GetCurrentTime()
        {
            DateTime serverTime = DateTime.Now;
            DateTime _localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(serverTime, TimeZoneInfo.Local.Id, "Singapore Standard Time");
            return _localTime;



        }
        static DateTime GetTimeZoneInPh()
        {
            DateTime localDateTime = DateTime.Now;
            TimeZoneInfo phTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"); // Closest equivalent to PH time
            DateTime phTime = TimeZoneInfo.ConvertTime(localDateTime, TimeZoneInfo.Local, phTimeZone);

            return phTime; // Return the Philippine time
        }


        [VisibleInListView(false), VisibleInDetailView(false)]
        public Branch Branch
        {
            get => branch;
            set => SetPropertyValue(nameof(Branch), ref branch, value);
        }


    }
}