using System.Linq.Expressions;
using System.Reflection;

namespace MediaStoreTestsDemo.Extensions
{
    public static class ApiModelsExtensions
    {
        public static TModel ClearValue<TModel, TProperty>(this TModel apiModel, Expression<Func<TModel, TProperty>> propertySelector)
      where TModel : ApiModel
        {
            if (propertySelector.Body is MemberExpression memberExpression)
            {
                if (memberExpression.Member is PropertyInfo propertyInfo)
                {
                    propertyInfo.SetValue(apiModel, default(TProperty));
                }
            }

            return apiModel;
        }

        public static object GetKey<TModel>(this TModel model)
         where TModel : ApiModel
        {
            var keyProperty = typeof(TModel)
                .GetProperties()
                .FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Any());

            if (keyProperty == null)
            { 
                throw new NotImplementedException($"Key attribute is not set on {typeof(TModel).Name}");
            }

            return keyProperty.GetValue(model);
        }

        public static T GetKey<T, TModel>(this TModel model)
         where TModel : ApiModel
        {
            var keyValue = model.GetKey();

            if (keyValue == null)
            {
                return default;
            }

            try
            {
                return (T)Convert.ChangeType(keyValue, typeof(T));
            }
            catch
            {
                return default;
            }
        }
    }
}
