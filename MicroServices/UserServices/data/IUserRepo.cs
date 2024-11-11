using System;
using UserServices.DTOs;
namespace UserServices.Data;


public interface  IUserRepo
{
    IEnumerable<User> GetAllUsers();
    User GetUserById(int id);
    User GetUserByEmail(string email);
    void CreateUser(User user);
    void UpdateUser(User user);
    void DeleteUser(int id);
    User Login(UserLoginDTO user);
    bool SaveChanges();
}
