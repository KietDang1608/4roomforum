using System;

namespace UserServices.Data;

public interface  IUserRepo
{
    IEnumerable<User> GetAllUsers();
    User GetUserById(int id);
    void CreateUser(User user);
    void UpdateUser(User user);
    void DeleteUser(int id);
    bool SaveChanges();
}
