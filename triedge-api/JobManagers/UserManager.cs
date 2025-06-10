using System;
using triedge_api.Database;
using triedge_api.Database.Models;

namespace triedge_api.JobManagers;

public class UserManager(TriContext context) : TriManager(context)
{
    public User CreateUser(string login, string name, string? password = null)
    {
        var user = new User()
        {
            Login = login,
            Name = name
        };

        if (password != null) user.SetPassword(password);
        user.MarkAsCreated();
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public List<User> FetchUsers()
    {
        return [.. _context.Users];
    }

    public User FetchUserById(long id) => _context.Users.FirstOrDefault(u => u.Id == id) ?? throw new Exception($"User not found for id {id}");

    public User? FetchUserByLogin(string login) => _context.Users.FirstOrDefault(u => u.Login.Equals(login));
}
