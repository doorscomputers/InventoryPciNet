﻿using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using Inventory.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class SaleHeaderViewController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public SaleHeaderViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(SaleHeader); // Target SaleHeader instead of BranchStock
            TargetViewType = ViewType.ListView; // This controller activates for ListViews.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            ApplyBranchFilter();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }


        private void ApplyBranchFilter()
        {
            var security = SecuritySystem.Instance;
            var user = security?.User as Employee;
            if (user?.Branch != null && View is ListView listView)
            {
                // Assume SaleHeader has a property 'Branch' that links to the Branch it belongs to
                // Adjust 'Branch.Oid' and 'user.Branch.Oid' as necessary based on your actual property names
                var filterCriteria = CriteriaOperator.Parse("Branch.Oid = ?", user.Branch.Oid);
                listView.CollectionSource.Criteria["FilterByUserSHBranch"] = filterCriteria;
            }
        }
    }
}
