using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.ReportsV2;
using Microsoft.Extensions.DependencyInjection;
using Inventory.Module.BusinessObjects;

namespace Inventory.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class MyPrintReportController : ObjectViewController<ListView, SaleHeader>
    {
        public MyPrintReportController()
        {
            SimpleAction SalesInvoiceAction = new SimpleAction(this, "Print selected Record", PredefinedCategory.RecordEdit);
            SalesInvoiceAction.Execute += SalesInvoiceAction_Execute;
        }

        private void SalesInvoiceAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ReportServiceController controller = Frame.GetController<ReportServiceController>();
            if (controller != null)
            {

                var reportStorage = Application.ServiceProvider.GetRequiredService<IReportStorage>();
                using IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(ReportDataV2));
                IReportDataV2 reportData = objectSpace.FirstOrDefault<ReportDataV2>(data => data.DisplayName == "SalesReceiptTemplate");
                string handle = reportStorage.GetReportContainerHandle(reportData);
                CriteriaOperator objectsCriteria = ((BaseObjectSpace)objectSpace).GetObjectsCriteria(View.ObjectTypeInfo, e.SelectedObjects);
                controller.ShowPreview(handle, objectsCriteria);
                //controller.ShowPreview(handle);
              
            };
        }
    }
}
