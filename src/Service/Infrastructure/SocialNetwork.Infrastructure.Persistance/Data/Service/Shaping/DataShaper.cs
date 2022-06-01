using System.Dynamic;
using System.Reflection;

namespace SocialNetwork.Infrastructure.Persistance.Data.Service.Shaping;

public class DataShaper<T> : IDataShaper<T>
{
    private readonly PropertyInfo[] _properties;

    public DataShaper()
    {
        _properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
    }

    public dynamic Shape(T entity, string fields)
    {
        //1. fileds -> requireProperties
        //2. properties --> Fetch Data 
        //3. create dynamic

        var requireProperties = GetProperties(fields);

        return GetDataEntity(entity, requireProperties);
    }

    private IEnumerable<PropertyInfo> GetProperties(string fields)
    {
        if (string.IsNullOrEmpty(fields))
        {
            return _properties;
        }
        else
        {
            var fieldNameList = fields.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

            var requireProperties = new List<PropertyInfo>();

            fieldNameList.ForEach(fieldName =>
            {
                var property = _properties.FirstOrDefault(p =>
                    p.Name.Equals(fieldName.Trim(), StringComparison.OrdinalIgnoreCase));

                if (property != null)
                {
                    requireProperties.Add(property);
                }
            });

            return requireProperties;
        }
    }

    private dynamic GetDataEntity(T entity, IEnumerable<PropertyInfo> properties)
    {
        var shaped = new ExpandoObject();
        properties.ToList().ForEach(p =>
        {
            var pValue = p.GetValue(entity);
            shaped.TryAdd(p.Name, pValue);
        });

        return shaped;
    }

    public IEnumerable<dynamic> Shape(IEnumerable<T> entities, string fields)
    {
        var requireProperties = GetProperties(fields);

        var shapedList = new List<ExpandoObject>();

        entities.ToList().ForEach(entity =>
        {
            shapedList.Add(GetDataEntity(entity, requireProperties));
        });

        return shapedList;
    }
}
