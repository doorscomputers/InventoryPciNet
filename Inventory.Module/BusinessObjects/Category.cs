using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;

namespace Inventory.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Other Records")]
    public class Category : XPObject
    {
        

        public Category(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }



        string categoryName;

        [Size(100)]
        [RuleRequiredField(DefaultContexts.Save), RuleUniqueValue]
        public string CategoryName
        {
            get => categoryName;
            set => SetPropertyValue(nameof(CategoryName), ref categoryName, value);
        }



    }

}