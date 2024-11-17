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


public enum ManualInvStatus
{
    Encoding = 1,
    Completed = 2

}


namespace Inventory.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [Appearance("EncodingMInv", Criteria = "ManualInvStatus= 1", BackColor = "255, 192, 192", TargetItems = "*", Context = "ListView")] //Turn inventory items 
    [Appearance("InvCompleted", Criteria = "ManualInvStatus=2", TargetItems = "*", Enabled = false)]
    [Appearance("InvComppleted2", Criteria = "ManualInvStatus=2", TargetItems = "ManualInventoryDetails", Enabled = false)]
    [Appearance("HideSaveStatInvEncode", AppearanceItemType = "Action", TargetItems = "Save", Enabled = false, Criteria = "ManualInvStatus=1")]
    [Appearance("HideRefshInvComplete", AppearanceItemType = "Action", TargetItems = "Refresh", Enabled = false, Criteria = "ManualInvStatus=2")]
    public class ManualInventory : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public ManualInventory(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            DateTime phTime = GetTimeZoneInPh();
            InventoryDate = phTime;//DateTime.Now;

            CreatedDate =phTime;
            ManualInvStatus = ManualInvStatus.Encoding;

            // Initialize the Branch to the logged-in user's branch.
            Employee currentUser = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);

            if (currentUser != null)
            {
                this.Branch = currentUser.Branch;
            }
            // Initialize the CreatedBy field to the currently logged-in user.
            CreatedBy = currentUser;



        }



        [Action(Caption = "Manual Inv. Completed?", ConfirmationMessage = "Are you sure you encoded correctly the Manual Inventory transaction?", ImageName = "Attention", AutoCommit = true)]
        public void ActionMethod()
        {
            // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
            if (this.ManualInvStatus == ManualInvStatus.Encoding)
            {
                this.ManualInvStatus = ManualInvStatus.Completed;
            }
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue(nameof(PersistentProperty), ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}


        ManualInvStatus manualInvStatus;
        string remarks;
        Branch branch;
        DateTime inventoryDate;

        public DateTime InventoryDate
        {
            get => inventoryDate;
            set => SetPropertyValue(nameof(InventoryDate), ref inventoryDate, value);
        }



        //[VisibleInDetailView(false)]
        public Branch Branch
        {
            get => branch;
            set => SetPropertyValue(nameof(Branch), ref branch, value);
        }



        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Remarks
        {
            get => remarks;
            set => SetPropertyValue(nameof(Remarks), ref remarks, value);
        }




        public ManualInvStatus ManualInvStatus
        {
            get => manualInvStatus;
            set => SetPropertyValue(nameof(ManualInvStatus), ref manualInvStatus, value);
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


        [Association("ManualInventory-ItemsCounted")]
        public XPCollection<ManualInventoryDetail> ItemsCounted
        {
            get
            {
                return GetCollection<ManualInventoryDetail>(nameof(ItemsCounted));
            }
        }










    }
}