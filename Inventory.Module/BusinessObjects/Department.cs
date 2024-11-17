using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace Inventory.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Other Records")]
    public class Department : XPObject
    {
        public Department(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }


        Branch _branch;
        string _departmentName;

        [Size(100)]        
        [RuleRequiredField(DefaultContexts.Save), RuleUniqueValue]        
        public string DepartmentName
        {
            get => _departmentName;
            set => SetPropertyValue(nameof(DepartmentName), ref _departmentName, value);
        }

        [Association("Branch-Departments")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Branch Branch
        {
            get => _branch;
            set => SetPropertyValue(nameof(Branch), ref _branch, value);
        }
    }
}