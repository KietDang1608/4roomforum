using System;
using MicroServices.UserServices.Data;

namespace UserServices.Data;

public class RoleRepo : IRoleRepo

{
    private readonly AppDBContext _context;

    public RoleRepo(AppDBContext context)
    {
        _context = context;
    }
    public void CreateRole(Role role)
    {
        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));

        }
        _context.Roles.Add(role);
        _context.SaveChanges();
    }

    public void DeleteRole(int id)
    {
        var role = _context.Roles.FirstOrDefault(c => c.RoleId == id);
        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }
        _context.Roles.Remove(role);
        _context.SaveChanges();
    }

    public IEnumerable<Role> GetAllRoles()
    {
        return _context.Roles.ToList();
    }

    public Role GetRoleById(int id)
    {
        return _context.Roles.FirstOrDefault(c => c.RoleId == id);
    }

    public void UpdateRole(Role role)
    {
        throw new NotImplementedException();
    }
    public bool SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
    }
}
