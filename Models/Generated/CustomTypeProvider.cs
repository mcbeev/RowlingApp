using System;
using System.Collections.Generic;
using System.Linq;
using Kentico.Kontent.Delivery;

namespace RowlingApp.Models.Generated
{
    public class CustomTypeProvider : ITypeProvider
    {
        private static readonly Dictionary<Type, string> _codenames = new Dictionary<Type, string>
        {
            {typeof(Team), "team"},
            {typeof(TeamMember), "teammember"}

        };

        public Type GetType(string contentType)
        {
            return _codenames.Keys.FirstOrDefault(type => GetCodename(type).Equals(contentType));
        }

        public string GetCodename(Type contentType)
        {
            return _codenames.TryGetValue(contentType, out var codename) ? codename : null;
        }
    }
}