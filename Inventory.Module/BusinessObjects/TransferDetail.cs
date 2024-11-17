using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;

namespace Inventory.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Bottom)]
    [VisibleInReports(true)]
    [RuleCriteria("ValidTrasnferQuantity", DefaultContexts.Save, "Quantity >= 0", SkipNullOrEmptyValues = false, UsedProperties = "Quantity", CustomMessageTemplate = "Quantity must be equal or greater than Zero.")] //Ensure the quantity is not 0
    public class TransferDetail : XPObject
    {

        public TransferDetail(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            //Employee currentUser = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            //if (currentUser != null)
            //{
            //    //CreatedBy = currentUser;
            //    CreatedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            //}

            Employee currentUser = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);

            if (currentUser != null)
            {
                this.Branch = currentUser.Branch;
                this.CreatedBy = currentUser;
            }

        }


        string itemName;
        Branch branch;
        string remark;
        private Transfer transfer;
        private Stock stock;
        private int quantity;


        [Association("Transfer-TransferDetails")]
        [VisibleInDetailView(false)]
        public Transfer Transfer
        {
            get => transfer;
            set => SetPropertyValue(nameof(Transfer), ref transfer, value);
        }

        [RuleRequiredField(DefaultContexts.Save)]
        public Stock Stock
        {
            get => stock;
            //set => SetPropertyValue(nameof(Stock), ref stock, value);
            set
            {
                SetPropertyValue(nameof(Stock), ref stock, value);
                if (!IsLoading && !IsSaving && !IsDeleted)
                {
                    if (Stock != null)
                    {
                        this.ItemName = value.ItemName;
                    }
                }
            }
        }

        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ItemName
        {
            get => itemName;
            set => SetPropertyValue(nameof(ItemName), ref itemName, value);
        }


        public int Quantity
        {
            get => quantity;
            set => SetPropertyValue(nameof(Quantity), ref quantity, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Remark
        {
            get => remark;
            set => SetPropertyValue(nameof(Remark), ref remark, value);
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

        [VisibleInListView(false), VisibleInDetailView(false)]
        public Branch Branch
        {
            get => branch;
            set => SetPropertyValue(nameof(Branch), ref branch, value);
        }

    }

}