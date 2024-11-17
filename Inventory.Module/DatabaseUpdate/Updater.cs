using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using Inventory.Module.BusinessObjects;

namespace Inventory.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater
{
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion)
    {
    }
    public override void UpdateDatabaseAfterUpdateSchema()
    {
        base.UpdateDatabaseAfterUpdateSchema();
        //string name = "MyName";
        //EntityObject1 theObject = ObjectSpace.FirstOrDefault<EntityObject1>(u => u.Name == name);
        //if(theObject == null) {
        //    theObject = ObjectSpace.CreateObject<EntityObject1>();
        //    theObject.Name = name;
        //}

        var defaultRole = CreateDefaultRole();

        var userAdmin = ObjectSpace.FirstOrDefault<Employee>(u => u.UserName == "Admin");
        if (userAdmin == null)
        {
            userAdmin = ObjectSpace.CreateObject<Employee>();
            userAdmin.UserName = "Admin";
            // Set a password if the standard authentication type is used
            userAdmin.SetPassword("");

            // The UserLoginInfo object requires a user object Id (Oid).
            // Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
            ObjectSpace.CommitChanges(); //This line persists created object(s).
            ((ISecurityUserWithLoginInfo)userAdmin).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(userAdmin));
        }
        // If a role with the Administrators name doesn't exist in the database, create this role
        var adminRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == "Administrators");
        if (adminRole == null)
        {
            adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
            adminRole.Name = "Administrators";
        }
        adminRole.IsAdministrative = true;
        userAdmin.Roles.Add(adminRole);


        var branches = ObjectSpace.GetObjectsCount(typeof(Branch), null);
        if (branches == 0)
        {
            var branch1 = ObjectSpace.CreateObject<Branch>();
            branch1.BranchName = "Branch1";

            var branch2 = ObjectSpace.CreateObject<Branch>();
            branch2.BranchName = "Branch2";

            var branchUserRole = CreateBranchUserRole();

            var user1 = CreateEmployeeUser("emp1");
            user1.Branch = branch1;
            user1.Roles.Add(branchUserRole);
            user1.Roles.Add(defaultRole);

            var user12 = CreateEmployeeUser("emp12");
            user12.Branch = branch1;
            user12.Roles.Add(branchUserRole);
            user12.Roles.Add(defaultRole);

            var user2 = CreateEmployeeUser("emp2");
            user2.Branch = branch2;
            user2.Roles.Add(branchUserRole);
            user2.Roles.Add(defaultRole);

            var user22 = CreateEmployeeUser("emp22");
            user22.Branch = branch2;
            user22.Roles.Add(branchUserRole);
            user22.Roles.Add(defaultRole);

            var branchManagerRole = CreateBranchManagerRole();

            var manager1 = CreateEmployeeUser("manager1");
            manager1.Branch = branch1;
            manager1.Roles.Add(branchManagerRole);
            manager1.Roles.Add(defaultRole);

            var manager2 = CreateEmployeeUser("manager2");
            manager2.Branch = branch2;
            manager2.Roles.Add(branchManagerRole);
            manager2.Roles.Add(defaultRole);
        }


        ObjectSpace.CommitChanges(); //This line persists created object(s).
    }


    private Employee CreateEmployeeUser(string userName)
    {
        var employee = ObjectSpace.CreateObject<Employee>();
        employee.UserName = userName;
        ObjectSpace.CommitChanges(); //This line persists created object(s).
        ((ISecurityUserWithLoginInfo)employee).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(employee));

        return employee;
    }

    private PermissionPolicyRole CreateBranchManagerRole()
    {
        var role = ObjectSpace.CreateObject<PermissionPolicyRole>();
        role.Name = "BranchManagerRole";

        role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
        role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Branch_ListView", SecurityPermissionState.Allow);
        role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Employee_ListView", SecurityPermissionState.Allow);

        //Branch
        role.AddObjectPermissionFromLambda<Branch>(SecurityOperations.FullObjectAccess, d => d.Employees.Any(au => au.Oid == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
        role.AddMemberPermissionFromLambda<Branch>(SecurityOperations.ReadWriteAccess, nameof(Branch.Employees), d => d.Employees.Any(au => au.Oid == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

        //Employee
        role.AddObjectPermissionFromLambda<Employee>(SecurityOperations.FullObjectAccess, au => au.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
        role.AddObjectPermissionFromLambda<Employee>(SecurityOperations.ReadWriteAccess, au => au.Branch.Employees.Any(au2 => au2.Oid == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

        return role;
    }

    private PermissionPolicyRole CreateBranchUserRole()
    {
        var role = ObjectSpace.CreateObject<PermissionPolicyRole>();
        role.Name = "BranchUserRole";
        role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
        role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Branch_ListView", SecurityPermissionState.Allow);
        role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Employee_ListView", SecurityPermissionState.Allow);

        //Branch
        role.AddObjectPermissionFromLambda<Branch>(SecurityOperations.Read, d => d.Employees.Any(au => au.Oid == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);


        //SaleHeader
        //role.AddObjectPermissionFromLambda<SaleHeader>(SecurityOperations.Read, d => d.Employees.Any(au => au.Oid == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

        //Employee
        role.AddObjectPermissionFromLambda<Employee>(SecurityOperations.Read, au => au.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
        role.AddObjectPermissionFromLambda<Employee>(SecurityOperations.Read, au => au.Branch.Employees.Any(au2 => au2.Oid == (Guid)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
        //this is from ChatGPT
        // Allow reading of Branch objects (or specific properties) necessary for initiating transfers.
        //role.AddTypePermissionsRecursively<Employee>(SecurityOperations.Read, SecurityPermissionState.Allow);


        return role;
    }

    private PermissionPolicyRole CreateDefaultRole()
    {
        PermissionPolicyRole defaultRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(role => role.Name == "Default");
        if (defaultRole == null)
        {
            defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
            defaultRole.Name = "Default";

            defaultRole.AddObjectPermissionFromLambda<Employee>(SecurityOperations.Read, cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
            defaultRole.AddMemberPermissionFromLambda<Employee>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            defaultRole.AddMemberPermissionFromLambda<Employee>(SecurityOperations.Write, "StoredPassword", cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
            defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
        }

        return defaultRole;
    }
}
