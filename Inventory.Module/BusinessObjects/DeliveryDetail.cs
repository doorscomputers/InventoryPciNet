

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;

namespace Inventory.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[VisibleInReports(true)]
    //[Appearance("HideDetailNew", AppearanceItemType = "Action", TargetItems = "New", Enabled = false)]
    [RuleCriteria("ValidDeliveryQuantity", DefaultContexts.Save, "QtyDelivered >= 0", SkipNullOrEmptyValues = false, UsedProperties = "QtyDelivered", CustomMessageTemplate = "Quantity must be greater than Zero.")] //Ensure the quantity is not 0
    [RuleCriteria("ValidDeliveryCost", DefaultContexts.Save, "Cost > 0", SkipNullOrEmptyValues = false, UsedProperties = "Cost", CustomMessageTemplate = "Cost must be greater than Zero.")]
    public class DeliveryDetail : XPObject
    {
        public DeliveryDetail(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            //CreatedDate = GetCurrentTime();
            //CreatedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            // Initialize the Branch to the logged-in user's branch.
            Discount = 0;
            //Employee currentUser = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            //if (currentUser != null)
            //{
            //    //CreatedBy = currentUser;
            //    CreatedBy = currentUser;
            //}

            Employee currentUser = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);

            if (currentUser != null)
            {
                this.Branch = currentUser.Branch;
                this.CreatedBy = currentUser;
            }



        }

        string itemName;
        Branch branch;
        string remark;
        int qtyDelivered;
        Stock stock;
        double _discount;
        double _cost;
        DeliveryHeader _deliveryHeader;
        double _amount;



        [Association("DeliveryHeader-DeliveryDetails")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public DeliveryHeader DeliveryHeader
        {
            get => _deliveryHeader;
            set => SetPropertyValue(nameof(DeliveryHeader), ref _deliveryHeader, value);
        }

        //public BranchStock BranchStock
        //{
        //    get => _branchStock;
        //    set => SetPropertyValue(nameof(BranchStock), ref _branchStock, value);
        //}


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



        public double Cost
        {
            get => _cost;
            //set => SetPropertyValue(nameof(Cost), ref _cost, value);
            set
            {
                if (SetPropertyValue(nameof(Cost), ref _cost, value))
                    UpdateAmount();
            }

        }


        private void UpdateAmount()
        {
            Amount = (Cost * QtyDelivered) - Discount;

        }

        //public int QtyDelivered
        //{
        //    get => qtyDelivered;
        //    set => SetPropertyValue(nameof(QtyDelivered), ref qtyDelivered, value);
        //}
        public int QtyDelivered
        {
            get => qtyDelivered;
            set
            {
                if (SetPropertyValue(nameof(QtyDelivered), ref qtyDelivered, value))
                    UpdateAmount();
            }
        }




        public double SubTotal
        {
            get { return (Cost * Convert.ToDouble(QtyDelivered)); }
        }

        public double Discount
        {
            get => _discount;
            set
            {
                if (SetPropertyValue(nameof(Discount), ref _discount, value))
                    UpdateAmount();
            }
        }



        public double Amount
        {
            get => _amount;
            protected set => SetPropertyValue(nameof(Amount), ref _amount, value);
        }


        //public double Amount
        //{
        //    get { return (Cost * Convert.ToDouble(QtyDelivered)) - Discount; }
        //}



        [Size(100)]
        public string Remark
        {
            get => remark;
            set => SetPropertyValue(nameof(Remark), ref remark, value);
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


        [Association("DeliveryDetail-DeliverySerials")]
        public XPCollection<DeliverySerial> DeliverySerials
        {
            get
            {
                return GetCollection<DeliverySerial>(nameof(DeliverySerials));
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public Branch Branch
        {
            get => branch;
            set => SetPropertyValue(nameof(Branch), ref branch, value);
        }
    }
}