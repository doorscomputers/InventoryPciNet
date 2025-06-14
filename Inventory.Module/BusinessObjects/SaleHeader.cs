using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;




public enum SaleStatus
{
    Encoding = 1,
    Completed = 2

}

namespace Inventory.Module.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Sales Entry Form")]
    [NavigationItem("Sales")]
    [Appearance("EncodingSales", Criteria = "SaleStatus= 1", BackColor = "255, 192, 192", TargetItems = "*", Context = "ListView")] //Turn inventory items 
    //[Appearance("SalesCompleted", Criteria = "SaleStatus=2", TargetItems = "*", Enabled = false)]
    [Appearance("SalesCompleted", Criteria = "SaleStatus=2", TargetItems = "SaleHeader", Enabled = false)]
    [Appearance("SalesCompletedCust", Criteria = "SaleStatus=2", TargetItems = "Customer", Enabled = false)]
    [Appearance("SaleComppleted2", Criteria = "SaleStatus=2", TargetItems = "SaleDetails", Enabled = false)]
    [Appearance("HideSaveStatEncode", AppearanceItemType = "Action", TargetItems = "Save", Enabled = false, Criteria = "SaleStatus=1")]
    [Appearance("HideRefshComplete", AppearanceItemType = "Action", TargetItems = "Refresh", Enabled = false, Criteria = "SaleStatus=2")]
    [Appearance("SalecompletedCP1", Criteria = "SaleStatus=1", TargetItems = "CustomerPayments", Enabled = false)]
    [Appearance("SalecompletedSR1", Criteria = "SaleStatus=1", TargetItems = "SaleRefunds", Enabled =false)]
    [Appearance("SalecompletedCP2", Criteria = "SaleStatus=2", TargetItems = "CustomerPayments", Enabled = true)] 
    [Appearance("SalecompletedSR2", Criteria = "SaleStatus=2", TargetItems = "SaleRefunds", Enabled = true)]
    [Appearance("DisablePOlderThanOneDay", Criteria = "AddDays(Today(), -2) > SoldDate", TargetItems = "SaleRefunds", Enabled = false)]
    public class SaleHeader : XPObject
    {

        public SaleHeader(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            DateTime phTime = GetTimeZoneInPh();
            SoldDate = phTime;//DateTime.Now;
            CreatedDate = phTime; //DateTime.Now;
            SaleStatus = SaleStatus.Encoding;
            //CreatedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);

            // Initialize the Branch to the logged-in user's branch.
            Employee currentUser = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            
            if (currentUser != null)
            {
                this.Branch = currentUser.Branch;
            }
            // Initialize the CreatedBy field to the currently logged-in user.
            CreatedBy = currentUser;
        }

        [Action(Caption = "Sales Completed?", ConfirmationMessage = "Are you sure you encoded correctly the sales transaction?", ImageName = "Attention", AutoCommit = true)]
        public void ActionMethod()
        {
            // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
            if (Customer != null)
            {
                if (this.SaleStatus == SaleStatus.Encoding)
                {
                    this.SaleStatus = SaleStatus.Completed;
                }
            }
        }



        string remarks;
        SaleStatus saleStatus;
        Branch branch;
        double _total;
        double _discount;
        //double _subTotal;
        string _invoiceNumber;
        DateTime _soldDate;
        Customer _customer;


        [RuleRequiredField(DefaultContexts.Save)]
        public Customer Customer
        {
            get => _customer;
            set => SetPropertyValue(nameof(Customer), ref _customer, value);
        }

        //[RuleRequiredField(DefaultContexts.Save)]
        public DateTime SoldDate
        {
            get => _soldDate;
            set => SetPropertyValue(nameof(SoldDate), ref _soldDate, value);
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        [Size(20)]
        public string InvoiceNumber
        {
            get => _invoiceNumber;
            set => SetPropertyValue(nameof(InvoiceNumber), ref _invoiceNumber, value);
        }

        //public double SubTotal
        //{
        //    get
        //    {
        //        try
        //        {
        //            double result = 0;
        //            result = SaleDetails.Where(w => w.QtySold > 0).Sum(s => s.Total);
        //            return result;
        //        }
        //        catch (Exception)
        //        {
        //            return 0;
        //        }
        //    }
        //}


        [VisibleInReports]
        [PersistentAlias("SaleDetails.Sum(Total)")]
        public double SubTotal
        {
            get { return Convert.ToDouble(EvaluateAlias("SubTotal")); }
        }


        [VisibleInReports]
        [PersistentAlias("SaleRefunds.Sum(Amount)")]
        public decimal RefundSubTotal
        {
            get { return Convert.ToDecimal(EvaluateAlias("RefundSubTotal")); }
        }




        public double Discount
        {
            get => _discount;
            set => SetPropertyValue(nameof(Discount), ref _discount, value);
        }


        //[Persistent]
        public double TotalAmount
        {
            get { return (SubTotal - Discount); }

        }


        [VisibleInReports]
        [PersistentAlias("CustomerPayments.Sum(Amount)")]
        public decimal PaymentTotal
        {
            //get => paymentTotal;
            //set => SetPropertyValue(nameof(PaymentTotal), ref paymentTotal, value);
            get { return Convert.ToDecimal(EvaluateAlias("PaymentTotal")); }
        }

        [VisibleInReports]
        [XafDisplayName("Balance")]
        [PersistentAlias("TotalAmount - (Iif(IsNull(CustomerPayments.Sum(Amount)), 0, CustomerPayments.Sum(Amount)) + Iif(IsNull(SaleRefunds.Sum(Amount)), 0, SaleRefunds.Sum(Amount)))")]
        public decimal PaymentBalance
        {
            get { return Convert.ToDecimal(EvaluateAlias("PaymentBalance")); }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Remarks
        {
            get => remarks;
            set => SetPropertyValue(nameof(Remarks), ref remarks, value);
        }



        [Association("SaleHeader-SaleDetails")]
        public XPCollection<SaleDetail> SaleDetails => GetCollection<SaleDetail>(nameof(SaleDetails));

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


        //[Association("Branch-SaleHeaders")]
        [Association("Branch-Sales")]
        [VisibleInDetailView(false)]
        public Branch Branch
        {
            get => branch;
            set => SetPropertyValue(nameof(Branch), ref branch, value);
        }



        //[VisibleInDetailView(false)]
        [ModelDefault("AllowEdit", "False")]
        public SaleStatus SaleStatus
        {
            get => saleStatus;
            set => SetPropertyValue(nameof(SaleStatus), ref saleStatus, value);
        }


        [Association("SaleHeader-SaleRefunds")]
        public XPCollection<SaleRefund> SaleRefunds
        {
            get
            {
                return GetCollection<SaleRefund>(nameof(SaleRefunds));
            }
        }


        [Association("SaleHeader-CustomerPayments")]
        public XPCollection<CustomerPayment> CustomerPayments
        {
            get
            {
                return GetCollection<CustomerPayment>(nameof(CustomerPayments));
            }
        }




    }
}