using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;

namespace Inventory.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Other Records")]
    public class Customer : XPObject
    {
        public Customer(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        double _balance;
        string _email;
        string _contactNumber;
        string _address;
        string _name;

        [Size(100)]        
        [RuleRequiredField(DefaultContexts.Save), RuleUniqueValue]        
        public string Name
        {
            get => _name;
            set => SetPropertyValue(nameof(Name), ref _name, value);
        }



        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Address
        {
            get => _address;
            set => SetPropertyValue(nameof(Address), ref _address, value);
        }



        [Size(11)]
        public string ContactNumber
        {
            get => _contactNumber;
            set => SetPropertyValue(nameof(ContactNumber), ref _contactNumber, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Email
        {
            get => _email;
            set => SetPropertyValue(nameof(Email), ref _email, value);
        }


        [VisibleInDetailView(false), VisibleInListView(false)]
        public double Balance
        {
            get => _balance;
            set => SetPropertyValue(nameof(Balance), ref _balance, value);
        }

        protected override void OnSaving()
        {
            LastModifiedDate = DateTime.Now;
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