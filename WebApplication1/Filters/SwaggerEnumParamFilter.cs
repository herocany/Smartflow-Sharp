using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class SwaggerEnumParamFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            var type = typeof(Nullable<>);
            var context_type = context.ApiParameterDescription.Type;
            if (context_type.IsGenericType && context_type.GetGenericTypeDefinition().Equals(type) && context_type.GenericTypeArguments[0].IsEnum)
            {
                var param_type = context_type.GenericTypeArguments[0];
                StringBuilder sb = new StringBuilder();
                sb.Append($"{parameter.Description} ");
                var values = Enum.GetValues(param_type);
                for (int i = 0; i < values.Length; i++)
                {
                    var value = (int)values.GetValue(i);
                    var des = GetDescription(param_type, value);
                    sb.Append($"{value}:{des},");
                }
                parameter.Description = sb.ToString().Remove(sb.Length - 1, 1);
            }
        }
        private static string GetDescription(Type t, object value)
        {
            foreach (MemberInfo mInfo in t.GetMembers())
            {
                if (mInfo.Name == t.GetEnumName(value))
                {
                    foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                    {
                        if (attr.GetType() == typeof(DescriptionAttribute))
                        {
                            return ((DescriptionAttribute)attr).Description;
                        }
                    }
                }
            }
            return string.Empty;
        }
    }

}
