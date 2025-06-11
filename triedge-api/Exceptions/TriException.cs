using System;

namespace triedge_api.Exceptions;

public class TriException(string message) : Exception(message)
{
}
