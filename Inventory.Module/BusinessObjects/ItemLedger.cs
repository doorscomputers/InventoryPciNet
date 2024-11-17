using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace Inventory.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Admin")]
    public class ItemLedger : XPObject
    {
        public ItemLedger() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public ItemLedger(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }


        int currentEditedQty;
        int previousEditedQty;
        int transactionOid;
        string transactionType;
        string remarks;
        int qtyChange;
        string currentQty;
        int previousQty;
        string itemName;
        int stockId;
        string invoiceNumber;
        string branchName;
        int branchId;
        string userID;
        DateTime transactionDate;




        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string TransactionType
        {
            get => transactionType;
            set => SetPropertyValue(nameof(TransactionType), ref transactionType, value);
        }


        public DateTime TransactionDate
        {
            get => transactionDate;
            set => SetPropertyValue(nameof(TransactionDate), ref transactionDate, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string UserID
        {
            get => userID;
            set => SetPropertyValue(nameof(UserID), ref userID, value);
        }


        public int BranchId
        {
            get => branchId;
            set => SetPropertyValue(nameof(BranchId), ref branchId, value);
        }



        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string BranchName
        {
            get => branchName;
            set => SetPropertyValue(nameof(BranchName), ref branchName, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string InvoiceNumber
        {
            get => invoiceNumber;
            set => SetPropertyValue(nameof(InvoiceNumber), ref invoiceNumber, value);
        }



        public int TransactionOid
        {
            get => transactionOid;
            set => SetPropertyValue(nameof(TransactionOid), ref transactionOid, value);
        }




        public int StockId
        {
            get => stockId;
            set => SetPropertyValue(nameof(StockId), ref stockId, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ItemName
        {
            get => itemName;
            set => SetPropertyValue(nameof(ItemName), ref itemName, value);
        }


        public int PreviousQty
        {
            get => previousQty;
            set => SetPropertyValue(nameof(PreviousQty), ref previousQty, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string CurrentQty
        {
            get => currentQty;
            set => SetPropertyValue(nameof(CurrentQty), ref currentQty, value);
        }



        public int QtyChange
        {
            get => qtyChange;
            set => SetPropertyValue(nameof(QtyChange), ref qtyChange, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Remarks
        {
            get => remarks;
            set => SetPropertyValue(nameof(Remarks), ref remarks, value);
        }



        public int PreviousEditedQty
        {
            get => previousEditedQty;
            set => SetPropertyValue(nameof(PreviousEditedQty), ref previousEditedQty, value);
        }

        
        public int CurrentEditedQty
        {
            get => currentEditedQty;
            set => SetPropertyValue(nameof(CurrentEditedQty), ref currentEditedQty, value);
        }





    }

}