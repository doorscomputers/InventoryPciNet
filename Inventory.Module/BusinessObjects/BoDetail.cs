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
    //[Appearance("HideNew", AppearanceItemType = "Action", TargetItems = "New", Enabled = false)]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Bottom)]
    [RuleCriteria("ValidBoQuantity", DefaultContexts.Save, "Quantity > 0", SkipNullOrEmptyValues = false, UsedProperties = "Quantity", CustomMessageTemplate = "Quantity must be greater than Zero.")]
    public class BoDetail : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public BoDetail(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).

            Employee currentUser = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);

            if (currentUser != null)
            {
                this.Branch = currentUser.Branch;
                this.CreatedBy = currentUser;
            }
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue(nameof(PersistentProperty), ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}



        string itemName;
        Branch branch;
        int quantity;
        string remarks;
        Stock stock;
        Bo bo;


        [VisibleInListView(false), VisibleInDetailView(false)]
        [Association("Bo-BoDetails")]
        public Bo Bo
        {
            get => bo;
            set => SetPropertyValue(nameof(Bo), ref bo, value);
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
        public string Remarks
        {
            get => remarks;
            set => SetPropertyValue(nameof(Remarks), ref remarks, value);
        }


        [Association("BoDetail-BoSerials")]
        public XPCollection<BoSerial> BoSerials
        {
            get
            {
                return GetCollection<BoSerial>(nameof(BoSerials));
            }
        }

        [VisibleInListView(false),VisibleInDetailView(false)]
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

    }
}