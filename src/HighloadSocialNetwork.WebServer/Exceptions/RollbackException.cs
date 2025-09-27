namespace HighloadSocialNetwork.WebServer.Exceptions;

public class RollbackException : Exception;

public class RollbackException<T> : RollbackException
{
    public T? Result { get; init; }
}
