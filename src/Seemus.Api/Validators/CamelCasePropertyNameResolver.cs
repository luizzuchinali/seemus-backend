using FluentValidation.Internal;
using Seemus.Domain.Extensions;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Seemus.Api.Validations
{
    public class CamelCasePropertyNameResolver
    {

        public static string ResolvePropertyName(Type type, MemberInfo memberInfo, LambdaExpression expression)
        {
            return DefaultPropertyNameResolver(type, memberInfo, expression).ToCamelCase();
        }

        private static string DefaultPropertyNameResolver(Type type, MemberInfo memberInfo, LambdaExpression expression)
        {
            if (expression != null)
            {
                var chain = PropertyChain.FromExpression(expression);
                if (chain.Count > 0) return chain.ToString();
            }

            if (memberInfo != null)
            {
                return memberInfo.Name;
            }

            return null;
        }

        
    }

}
