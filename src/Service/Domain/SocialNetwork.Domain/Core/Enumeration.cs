using System.Reflection;

namespace SocialNetwork.Domain.Core;

public abstract class Enumeration : IComparable<Enumeration>, IEquatable<Enumeration>
{
    public int Id { get; set; }
    public string Name { get; set; }
    protected Enumeration(int id, string name) => (Id, Name) = (id, name);
    public override string ToString() => Name;
    public override int GetHashCode() => Id.GetHashCode();
    public int CompareTo(Enumeration other) => Id.CompareTo(other.Id);
    public bool Equals(Enumeration other)
    {
        if (other == null)
        {
            return false;
        }

        if (Object.ReferenceEquals(this, other))
        {
            return true;
        }

        return Id.Equals(other.Id);
    }
    public override bool Equals(object obj)
    {
        if (this.GetType() != obj.GetType())
        {
            return false;
        }

        return true;
    }
    public static bool operator ==(Enumeration left, Enumeration right)
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
    public static bool operator !=(Enumeration left, Enumeration right)
    {
        return !(left == right);
    }
    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        return
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                      .Select(f => f.GetValue(null))
                      .Cast<T>();
    }
    public static T FromValue<T>(int value) where T : Enumeration
    {
        return Parse<T>(item => item.Id == value);
    }
    public static T FromName<T>(string name) where T : Enumeration
    {
        return Parse<T>(item => item.Name == name);
    }
    private static T Parse<T>(Func<T, bool> predicate) where T : Enumeration
    {
        var result = GetAll<T>().FirstOrDefault(predicate);

        if (result == null)
        {
            throw new InvalidOperationException($"Value is not a valid in {typeof(T).Name}");
        }

        return result;
    }
}
