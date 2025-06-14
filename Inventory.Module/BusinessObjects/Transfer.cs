using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;



public enum TransferStatus
{
    Encoding = 1,
    Transferred = 2
    
}


namespace Inventory.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Transfers")]
    [XafDisplayName("Transfer Entry Form")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Bottom)]
    [Appearance("EncodingSalesT", Criteria = "TransferStatus= 1", BackColor = "255, 192, 192", TargetItems = "*", Context = "ListView")] //Turn inventory items 
    [Appearance("SalesCompletedT", Criteria = "TransferStatus=2", TargetItems = "*", Enabled = false)]
    [Appearance("SaleComppleted2T", Criteria = "TransferStatus=2", TargetItems = "TransferDetails", Enabled = false)]
    [Appearance("HideSaveStatEncodeT", AppearanceItemType = "Action", TargetItems = "Save", Enabled = false, Criteria = "TransferStatus=1")]
    [Appearance("HideRfshCompleteT", AppearanceItemType = "Action", TargetItems = "Refresh", Enabled = false, Criteria = "TransferStatus=2")]
    [Appearance("TransferD8Disable", Criteria = "AddDays(Today(), -2) > DateTransferred", TargetItems = "*", Enabled = false)]
    //[RuleCriteria("FromBranchToBranchMustNotBeEqual", DefaultContexts.Save, "FromBranch <> ToBranch",
    //SkipNullOrEmptyValues = false, UsedProperties = "FromBranch",
    //CustomMessageTemplate = "From Branch must not be equal with To Branch")]
    //[RuleCriteria("FromBranchToBranchMustNotBeEqual", DefaultContexts.Save, "FromBranch <> ToBranch", SkipNullOrEmptyValues = false, UsedProperties = "FromBranch", CustomMessageTemplate = "From Branch must not be equal with To Branch.")]
    public class Transfer : XPObject
    {
        

        public Transfer(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            DateTime phTime = GetTimeZoneInPh();
            DateTransferred = phTime;
            Employee currentUser = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            TransferStatus = TransferStatus.Encoding;
            if (currentUser != null)
            {
                this.FromBranch = currentUser.Branch;
            }
            //// Initialize the CreatedBy field to the currently logged-in user.
            CreatedBy = currentUser;

        }

        [Action(Caption = "Transfer Items Completed?", ConfirmationMessage = "Are you sure you encoded correctly the transfer transaction?", ImageName = "Attention", AutoCommit = true)]
        public void ActionMethod()
        {
            // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
            if (ToBranch != null)
            {
                if (this.TransferStatus == TransferStatus.Encoding)
                {
                    this.TransferStatus = TransferStatus.Transferred;
                }
            }
        }

        int toBranch;
        Branches branches;
        TransferStatus transferStatus;
        string remarks;
        DateTime dateTransferred;
        //Branches transferTo;
        private Branch fromBranch;




        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(false)]
        public DateTime DateTransferred
        {
            get => dateTransferred;
            set => SetPropertyValue(nameof(DateTransferred), ref dateTransferred, value);
        }


        [Association("Branch-TransfersFrom")]
        [VisibleInDetailView(false)]
        public Branch FromBranch
        {
            get => fromBranch;
            set => SetPropertyValue(nameof(FromBranch), ref fromBranch, value);
        }


        [RuleRequiredField(DefaultContexts.Save)]
        [XafDisplayName("Transfer To")]
        public Branches Branches
        {
            get => branches;
            set
            {
                SetPropertyValue(nameof(Branches), ref branches, value);
                if (!IsLoading && !IsSaving && !IsDeleted)
                {
                    if (Branches != null)
                    {
                        this.ToBranch = value.BranchId;
                    }
                }
            }
        }


        [VisibleInListView(false), VisibleInDetailView(false)]
        public int ToBranch
        {
            get => toBranch;
            set => SetPropertyValue(nameof(ToBranch), ref toBranch, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Remarks
        {
            get => remarks;
            set => SetPropertyValue(nameof(Remarks), ref remarks, value);
        }


        [ModelDefault("AllowEdit", "false")]
        public TransferStatus TransferStatus
        {
            get => transferStatus;
            set => SetPropertyValue(nameof(TransferStatus), ref transferStatus, value);
        }


        [Association("Transfer-TransferDetails")]
        public XPCollection<TransferDetail> TransferDetails
        {
            get { return GetCollection<TransferDetail>(nameof(TransferDetails)); }
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