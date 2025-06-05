using System;
using triedge_api.Database;

namespace triedge_api.JobManagers;

public class TriManager(TriContext context)
{
    protected TriContext _context = context;
}
