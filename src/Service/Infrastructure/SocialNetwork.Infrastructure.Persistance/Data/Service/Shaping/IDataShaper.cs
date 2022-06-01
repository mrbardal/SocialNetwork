namespace SocialNetwork.Infrastructure.Persistance.Data.Service.Shaping;

public interface IDataShaper<in T>
{
    dynamic Shape(T entity, string fields);
    IEnumerable<dynamic> Shape(IEnumerable<T> entities, string fields);
}