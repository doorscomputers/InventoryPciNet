using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;

namespace Inventory.Module.BusinessObjects
{
    //[XafDisplayName("Sales Entry Form")]
    [DefaultClassOptions]
    [NavigationItem("Other Records")]
    public class Brand : XPObject
    {
        

        public Brand(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }



        string brandName;

        [Size(100)]
        [RuleRequiredField(DefaultContexts.Save), RuleUniqueValue]        
        public string BrandName
        {
            get => brandName;
            set => SetPropertyValue(nameof(BrandName), ref brandName, value);
        }




    }

}