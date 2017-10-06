using Entity;
using System;
using Wakool.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CustomDataAnnotations;

namespace MetadataDiscover
{
    public static class AssemblyUtils
    {
        public static List<Type> GetEntities()
        {
            List<Type> entities = new List<Type>();
            Type[] types = Assembly.GetAssembly(typeof(EntityBase)).GetTypes();
            foreach (var item in types)
            {
                if (item.BaseType == typeof(EntityBase))
                    entities.Add(item);
            }
            return entities;
        }

        public static int GetMaxStringLength(this PropertyInfo type, out string errorMessage)
        {
            StringLengthAttribute attribute = type.GetCustomAttribute<StringLengthAttribute>();
            errorMessage = attribute == null ? null : attribute.ErrorMessage;
            return attribute == null ? 0 : attribute.MaximumLength;
        }

        public static int GetMinStringLength(this PropertyInfo type, out string errorMessage)
        {
            StringLengthAttribute attribute = type.GetCustomAttribute<StringLengthAttribute>();
            errorMessage = attribute == null ? null : attribute.ErrorMessage;
            return attribute == null ? 0 : attribute.MinimumLength;
        }

        public static List<string> GetEntitiesName()
        {
            List<Type> types = GetEntities();
            List<string> names = new List<string>();
            foreach (var item in types)
            {
                names.Add(GetEntityDisplayAttribute(item));
            }
            return names;
        }

        public static string GetEntityDisplayAttribute(Type type)
        {
            DisplayNameAttribute attribute = type.GetCustomAttribute<DisplayNameAttribute>();
            return attribute == null ? type.Name : attribute.DisplayName;
        }

        public static string GetPropertyDisplay(PropertyInfo type)
        {
            DisplayNameAttribute attribute = type.GetCustomAttribute<DisplayNameAttribute>();
            return attribute == null ? type.Name : attribute.DisplayName;
        }

        public static List<string> GetPropertyDisplay(this Type type)
        {
            List<string> names = new List<string>();

            foreach (PropertyInfo item in type.GetProperties())
            {
                names.Add(GetDisplay(item));
            }
            return names;
        }

        public static string GetPropertyDisplayAttribute(Type type)
        {
            DisplayNameAttribute attribute = type.GetCustomAttribute<DisplayNameAttribute>();
            return attribute == null ? type.Name : attribute.DisplayName;
        }

        public static List<string> GetPropertyDisplayName(this PropertyInfo[] properties)
        {
            List<string> list = new List<string>();
            foreach (PropertyInfo item in properties)
            {
                DisplayNameAttribute attribute = item.GetCustomAttribute<DisplayNameAttribute>();
                list.Add(attribute == null ? item.Name : attribute.DisplayName);
            }
            return list;
        }

        public static string GetPropertyTableNameAttribute(Type type)
        {
            var attribute = type.GetCustomAttribute<TableName>();
            return (attribute == null || string.IsNullOrWhiteSpace(attribute.Text)) ? type.Name : attribute.Text;
        }

        public static List<string> GetPropertyName()
        {
            List<Type> types = GetEntities();
            List<string> names = new List<string>();
            foreach (var item in types)
            {
                names.Add(GetPropertyDisplayAttribute(item));
            }
            return names;
        }

        public static List<PropertyInfo> GetAllPropertyAttributes(this Type type, Attribute attribute)
        {
            List<PropertyInfo> property = type.GetProperties().ToList();
            List<PropertyInfo> propertyReturn = new List<PropertyInfo>();
            foreach (PropertyInfo item in property)
            {
                if (item.GetCustomAttribute(attribute.GetType()) != null)
                    propertyReturn.Add(item);
            }
            return propertyReturn;
        }

        public static bool GetRegularExpression(this PropertyInfo type, out string errorMessage, object obj)
        {
            bool temp = true;
            errorMessage = "";
            RegularExpressionAttribute attribute = type.GetCustomAttribute<RegularExpressionAttribute>();
            if (attribute != null)
                if (!attribute.IsValid(type.GetValue(obj)))
                {
                    errorMessage += attribute.ErrorMessage;
                    temp = false;
                }
            return temp;
        }

        public static string GetDisplay(PropertyInfo type)
        {
            DisplayNameAttribute attribute = type.GetCustomAttribute<DisplayNameAttribute>();
            return attribute == null ? type.Name : attribute.DisplayName;
        }

        public static List<Type> GetEntitiesAttributes(Attribute attribute)
        {
            List<Type> types = GetEntities();
            List<Type> hasAttribute = new List<Type>();
            foreach (Type item in types)
            {
                if (item.GetCustomAttribute(attribute.GetType()) != null)
                    hasAttribute.Add(item);
            }
            return hasAttribute;
        }

        public static bool GetRequired(this PropertyInfo type, out string errorMessage, object obj)
        {
            bool temp = true;
            errorMessage = "";
            RequiredAttribute attribute = type.GetCustomAttribute<RequiredAttribute>();
            if (attribute != null)
                if (!attribute.IsValid(type.GetValue(obj)))
                {
                    errorMessage += attribute.ErrorMessage;
                    temp = false;
                }
            return temp;
        }

        public static Type GetEntityByName(string name)
        {
            return GetEntities().FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
        }

        public static string GetEntityIcon(this Type type)
        {
            IconAttribute attr = type.GetCustomAttribute<IconAttribute>();
            return attr == null ? "mdi mdi-book-open-variant" : attr.Icon;
        }

        public static string GetWebMaxLength(this PropertyInfo type)
        {
            MaskedTextAttribute attribute = type.GetCustomAttribute<MaskedTextAttribute>();
            if (attribute != null)
                return string.Format("maxlength={0}", attribute.Mask.Length);
            else
            {
                StringLengthAttribute attr = type.GetCustomAttribute<StringLengthAttribute>();
                return (attr != null) ? string.Format("maxlength={0}", attr.MaximumLength) : "";
            }
        }

        public static string GetWebMinLength(this PropertyInfo type)
        {
            StringLengthAttribute attribute = type.GetCustomAttribute<StringLengthAttribute>();
            if (attribute != null)
                return string.Format("minlengt ={0}", attribute.MinimumLength);
            return "";

        }

        public static string GetWebStringType(this PropertyInfo type)
        {
            string webType = "Text";

            if (type.Name.ToLower().Contains("phone"))
                webType = "tel";
            else
                if (type.Name.ToLower().Contains("email"))
                webType = "email";
            else
                    if (type.Name.ToLower().Contains("url"))
                webType = "url";
            return webType;
        }

        public static string GetWebRequired(this PropertyInfo property)
        {
            RequiredAttribute regex = property.GetCustomAttribute<RequiredAttribute>();
            return (regex != null) ? "required" : "";
        }

        public static string GetWebPattern(this PropertyInfo property)
        {
            RegularExpressionAttribute regex = property.GetCustomAttribute<RegularExpressionAttribute>();
            return (regex != null) ? string.Format("pattern={0}", regex.Pattern.ToString()) : "";
        }

        public static string RemoveMask(this PropertyInfo type, string text)
        {
            MaskedTextAttribute attribute = type.GetCustomAttribute<MaskedTextAttribute>();
            if (attribute != null)
            {
                string masc = attribute.Mask.Replace("9", "").Replace(",", ".");

                foreach (char item in masc.ToCharArray())
                {
                    text = text.Replace(item.ToString(), string.Empty);
                }
            }
            return text;
        }
    }
}
