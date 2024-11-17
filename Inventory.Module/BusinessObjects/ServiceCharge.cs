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
    [NavigationItem("Service Charge")]

    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class ServiceCharge : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public ServiceCharge(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            DateTime phTime = GetTimeZoneInPh();
            DateCollected = phTime;
            Employee currentUser = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);

            if (currentUser != null)
            {
                this.Branch = currentUser.Branch;
            }
            CreatedBy = currentUser;
        }


        double amount;
        string jONumber;
        Technician technician;
        ServiceType serviceType;
        Branch branch;
        DateTime dateCollected;



        [VisibleInDetailView(false)]
        public DateTime DateCollected
        {
            get => dateCollected;
            set => SetPropertyValue(nameof(DateCollected), ref dateCollected, value);
        }

        public ServiceType ServiceType
        {
            get => serviceType;
            set => SetPropertyValue(nameof(ServiceType), ref serviceType, value);
        }


        public Technician Technician
        {
            get => technician;
            set => SetPropertyValue(nameof(Technician), ref technician, value);
        }


        [Size(15)]
        public string JONumber
        {
            get => jONumber;
            set => SetPropertyValue(nameof(JONumber), ref jONumber, value);
        }

        public double Amount
        {
            get => amount;
            set => SetPropertyValue(nameof(Amount), ref amount, value);
        }

        [VisibleInDetailView(false)]
        public Branch Branch
        {
            get => branch;
            set => SetPropertyValue(nameof(Branch), ref branch, value);
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

        static DateTime GetTimeZoneInPh()
        {
            DateTime localDateTime = DateTime.Now;
            TimeZoneInfo phTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"); // Closest equivalent to PH time
            DateTime phTime = TimeZoneInfo.ConvertTime(localDateTime, TimeZoneInfo.Local, phTimeZone);

            return phTime; // Return the Philippine time
        }


    }
}