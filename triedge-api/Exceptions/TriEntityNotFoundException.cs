using System;

namespace triedge_api.Exceptions;

public class TriEntityNotFoundException(string message) : TriException(message)
{
}
