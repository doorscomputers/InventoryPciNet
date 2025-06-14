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
    public class BranchStockPivot : XPObject
    { 
        public BranchStockPivot(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        bool active;
        int lastQtyDelivered;
        DateTime lastDeliveryDate;
        string brand;
        string category;
        string supplier;
        string tuguegarao;
        decimal totalPrice;
        decimal totalCost;
        decimal totalStocks;
        string bambang;
        string mainStore;
        string warehouse;
        decimal price;
        decimal cost;
        string itemName;
        string itemCode;

        [Index(0), Size(SizeAttribute.DefaultStringMappingFieldSize)]

        public string ItemCode
        {
            get => itemCode;
            set => SetPropertyValue(nameof(ItemCode), ref itemCode, value);
        }


        [Index(1), Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ItemName
        {
            get => itemName;
            set => SetPropertyValue(nameof(ItemName), ref itemName, value);
        }



        [Index(2), Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Supplier
        {
            get => supplier;
            set => SetPropertyValue(nameof(Supplier), ref supplier, value);
        }


        [Index(3), Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Category
        {
            get => category;
            set => SetPropertyValue(nameof(Category), ref category, value);
        }


        [Index(4), Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Brand
        {
            get => brand;
            set => SetPropertyValue(nameof(Brand), ref brand, value);
        }

        [Index(5)]
        public DateTime LastDeliveryDate
        {
            get => lastDeliveryDate;
            set => SetPropertyValue(nameof(LastDeliveryDate), ref lastDeliveryDate, value);
        }


        [Index(6)]
        public int LastQtyDelivered
        {
            get => lastQtyDelivered;
            set => SetPropertyValue(nameof(LastQtyDelivered), ref lastQtyDelivered, value);
        }



        [Index(7)]
        [ModelDefault("DisplayFormat", "₱{0:N2}")]
        public decimal Cost
        {
            get => cost;
            set => SetPropertyValue(nameof(Cost), ref cost, value);
        }


        [Index(8)]
        [ModelDefault("DisplayFormat", "₱{0:N2}")]
        public decimal Price
        {
            get => price;
            set => SetPropertyValue(nameof(Price), ref price, value);
        }



        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [Index(9)]
        public string Warehouse
        {
            get => warehouse;
            set => SetPropertyValue(nameof(Warehouse), ref warehouse, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [Index(10)]
        public string MainStore
        {
            get => mainStore;
            set => SetPropertyValue(nameof(MainStore), ref mainStore, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [Index(11)]
        public string Bambang
        {
            get => bambang;
            set => SetPropertyValue(nameof(Bambang), ref bambang, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [Index(12)]
        public string Tuguegarao
        {
            get => tuguegarao;
            set => SetPropertyValue(nameof(Tuguegarao), ref tuguegarao, value);
        }


        [Index(13)]
        public decimal TotalStocks
        {
            get => totalStocks;
            set => SetPropertyValue(nameof(TotalStocks), ref totalStocks, value);
        }


        [Index(14)]
        [ModelDefault("DisplayFormat", "₱{0:N2}")]
        public decimal TotalCost
        {
            get => totalCost;
            set => SetPropertyValue(nameof(TotalCost), ref totalCost, value);
        }


        [Index(15)]
        [ModelDefault("DisplayFormat", "₱{0:N2}")]
        public decimal TotalPrice
        {
            get => totalPrice;
            set => SetPropertyValue(nameof(TotalPrice), ref totalPrice, value);
        }

        
        public bool Active
        {
            get => active;
            set => SetPropertyValue(nameof(Active), ref active, value);
        }



    }
}