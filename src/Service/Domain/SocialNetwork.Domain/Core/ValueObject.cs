namespace SocialNetwork.Domain.Core;

public abstract class ValueObject<T> : ICloneable
    where T : ValueObject<T>
{
    public object Clone()
    {
        return this.MemberwiseClone();
    }
    public override bool Equals(object other)
    {
        if (other == null)
        {
            return false;
        }

        if (Object.ReferenceEquals(this, other))
        {
            return true;
        }

        if (this.GetType() != other.GetType())
        {
            return false;
        }

        //return MemberEquals((T)other);
        return GetMembersEquality().SequenceEqual(((T)other).GetMembersEquality());
    }
    protected abstract bool MemberEquals(T other);
    protected abstract IEnumerable<object> GetMembersEquality();
    protected abstract int MemberHashCode();
    public override int GetHashCode()
    {
        return MemberHashCode();
    }
    public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
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
    public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
    {
        return !(left == right);
    }
}
