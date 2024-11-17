using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;



public enum WarrantyStatus
{
    Encoding = 1,
    Completed = 2

}

namespace Inventory.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Bottom)]
    [XafDisplayName("Warranty Entry Form")]
    [NavigationItem("Warranty")]
    [Appearance("EncodingWarranty", Criteria = "WarrantyStatus= 1", BackColor = "255, 192, 192", TargetItems = "*", Context = "ListView")] //Turn inventory items 
    [Appearance("WarrantyCompleted", Criteria = "WarrantyStatus=2", TargetItems = "*", Enabled = false)]
    [Appearance("WarrantyComppleted2", Criteria = "WarrantyStatus=2", TargetItems = "WarrantyDetails", Enabled = false)]
    [Appearance("HideSaveStatEncodeW", AppearanceItemType = "Action", TargetItems = "Save", Enabled = false, Criteria = "WarrantyStatus=1")]
    [Appearance("HideRefshCompleteW", AppearanceItemType = "Action", TargetItems = "Refresh", Enabled = false, Criteria = "WarrantyStatus=2")]

    public class Warranty : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Warranty(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            DateTime phTime = GetTimeZoneInPh();
            WarrantyDate = phTime;
            CreatedDate = phTime;
            WarrantyStatus = WarrantyStatus.Encoding;
            Employee currentUser = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            if (currentUser != null)
            {
                this.Branch = currentUser.Branch;
            }
            CreatedBy = currentUser;
        }

        [Action(Caption = "For Warranty Completed?", ConfirmationMessage = "Are you sure you encoded correctly the warranty transaction?", ImageName = "Attention", AutoCommit = true)]
        public void ActionMethod()
        {
            if (Customer != null)
            {
                if (this.WarrantyStatus == WarrantyStatus.Encoding)
                {
                    this.WarrantyStatus = WarrantyStatus.Completed;
                }
            }
        }


        WarrantyStatus warrantyStatus;
        Branch branch;
        Customer customer;
        DateTime warrantyDate;

        public DateTime WarrantyDate
        {
            get => warrantyDate;
            set => SetPropertyValue(nameof(WarrantyDate), ref warrantyDate, value);
        }


        [RuleRequiredField(DefaultContexts.Save)]
        public Customer Customer
        {
            get => customer;
            set => SetPropertyValue(nameof(Customer), ref customer, value);
        }


        [Association("Branch-Warranties")]
        [VisibleInDetailView(false)]
        public Branch Branch
        {
            get => branch;
            set => SetPropertyValue(nameof(Branch), ref branch, value);
        }
        
        public WarrantyStatus WarrantyStatus
        {
            get => warrantyStatus;
            set => SetPropertyValue(nameof(WarrantyStatus), ref warrantyStatus, value);
        }

        [Association("Warranty-WarrantyDetails")]
        public XPCollection<WarrantyDetail> WarrantyDetails
        {
            get
            {
                return GetCollection<WarrantyDetail>(nameof(WarrantyDetails));
            }
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
    }
}