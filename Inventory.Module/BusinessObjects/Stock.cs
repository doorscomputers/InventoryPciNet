using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;

namespace Inventory.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Admin")]
    [XafDefaultProperty("ItemCode")]
    public class Stock : XPObject
    {
        public Stock(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            CreatedDate = GetCurrentTime();
            CreatedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            Active = true;
            AddStockToAllBranches();
        }


        double latestCost;
        double lastCost;
        double lastQtyDelivered;
        string lastSupplier;
        DateTime lastDelivery;
        Supplier supplier;
        ItemSize itemSize;
        ItemType itemType;
        Brand brand;
        Category category;
        bool active;
        double price;
        double _cost;
        string _itemCode;
        string _stockDescription;
        string _itemName;


        [Size(25)]
        [RuleRequiredField(DefaultContexts.Save), RuleUniqueValue]
        public string ItemCode
        {
            get => _itemCode;
            set => SetPropertyValue(nameof(ItemCode), ref _itemCode, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField(DefaultContexts.Save), RuleUniqueValue]
        public string ItemName
        {
            get => _itemName;
            set => SetPropertyValue(nameof(ItemName), ref _itemName, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string StockDescription
        {
            get => _stockDescription;
            set => SetPropertyValue(nameof(StockDescription), ref _stockDescription, value);
        }

        [Association("Stock-BranchStocks")]
        public XPCollection<BranchStock> BranchStocks => GetCollection<BranchStock>(nameof(BranchStocks));

        public double Cost
        {
            get => _cost;
            set => SetPropertyValue(nameof(Cost), ref _cost, value);
        }


        public double Price
        {
            get => price;
            set => SetPropertyValue(nameof(Price), ref price, value);
        }


        public bool Active
        {
            get => active;
            set => SetPropertyValue(nameof(Active), ref active, value);
        }



        public Supplier Supplier
        {
            get => supplier;
            set => SetPropertyValue(nameof(Supplier), ref supplier, value);
        }



        public Category Category
        {
            get => category;
            set => SetPropertyValue(nameof(Category), ref category, value);
        }


        public Brand Brand
        {
            get => brand;
            set => SetPropertyValue(nameof(Brand), ref brand, value);
        }


        public ItemType ItemType
        {
            get => itemType;
            set => SetPropertyValue(nameof(ItemType), ref itemType, value);
        }


        public ItemSize ItemSize
        {
            get => itemSize;
            set => SetPropertyValue(nameof(ItemSize), ref itemSize, value);
        }


        [VisibleInDetailView(false)]
        public DateTime LastDelivery
        {
            get => lastDelivery;
            set => SetPropertyValue(nameof(LastDelivery), ref lastDelivery, value);
        }


        [VisibleInDetailView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string LastSupplier
        {
            get => lastSupplier;
            set => SetPropertyValue(nameof(LastSupplier), ref lastSupplier, value);
        }

        [VisibleInDetailView(false)]
        public double LastQtyDelivered
        {
            get => lastQtyDelivered;
            set => SetPropertyValue(nameof(LastQtyDelivered), ref lastQtyDelivered, value);
        }

        [VisibleInDetailView(false)]
        public double LastCost
        {
            get => lastCost;
            set => SetPropertyValue(nameof(LastCost), ref lastCost, value);
        }

        [VisibleInDetailView(false)]
        public double LatestCost
        {
            get => latestCost;
            set => SetPropertyValue(nameof(LatestCost), ref latestCost, value);
        }


        private void AddStockToAllBranches()
        {
            // Assuming you have a method to get all branches. It can be a direct data access or a service method.
            var branches = Session.GetObjects(Session.GetClassInfo<Branch>(), null, null, 0, false, true);
            foreach (Branch branch in branches)
            {
                var branchStock = new BranchStock(Session)
                {
                    Stock = this,
                    Branch = branch,
                    // Set initial quantity to 0 or any default value you prefer
                    Quantity = 0,
                    Active = true
                };
                // No need to save each branchStock individually; the session.Save() will handle it.
            }
        }

        private bool costChanged = false;
        private bool priceChanged = false;
        private bool activeChanged = false;

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            switch (propertyName)
            {
                case nameof(Cost):
                    costChanged = true;
                    break;
                case nameof(Price):
                    priceChanged = true;
                    break;
                case nameof(Active):
                    activeChanged = true;
                    break;
            }
        }

        protected override void OnSaving()
        {
            if (costChanged || priceChanged)
            {
                foreach (var branchStock in BranchStocks)
                {
                    if (costChanged) branchStock.Cost = this.Cost;
                    if (priceChanged) branchStock.Price = this.Price;
                    if (activeChanged) branchStock.Active = this.Active;
                    // No need for branchStock.Save() as XPO handles this in the transaction.
                }
            }
            // Reset flags
            costChanged = false;
            priceChanged = false;
            activeChanged = false;
            LastModifiedDate = GetCurrentTime();
            LastModifiedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);

            base.OnSaving();
        }


        protected override void OnDeleting()
        {
            foreach (var branchStock in BranchStocks.ToList())
            {
                branchStock.Delete();
                // Deletion will be part of the session's transaction
            }
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

    }
}