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
using DevExpress.Persistent.Validation;
using Inventory.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Xpo;

namespace Inventory.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ViewController1 : ViewController
    {
        private readonly string connectionString;
        private List<BranchStockPivot> branchStockPivots; // Local collection to hold the data

        // Simple Action for fetching data
        private SimpleAction fetchBranchStockPivotAction;
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public ViewController1()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(BranchStockPivot); // Target SaleHeader instead of BranchStock
            TargetViewType = ViewType.ListView;
            // Read connection string from configuration
            //Offline
            //connectionString = "Integrated Security=SSPI;Pooling=false;Data Source=DOORSCOMPUTERS\\SQLEXPRESS;Initial Catalog=db_aa6a53_pcnet";
            
            //online
            connectionString = "Data Source=sql9001.site4now.net;Initial Catalog=db_aa6a53_pcnet;User Id=db_aa6a53_pcnet_admin;Password=Ssss9999;";

            // Initialize the action
            fetchBranchStockPivotAction = new SimpleAction(this, "FetchBranchStockPivot", PredefinedCategory.View)
            {
                Caption = "Fetch Branch Stock Pivot",
                ImageName = "Action_Refresh",
                TargetObjectType = typeof(BranchStockPivot),
                TargetViewType = ViewType.ListView,
                SelectionDependencyType = SelectionDependencyType.Independent
            };
            fetchBranchStockPivotAction.Execute += FetchBranchStockPivotAction_Execute;

            // Initialize the local collection
            branchStockPivots = new List<BranchStockPivot>();
        }
        private void FetchBranchStockPivotAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            branchStockPivots.Clear();
            branchStockPivots.AddRange(GetBranchStockPivotData());

            // Notify the ObjectSpace that the data has been updated
            if (View.ObjectSpace is NonPersistentObjectSpace nonPersistentObjectSpace)
            {
                //nonPersistentObjectSpace.Reloaded += NonPersistentObjectSpace_Reloaded;
                nonPersistentObjectSpace.Refresh();
                View.RefreshDataSource(); // Refresh the view to display updated data
            }
        }

        private void NonPersistentObjectSpace_Reloaded(object sender, EventArgs e)
        {
            if (sender is NonPersistentObjectSpace nonPersistentObjectSpace)
            {
                //nonPersistentObjectSpace.Reloaded -= NonPersistentObjectSpace_Reloaded;
                nonPersistentObjectSpace.Refresh();
            }
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            System.Diagnostics.Debug.WriteLine("BranchStockPivotViewController activated");

            if (View.ObjectSpace is NonPersistentObjectSpace nonPersistentObjectSpace)
            {
                nonPersistentObjectSpace.ObjectsGetting += NonPersistentObjectSpace_ObjectsGetting;
            }
        }


        private void NonPersistentObjectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            if (e.ObjectType == typeof(BranchStockPivot))
            {
                e.Objects = (System.Collections.IList)branchStockPivots; // Provide the current list of BranchStockPivot objects
            }
        }

        private IList<BranchStockPivot> GetBranchStockPivotData()
        {
            var result = new List<BranchStockPivot>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("[dbo].[spGetBranchStockPivot2]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var branchStock = new BranchStockPivot(Session.DefaultSession);

                            {
                                // 1) Read the five new columns first
                                branchStock.Supplier = reader.IsDBNull(reader.GetOrdinal("Supplier"))? string.Empty: reader.GetString(reader.GetOrdinal("Supplier"));
                                branchStock.Category = reader.IsDBNull(reader.GetOrdinal("Category"))? string.Empty: reader.GetString(reader.GetOrdinal("Category"));
                                branchStock.Brand = reader.IsDBNull(reader.GetOrdinal("Brand"))? string.Empty: reader.GetString(reader.GetOrdinal("Brand"));
                                if (!reader.IsDBNull(reader.GetOrdinal("LastDeliveryDate")))
                                {
                                    branchStock.LastDeliveryDate = reader.GetDateTime(reader.GetOrdinal("LastDeliveryDate"));
                                }
                                else
                                {
                                    // Assign a non-null default:
                                    branchStock.LastDeliveryDate = DateTime.MinValue;
                                    // or: branchStock.LastDeliveryDate = DateTime.Now;
                                }


                                branchStock.LastQtyDelivered = reader.IsDBNull(reader.GetOrdinal("LastQtyDelivered"))? 0: Convert.ToInt32(reader["LastQtyDelivered"]);
                                branchStock.ItemCode = reader["ItemCode"].ToString();
                                branchStock.ItemName = reader["ItemName"].ToString();
                                branchStock.Cost = reader.IsDBNull(reader.GetOrdinal("Cost")) ? 0 : Convert.ToDecimal(reader["Cost"]);
                                branchStock.Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : Convert.ToDecimal(reader["Price"]);
                                branchStock.Warehouse = reader["Warehouse"].ToString();
                                branchStock.MainStore = reader["MainStore"].ToString();
                                branchStock.Bambang = reader["Bambang"].ToString();
                                branchStock.TotalStocks = reader.IsDBNull(reader.GetOrdinal("TotalStocks")) ? 0 : reader.GetInt32(reader.GetOrdinal("TotalStocks"));
                                branchStock.TotalCost = reader.IsDBNull(reader.GetOrdinal("TotalCost")) ? 0 : Convert.ToDecimal(reader["TotalCost"]);
                                branchStock.TotalPrice = reader.IsDBNull(reader.GetOrdinal("TotalPrice")) ? 0 : Convert.ToDecimal(reader["TotalPrice"]);
                                branchStock.Active = reader.IsDBNull(reader.GetOrdinal("Active")) ? false : Convert.ToBoolean(reader["Active"]);
                            };
                            result.Add(branchStock);
                        }
                    }
                }
            }

            return result;
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            if (View.ObjectSpace is NonPersistentObjectSpace nonPersistentObjectSpace)
            {
                nonPersistentObjectSpace.ObjectsGetting -= NonPersistentObjectSpace_ObjectsGetting;
            }
            base.OnDeactivated();
        }
    }
}
