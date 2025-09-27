namespace HighloadSocialNetwork.WebServer.Exceptions;

public class EntityNotFoundException : Exception;

public class EntityNotFoundException<TEntity> : EntityNotFoundException
{
    public readonly Type EntityType = typeof(TEntity);
}