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

namespace Inventory.Module.BusinessObjects
{
    //[DefaultClassOptions]
    //[Appearance("Refundcompleted", Criteria = "SaleHeader!=null", TargetItems = "SaleRefund", Enabled = false)]
    //[RuleCriteria("InValidAmount", DefaultContexts.Save, "Amount <= TotalPurchase", SkipNullOrEmptyValues = false, UsedProperties = "Amount", CustomMessageTemplate = "Amount must be less than the total purchase.")]
    [Appearance("DisableIfOlderThanFiveDays",Criteria = "AddDays(Today(), -2) > RefundDate",TargetItems = "*",Enabled = false)]
    //[Appearance("DisNew", AppearanceItemType = "Action", TargetItems = "New", Enabled = false, Criteria = "AddDays(Today(), -5) > RefundDate")]
    public class SaleRefund : XPObject
    {
        public SaleRefund(Session session)
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
                this.CreatedBy = currentUser;
            }
            DateTime phTime = GetTimeZoneInPh();
            RefundDate = phTime;//DateTime.Now;
        }


        DateTime refundDate;
        double totalPurchase;
        string remark;
        double quantity;
        Stock stock;
        double price;
        Branch branch;
        int saleRefNo;
        SaleHeader saleHeader;

        [Association("SaleHeader-SaleRefunds")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public SaleHeader SaleHeader
        {
            get { return saleHeader; }
            set
            {
                bool modified = SetPropertyValue(nameof(SaleHeader), ref saleHeader, value);
                if (!IsLoading && !IsSaving && value != null && modified)
                {
                    this.SaleRefNo = value.Oid;
                    this.TotalPurchase = value.TotalAmount;
                }
            }
        }


        [VisibleInListView(false), VisibleInDetailView(false)]
        public int SaleRefNo
        {
            get => saleRefNo;
            set => SetPropertyValue(nameof(SaleRefNo), ref saleRefNo, value);
        }

        [VisibleInDetailView(false)]
        public DateTime RefundDate
        {
            get => refundDate;
            set => SetPropertyValue(nameof(RefundDate), ref refundDate, value);
        }




        [VisibleInListView(false), VisibleInDetailView(false)]
        public Branch Branch
        {
            get => branch;
            set => SetPropertyValue(nameof(Branch), ref branch, value);
        }


        [RuleRequiredField(DefaultContexts.Save)]
        public Stock Stock
        {
            get => stock;
            //set => SetPropertyValue(nameof(Stock), ref stock, value);
            set
            {
                SetPropertyValue(nameof(Stock), ref stock, value);
                if (!IsLoading && !IsSaving && !IsDeleted)
                {
                    if (Stock != null)
                    {
                        this.Price = value.Price;
                    }
                }
            }
        }
        //public double Price
        //{
        //    get => price;
        //    set => SetPropertyValue(nameof(Price), ref price, value);
        //}

        //public double Quantity
        //{
        //    get => quantity;
        //    set => SetPropertyValue(nameof(Quantity), ref quantity, value);
        //}

        //public double Amount
        //{
        //    get { return Price * Quantity; }
        //}

        public double Price
        {
            get => price;
            set
            {
                if (SetPropertyValue(nameof(Price), ref price, value))
                {
                    OnChanged(nameof(Amount)); // Notify that Amount has changed
                }
            }
        }

        public double Quantity
        {
            get => quantity;
            set
            {
                if (SetPropertyValue(nameof(Quantity), ref quantity, value))
                {
                    OnChanged(nameof(Amount)); // Notify that Amount has changed
                }
            }
        }

        public double Amount
        {
            get { return Price * Quantity; } // Calculate Amount based on Price and Quantity
        }



        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Remark
        {
            get => remark;
            set => SetPropertyValue(nameof(Remark), ref remark, value);
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public double TotalPurchase
        {
            get => totalPurchase;
            set => SetPropertyValue(nameof(TotalPurchase), ref totalPurchase, value);
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

    }
}