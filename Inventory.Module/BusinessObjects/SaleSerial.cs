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
    //[VisibleInReports(true)]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Bottom)]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class SaleSerial : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public SaleSerial(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            Employee currentUser = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            CreatedBy = currentUser;
        }




        SaleDetail saleDetail;
        string remark;
        string serialNumber;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string SerialNumber
        {
            get => serialNumber;
            set => SetPropertyValue(nameof(SerialNumber), ref serialNumber, value);
        }



        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Remark
        {
            get => remark;
            set => SetPropertyValue(nameof(Remark), ref remark, value);
        }

        
        [Association("SaleDetail-SaleSerials")]
        [VisibleInListView(false),VisibleInDetailView(false)]
        public SaleDetail SaleDetail
        {
            get => saleDetail;
            set => SetPropertyValue(nameof(SaleDetail), ref saleDetail, value);
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
        protected override void OnSaving()
        {
            LastModifiedDate = GetCurrentTime();
            LastModifiedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            base.OnSaving();
        }



        protected override void OnDeleting()
        {
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


        protected DateTime GetCurrentTime()
        {
            DateTime serverTime = DateTime.Now;
            DateTime _localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(serverTime, TimeZoneInfo.Local.Id, "Singapore Standard Time");
            return _localTime;
        }


    }
}