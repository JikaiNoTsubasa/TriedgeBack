using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;
using triedge_api.JobManagers;

namespace triedge_api.Global;

public static class ProjectInit
{
    public static void InitAdminUser(UserManager manager)
    {
        var user = manager.FetchUserByLogin("admin") ?? manager.CreateUser("admin", "Admin", "admin");
    }
}
