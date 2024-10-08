using System;
using MicroServices.UserServices.Data;

namespace UserServices.Data;

public class UserRepo : IUserRepo

{
    private readonly AppDBContext _context;

    public UserRepo(AppDBContext context)
    {
        _context = context;
    }
    public void CreateUser(User user)
    {
        if (user == null){
            throw new ArgumentNullException(nameof(user));

        }
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void DeleteUser(int id)
    {
        var user = _context.Users.FirstOrDefault(c => c.UserId == id);
        if (user == null){
            throw new ArgumentNullException(nameof(user));
        }
        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }

    public User GetUserById(int id)
    {
        return _context.Users.FirstOrDefault(c => c.UserId == id);
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }
    public bool SaveChanges(){
        return(_context.SaveChanges() >= 0);
    }
}
