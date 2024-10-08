    using System;

namespace UserServices.Data;

public interface IRoleRepo
{
    IEnumerable<Role> GetAllRoles();
    Role GetRoleById(int id);
    void CreateRole(Role role);
    void UpdateRole(Role role);
    void DeleteRole(int id);
    bool SaveChanges();
}
