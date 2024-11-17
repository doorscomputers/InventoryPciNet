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


public enum BoStatus
{
    Encoding = 1,
    Completed = 2

}


namespace Inventory.Module.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Items Returned to Suppliers")]
    [NavigationItem("BO")]
    [Appearance("EncodingSalesBo", Criteria = "BoStatus= 1", BackColor = "255, 192, 192", TargetItems = "*", Context = "ListView")] //Turn inventory items 
    [Appearance("BoCompleted", Criteria = "BoStatus=2", TargetItems = "*", Enabled = false)]
    [Appearance("BoComppleted2", Criteria = "BoStatus=2", TargetItems = "BoDetails", Enabled = false)]
    [Appearance("HideSaveStatEncodeB", AppearanceItemType = "Action", TargetItems = "Save", Enabled = false, Criteria = "BoStatus=1")]
    [Appearance("HideRefshCompleteB", AppearanceItemType = "Action", TargetItems = "Refresh", Enabled = false, Criteria = "BoStatus=2")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Bo : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Bo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            DateTime phTime = GetTimeZoneInPh();
            BoDate = phTime;
            CreatedDate = phTime;
            BoStatus = BoStatus.Encoding;
            // Initialize the Branch to the logged-in user's branch.
            Employee currentUser = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);

            if (currentUser != null)
            {
                this.Branch = currentUser.Branch;
            }
            // Initialize the CreatedBy field to the currently logged-in user.
            CreatedBy = currentUser;
        }

        [Action(Caption = "BO Completed?", ConfirmationMessage = "Are you sure you encoded correctly the BO transaction?", ImageName = "Attention", AutoCommit = true)]
        public void ActionMethod()
        {
            // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
            if (Supplier != null)
            { 
                if (this.BoStatus == BoStatus.Encoding)
                {
                    this.BoStatus = BoStatus.Completed;
                }
            }
        }

        BoStatus boStatus;
        Branch branch;
        string remark;
        Supplier supplier;
        DateTime boDate;




        [Association("Branch-Bos")]
        [VisibleInDetailView(false)]
        public Branch Branch
        {
            get => branch;
            set => SetPropertyValue(nameof(Branch), ref branch, value);
        }



        //[RuleRequiredField(DefaultContexts.Save)]
        public DateTime BoDate
        {
            get => boDate;
            set => SetPropertyValue(nameof(BoDate), ref boDate, value);
        }

        [RuleRequiredField(DefaultContexts.Save)]
        public Supplier Supplier
        {
            get => supplier;
            set => SetPropertyValue(nameof(Supplier), ref supplier, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Remark
        {
            get => remark;
            set => SetPropertyValue(nameof(Remark), ref remark, value);
        }

        
        public BoStatus BoStatus
        {
            get => boStatus;
            set => SetPropertyValue(nameof(BoStatus), ref boStatus, value);
        }



        [Association("Bo-BoDetails")]
        public XPCollection<BoDetail> BoDetails
        {
            get
            {
                return GetCollection<BoDetail>(nameof(BoDetails));
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
            DeletedDate = GetTimeZoneInPh();
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



        static DateTime GetTimeZoneInPh()
        {
            DateTime localDateTime = DateTime.Now;
            TimeZoneInfo phTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"); // Closest equivalent to PH time
            DateTime phTime = TimeZoneInfo.ConvertTime(localDateTime, TimeZoneInfo.Local, phTimeZone);

            return phTime; // Return the Philippine time
        }




    }
}