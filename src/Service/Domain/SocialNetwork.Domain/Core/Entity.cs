namespace SocialNetwork.Domain.Core;

public abstract class Entity<TKey> : IEntity<TKey>, IEquatable<Entity<TKey>>
where TKey : IEquatable<TKey>, IComparable<TKey>

{
    private int? _hashCode;
    public TKey Id { get; protected set; }
    public bool IsTransient()
    {
        return Id.Equals(default(TKey));
    }
    public override int GetHashCode()
    {
        if (IsTransient())
        {
            return base.GetHashCode();
        }
        else
        {
            if (!_hashCode.HasValue)
            {
                _hashCode = Id.GetHashCode() ^ 31;
            }

            return _hashCode.Value;
        }
    }
    public bool Equals(Entity<TKey> other)
    {
        if (other == null)
        {
            return false;
        }

        if (Object.ReferenceEquals(this, other))
        {
            return true;
        }

        if (other.IsTransient() || this.IsTransient())
        {
            return false;
        }

        return Id.Equals(other.Id);
    }
    public override bool Equals(object obj)
    {
        //if (obj == null)
        //{
        //    return false;
        //}

        //if (Object.ReferenceEquals(this, obj))
        //{
        //    return true;
        //}

        if (this.GetType() != obj.GetType())
        {
            return false;
        }
        return true;

        //var entity = obj as Entity<TKey>;

        //if (entity.IsTransient() || this.IsTransient())
        //{
        //    return false;
        //}

        //return Id.Equals(entity.Id);
    }
    public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
    {
        if (Object.Equals(left, null))
        {
            return Object.Equals(right, null) ? true : false;
        }
        else
        {
            return left.Equals(right);
        }
    }
    public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
    {
        return !(left == right);
    }
}
