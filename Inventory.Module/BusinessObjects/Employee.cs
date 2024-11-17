using System.ComponentModel;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;

namespace Inventory.Module.BusinessObjects;

[MapInheritance(MapInheritanceType.ParentTable)]
[DefaultProperty(nameof(UserName))]
public class Employee : PermissionPolicyUser, ISecurityUserWithLoginInfo
{

    public Employee(Session session) : base(session)
    {
    }

    [Browsable(false)]
    [Aggregated, Association("User-LoginInfo")]
    public XPCollection<ApplicationUserLoginInfo> LoginInfo =>
        GetCollection<ApplicationUserLoginInfo>(nameof(LoginInfo));

    IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => LoginInfo.OfType<ISecurityUserLoginInfo>();

    ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName,
        string providerUserKey)
    {
        ApplicationUserLoginInfo result = new ApplicationUserLoginInfo(Session);
        result.LoginProviderName = loginProviderName;
        result.ProviderUserKey = providerUserKey;
        result.User = this;
        return result;
    }

    public override void AfterConstruction()
    {
        base.AfterConstruction();
        // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
    }


    Branch _branch;
    string _firstName;
    string _lastName;

    [Size(SizeAttribute.DefaultStringMappingFieldSize)]
    public string LastName
    {
        get => _lastName;
        set => SetPropertyValue(nameof(LastName), ref _lastName, value);
    }

    [Size(SizeAttribute.DefaultStringMappingFieldSize)]
    public string FirstName
    {
        get => _firstName;
        set => SetPropertyValue(nameof(FirstName), ref _firstName, value);
    }

    [Association("Branch-Employees")]
    public Branch Branch
    {
        get => _branch;
        set => SetPropertyValue(nameof(Branch), ref _branch, value);
    }

}
