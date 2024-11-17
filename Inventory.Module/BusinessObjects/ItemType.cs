using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;

namespace Inventory.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Other Records")]
    public class ItemType : XPObject
    {
        //public ItemType() : base()
        //{
        //    // This constructor is used when an object is loaded from a persistent storage.
        //    // Do not place any code here.
        //}

        public ItemType(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }


        string typeName;
        

        [Size(100)]
        [RuleRequiredField(DefaultContexts.Save), RuleUniqueValue]               
        public string TypeName
        {
            get => typeName;
            set => SetPropertyValue(nameof(TypeName), ref typeName, value);
        }





    }

}