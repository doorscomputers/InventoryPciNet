using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.PivotGrid.PivotTable;
using DevExpress.Xpo;
using System.ComponentModel;


public enum DeliveryStatus
{
    Encoding = 1,
    Completed = 2
    
}


namespace Inventory.Module.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Delivery Entry Form")]
    [Appearance("Encoding", Criteria = "DeliveryStatus= 1", BackColor = "255, 192, 192", TargetItems = "*", Context = "ListView")] //Turn inventory items 
    [Appearance("DlvryCompleted", Criteria = "DeliveryStatus=2", TargetItems = "*", Enabled = false)] 
    [Appearance("DlvryComppleted2", Criteria = "DeliveryStatus=2", TargetItems = "DeliveryDetails", Enabled = false)]
    //[Appearance("DlvryCancelled1", Criteria = "DeliveryStatus=2", TargetItems = "DeliveryHeader", Context = "Any",Enabled = false)] 
    //[Appearance("HideDeleteIfSOSaved", AppearanceItemType = "Action", TargetItems = "Delete", Enabled = false, Criteria = "DeliveryStatus=2")]
    [Appearance("HideSaveIfStatusEncode", AppearanceItemType = "Action", TargetItems = "Save", Enabled = false, Criteria = "DeliveryStatus=1")]
    [Appearance("HideRefreshCompleted", AppearanceItemType = "Action", TargetItems = "Refresh", Enabled = false, Criteria = "DeliveryStatus=2")]
    [Appearance("DateDeliveredDisable", Criteria = "AddDays(Today(), -2) > DateDelivered", TargetItems = "*", Enabled = false)]
    public class DeliveryHeader : XPObject
    {
        public DeliveryHeader(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
            DateTime phTime = GetTimeZoneInPh();
            DateDelivered = phTime;
            DeliveryStatus = DeliveryStatus.Encoding;
            // Initialize the Branch to the logged-in user's branch.
            Employee currentUser = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
           
            if (currentUser != null)
            {
                this.Branch = currentUser.Branch;
            }


            // Initialize the CreatedBy field to the currently logged-in user.
            //CreatedBy = currentUser;
            CreatedBy = currentUser;
        }

        //[Action(Caption = "Delivery Completed?", ConfirmationMessage = "Are you sure you checked and counted the items received correctly and encoding is already complete?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod()
        //{
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).

        //    if (InvoiceNumber != null & Supplier != null)
        //    {
        //        if (this.DeliveryStatus == DeliveryStatus.Encoding)
        //        {
        //            this.DeliveryStatus = DeliveryStatus.Completed;
        //        }
        //    }
        //}

        [Action(Caption = "Delivery Completed?", 
        ConfirmationMessage = "Are you sure you checked and counted the items received correctly and encoding is already complete?", 
        ImageName = "Attention", 
        AutoCommit = true)]
        
        public void ActionMethod()
{
    // Trigger a custom business logic for the current record in the UI.

    // Guard clauses to make the code more readable.
    if (InvoiceNumber == null || Supplier == null)
    {
        return;
    }

    if (this.DeliveryStatus == DeliveryStatus.Encoding)
    {
        this.DeliveryStatus = DeliveryStatus.Completed;
    }
}




        string remarks;
        DeliveryStatus deliveryStatus;
        Branch branch;
        string _invoiceNumber;
        double _discount;
        Supplier _supplier;
        DateTime _dateDelivered;




        [Association("Branch-Deliveries")]
        [VisibleInDetailView(false)]
        public Branch Branch
        {
            get => branch;
            set => SetPropertyValue(nameof(Branch), ref branch, value);
        }


        //[RuleRequiredField(DefaultContexts.Save)]
        public DateTime DateDelivered
        {
            get => _dateDelivered;
            set => SetPropertyValue(nameof(DateDelivered), ref _dateDelivered, value);
        }

        [Size(15)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string InvoiceNumber
        {
            get => _invoiceNumber;
            set => SetPropertyValue(nameof(InvoiceNumber), ref _invoiceNumber, value);
        }

        [RuleRequiredField(DefaultContexts.Save)]
        public Supplier Supplier
        {
            get => _supplier;
            set => SetPropertyValue(nameof(Supplier), ref _supplier, value);
        }

        //public double SubTotal
        //{
        //    get
        //    {
        //        try
        //        {
        //            double result = 0;
        //            result = DeliveryDetails.Where(w => w.QtyDelivered > 0).Sum(s => s.Amount);
        //            return result;
        //        }
        //        catch (Exception)
        //        {
        //            return 0;
        //        }
        //    }
        //}

        [VisibleInReports]
        [PersistentAlias("DeliveryDetails.Sum(Amount)")]
        public double SubTotal
        {
            //get => subTotal;
            //set => SetPropertyValue(nameof(SubTotal), ref subTotal, value);
            get { return Convert.ToDouble(EvaluateAlias("SubTotal")); }
        }

        public double Discount
        {
            get => _discount;
            set => SetPropertyValue(nameof(Discount), ref _discount, value);
        }


        //[Persistent]
        public double DeliveryTotal
        {
            get { return (SubTotal - Discount); }
        }



        //[Association("DeliveryHeader-DeliveryDetails")]
        //public XPCollection<DeliveryDetail> DeliveryDetails => GetCollection<DeliveryDetail>(nameof(DeliveryDetails));


        [Association("DeliveryHeader-DeliveryDetails")]
        public XPCollection<DeliveryDetail> DeliveryDetails
        {
            get
            {
                return GetCollection<DeliveryDetail>(nameof(DeliveryDetails));
            }
        }






        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Remarks
        {
            get => remarks;
            set => SetPropertyValue(nameof(Remarks), ref remarks, value);
        }

        protected override void OnSaving()
        {
            LastModifiedDate = GetCurrentTime();
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


        [VisibleInDetailView(false)]
        public DeliveryStatus DeliveryStatus
        {
            get => deliveryStatus;
            set => SetPropertyValue(nameof(DeliveryStatus), ref deliveryStatus, value);
        }




    }
}