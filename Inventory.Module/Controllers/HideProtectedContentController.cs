using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security;

namespace Inventory.Module.Controllers
{
    public class HideProtectedContentController : ViewController<ObjectView>
    {
        private AppearanceController _appearanceController;

        protected override void OnActivated()
        {
            base.OnActivated();
            _appearanceController = Frame.GetController<AppearanceController>();
            if (_appearanceController != null)
            {
                _appearanceController.CustomApplyAppearance += appearanceController_CustomApplyAppearance;
            }
        }

        protected override void OnDeactivated()
        {
            if (_appearanceController != null)
            {
                _appearanceController.CustomApplyAppearance -= appearanceController_CustomApplyAppearance;
            }
            base.OnDeactivated();
        }

        void appearanceController_CustomApplyAppearance(object sender, ApplyAppearanceEventArgs e)
        {
            if (e.AppearanceObject.Visibility == null || e.AppearanceObject.Visibility == ViewItemVisibility.Show)
            {
                var security = Application.GetSecurityStrategy();
                if (View is ListView && e.Item is ColumnWrapper wrapper)
                {
                    if (!security.CanRead(View.ObjectTypeInfo.Type,
                            wrapper.PropertyName))
                    {
                        e.AppearanceObject.Visibility = ViewItemVisibility.Hide;
                    }
                }
                if (View is DetailView && e.Item is PropertyEditor editor)
                {
                    var targetObject = e.ContextObjects.Length > 0 ? e.ContextObjects[0] : null;
                    if (targetObject != null && !security.CanRead(targetObject, editor.PropertyName))
                    {
                        e.AppearanceObject.Visibility = ViewItemVisibility.Hide;
                    }
                }
            }
        }
    }
}
