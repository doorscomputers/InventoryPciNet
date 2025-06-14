using DevExpress.CodeParser;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.Logger.Transport;
using System.ComponentModel;

namespace Inventory.Module.BusinessObjects
{

    [DefaultClassOptions]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Bottom)]
    //[VisibleInReports(true)]
    //[Appearance("HideNew", AppearanceItemType = "Action", TargetItems = "New", Enabled = false)]

    [RuleCriteria("ValidSaleQuantity", DefaultContexts.Save, "QtySold >= 0", SkipNullOrEmptyValues = false, UsedProperties = "QtySold", CustomMessageTemplate = "Quantity must be equal or greater than Zero.")] //Ensure the quantity is not 0
    [RuleCriteria("ValidSaleUnitPrice", DefaultContexts.Save, "Price >= 0", SkipNullOrEmptyValues = false, UsedProperties = "Price", CustomMessageTemplate = "Price must be equal or greater than Zero.")]
    //[Appearance("DisableOneDay", Criteria = "AddDays(Today(), -2) > CreatedDate", TargetItems = "*", Enabled = false)]
    public class SaleDetail : XPObject
    {

        public SaleDetail(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Discount = 0;
            Employee currentUser = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            if (currentUser != null)
            {
                this.Branch = currentUser.Branch;
                this.CreatedBy = currentUser;
            }
        }

        Branch branch;
        string itemName;
        //Branch branch;
        string remark;
        double cost;
        Stock stock;
        int qtySold;
        double _discount;
        double _price;
        BranchStock _branchStock;
        SaleHeader _saleHeader;



        [Association("SaleHeader-SaleDetails")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public SaleHeader SaleHeader
        {
            get => _saleHeader;
            set => SetPropertyValue(nameof(SaleHeader), ref _saleHeader, value);
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
                        this.Cost = value.Cost;
                        this.ItemName = value.ItemName;
                    }
                }
            }

        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ItemName
        {
            get => itemName;
            set => SetPropertyValue(nameof(ItemName), ref itemName, value);
        }



        //public double Price
        //{
        //    get => _price;
        //    set => SetPropertyValue(nameof(Price), ref _price, value);
        //}


        //[VisibleInDetailView(false), VisibleInListView(false)]
        //public double Cost
        //{
        //    get => cost;
        //    set => SetPropertyValue(nameof(Cost), ref cost, value);
        //}


        //public int QtySold
        //{
        //    get => qtySold;
        //    set => SetPropertyValue(nameof(QtySold), ref qtySold, value);
        //}

        //public double SubTotal
        //{
        //    get { return Price * QtySold; }
        //}

        //public double Discount
        //{
        //    get => _discount;
        //    set => SetPropertyValue(nameof(Discount), ref _discount, value);
        //}

        //[XafDisplayName("Amount")]
        //public double Total
        //{
        //    get { return SubTotal - Discount; }
        //}

        public double Price
        {
            get => _price;
            set
            {
                if (SetPropertyValue(nameof(Price), ref _price, value))
                {
                    OnChanged(nameof(SubTotal)); // Notify that SubTotal has changed
                    OnChanged(nameof(Total));    // Notify that Total has changed
                }
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public double Cost
        {
            get => cost;
            set => SetPropertyValue(nameof(Cost), ref cost, value);
        }

        public int QtySold
        {
            get => qtySold;
            set
            {
                if (SetPropertyValue(nameof(QtySold), ref qtySold, value))
                {
                    OnChanged(nameof(SubTotal)); // Notify that SubTotal has changed
                    OnChanged(nameof(Total));    // Notify that Total has changed
                }
            }
        }

        public double SubTotal
        {
            get { return Price * QtySold; }
        }


        [VisibleInDetailView(false), VisibleInListView(false)]
        public double Discount
        {
            get => _discount;
            set
            {
                if (SetPropertyValue(nameof(Discount), ref _discount, value))
                {
                    OnChanged(nameof(Total));  // Notify that Total has changed
                }
            }
        }

        [XafDisplayName("Amount")]
        public double Total
        {
            get { return SubTotal - Discount; }
        }


        [Size(100)]
        public string Remark
        {
            get => remark;
            set => SetPropertyValue(nameof(Remark), ref remark, value);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
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


        [Association("SaleDetail-SaleSerials")]
        public XPCollection<SaleSerial> SaleSerials
        {
            get
            {
                return GetCollection<SaleSerial>(nameof(SaleSerials));
            }
        }

        //[VisibleInDetailView(false)]
        //public Branch Branch
        //{
        //    get => branch;
        //    set => SetPropertyValue(nameof(Branch), ref branch, value);
        //}


        
        [Association("Branch-SaleDetails")]
        [VisibleInDetailView(false)]
        public Branch Branch
        {
            get => branch;
            set => SetPropertyValue(nameof(Branch), ref branch, value);
        }



    }
}