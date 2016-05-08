using Ferhah.SyncNetObjects.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects
{
    public class SyncConfiguration<T> : SyncConfiguration where T : class
    {

        public void Key<TValue>(Expression<Func<T, TValue>> selector)
        {
            PropertyInfo info = GetProperty(selector);

            if (!IsSimpleType(info.PropertyType))
            {
                throw new SyncConfigException(typeof(T).Name, info.Name," Not primitive key");
            }

            this.Keys.Add(new SyncProperty(info));
        }

        public void KeyNavigationProperty<TValue>(Expression<Func<T, TValue>> selector)
        {
            PropertyInfo info = GetProperty(selector);

            if (IsSimpleType(info.PropertyType))
            {
                throw new SyncConfigException(typeof(T).Name, info.Name, " Not a navigation property");
            }

            this.KeyNavigationProperties.Add(new SyncProperty(GetProperty(selector)));
        }

        public void NavigationProperty<TValue>(Expression<Func<T, TValue>> selector)
        {
            PropertyInfo info = GetProperty(selector);

            if (IsSimpleType(info.PropertyType))
            {   
                throw new SyncConfigException(typeof(T).Name, info.Name ," Not a navigation property");
            }

            this.NavigationProperties.Add(new SyncProperty(info));
        }

        public void PrimitiveProperty<TValue>(Expression<Func<T, TValue>> selector)
        {
            PropertyInfo info = GetProperty(selector);

            if(!IsSimpleType(info.PropertyType))
            {
                throw new SyncConfigException(typeof(T).Name, info.Name, " Not primitive key");
            }

            this.PrimitiveProperties.Add(new SyncProperty(info));
        }

        public static bool IsSimpleType(Type type)
        {
            return
                type.GetTypeInfo().IsPrimitive ||
                new Type[] {
            typeof(Enum),
            typeof(String),
            typeof(Decimal),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Guid)
                }.Contains(type)
                 ||
                //Convert.GetTypeCode(type) != TypeCode.Object ||
                (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && IsSimpleType(type.GetTypeInfo().GenericTypeArguments[0]))
                ;
        }

        public void ComplexType<TValue>(Expression<Func<T, TValue>> selector, SyncConfiguration complexTypeConfig)
        {
            PropertyInfo info = GetProperty(selector);


            SyncProperty syncProperty = new SyncProperty(info);
            syncProperty.Configuration = complexTypeConfig;

            if (complexTypeConfig.Keys.Count > 0)
            {
                throw new SyncConfigException(typeof(T).Name, info.Name, " ComplexTypes cannot have keys");
            }

            this.ComplexTypes.Add(syncProperty);
        }


        public PropertyInfo GetProperty<TValue>(Expression<Func<T, TValue>> selector)
        {
            Expression body = selector;
            if (body is LambdaExpression)
            {
                body = ((LambdaExpression)body).Body;
            }
            switch (body.NodeType)
            {
                case ExpressionType.MemberAccess:

                    var member = ((MemberExpression)body).Member;

                    if(!(member is PropertyInfo))
                    {
                        throw new SyncConfigException(typeof(T).Name, member.Name, " is not a property");
                    }

                    return (PropertyInfo)member;
                default:
                    throw new InvalidOperationException();
            }
        }

        public override Type GetGenericType()
        {
            return typeof(T);
        }

    }

}
