namespace HighloadSocialNetwork.WebServer.Exceptions;

internal class RollbackException : Exception
{
}

internal class RollbackException<T> : RollbackException
{
    public T? Result { get; init; }
}
