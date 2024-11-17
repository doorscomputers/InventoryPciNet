using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace Inventory.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Admin")]
    public class Branch : XPObject
    {
        public Branch(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }


        string contactNo;
        string address;
        string branchName;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField(DefaultContexts.Save), RuleUniqueValue]        
        public string BranchName
        {
            get => branchName;
            set => SetPropertyValue(nameof(BranchName), ref branchName, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Address
        {
            get => address;
            set => SetPropertyValue(nameof(Address), ref address, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ContactNo
        {
            get => contactNo;
            set => SetPropertyValue(nameof(ContactNo), ref contactNo, value);
        }

        [Association("Branch-Departments")]
        public XPCollection<Department> Departments => GetCollection<Department>(nameof(Departments));

        [Association("Branch-Employees")]
        public XPCollection<Employee> Employees => GetCollection<Employee>(nameof(Employees));


        [Association("Branch-BranchStocks")]
        public XPCollection<BranchStock> BranchStocks => GetCollection<BranchStock>(nameof(BranchStocks));


        [Association("Branch-Sales")]
        public XPCollection<SaleHeader> Sales => GetCollection<SaleHeader>(nameof(Sales));


        [Association("Branch-TransfersFrom")]
        public XPCollection<Transfer> Transfers => GetCollection<Transfer>(nameof(Transfers));


        [Association("Branch-Deliveries")]
        public XPCollection<DeliveryHeader> Deliveries => GetCollection<DeliveryHeader>(nameof(Deliveries));

        [Association("Branch-Bos")]
        public XPCollection<Bo> Bos => GetCollection<Bo>(nameof(Bos));


        [Association("Branch-Warranties")]
        public XPCollection<Warranty> Warranties => GetCollection<Warranty>(nameof(Warranties));


        [Association("Branch-SaleDetails")]
        public XPCollection<SaleDetail> SaleDetails => GetCollection<SaleDetail>(nameof(SaleDetails));
            



    }
}