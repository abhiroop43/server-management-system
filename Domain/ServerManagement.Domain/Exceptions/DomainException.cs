namespace ServerManagement.Domain.Exceptions;

public class DomainException(string message)
    : Exception($"Domain exception was thrown: \"{message}\"");
