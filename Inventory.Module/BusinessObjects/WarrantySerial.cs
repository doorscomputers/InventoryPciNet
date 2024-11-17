using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
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
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class WarrantySerial : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public WarrantySerial(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }



        string remarks;
        string serialNumber;
        WarrantyDetail warrantyDetail;

        [Association("WarrantyDetail-WarrantySerials")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public WarrantyDetail WarrantyDetail
        {
            get => warrantyDetail;
            set => SetPropertyValue(nameof(WarrantyDetail), ref warrantyDetail, value);
        }



        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string SerialNumber
        {
            get => serialNumber;
            set => SetPropertyValue(nameof(SerialNumber), ref serialNumber, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Remarks
        {
            get => remarks;
            set => SetPropertyValue(nameof(Remarks), ref remarks, value);
        }
    }

}