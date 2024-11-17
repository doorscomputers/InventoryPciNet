using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace Inventory.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class BranchStock : XPObject
    {
        public BranchStock(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Active = true;
        }

        bool active;
        double _cost;
        double _price;
        int _quantity;
        Stock _stock;
        Branch _branch;

        [Association("Branch-BranchStocks")]
        public Branch Branch
        {
            get => _branch;
            set => SetPropertyValue(nameof(Branch), ref _branch, value);
        }

        [Association("Stock-BranchStocks")]
        public Stock Stock
        {
            get => _stock;
            set => SetPropertyValue(nameof(Stock), ref _stock, value);
        }

        public int Quantity
        {
            get => _quantity;
            set => SetPropertyValue(nameof(Quantity), ref _quantity, value);
        }

        public double Cost
        {
            get => _cost;
            set => SetPropertyValue(nameof(Cost), ref _cost, value);
        }

        public double Price
        {
            get => _price;
            set => SetPropertyValue(nameof(Price), ref _price, value);
        }

        
        public bool Active
        {
            get => active;
            set => SetPropertyValue(nameof(Active), ref active, value);
        }




        
    }
}