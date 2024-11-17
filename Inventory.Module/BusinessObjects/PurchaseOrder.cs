using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace Inventory.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class PurchaseOrder : XPObject
    {
        public PurchaseOrder(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

    }
}