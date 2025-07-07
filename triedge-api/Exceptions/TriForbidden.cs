using System;

namespace triedge_api.Exceptions;

public class TriForbidden(string message) : TriException(message)
{
}
