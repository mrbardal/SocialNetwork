namespace SocialNetwork.Domain.Core;

public interface IEntity<TKey>
where TKey : IEquatable<TKey>, IComparable<TKey>
{
    TKey Id { get; }
    bool IsTransient();
}
