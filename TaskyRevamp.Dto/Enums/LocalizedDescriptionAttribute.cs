using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.Enums
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private readonly string _resourceKey;
        private readonly ResourceManager _resource;
        public LocalizedDescriptionAttribute(string resourceKey, Type resourceType)
        {
            _resource = new ResourceManager(resourceType);
            _resourceKey = resourceKey;
        }

        public override string Description
        {
            get
            {
                string? displayName = _resource.GetString(_resourceKey);

                return string.IsNullOrEmpty(displayName)
                    ? string.Format("[[{0}]]", _resourceKey)
                    : displayName;
            }
        }

        public string ResourceKey => _resourceKey;
        public ResourceManager ResourceManager => _resource;
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumValue)
        {
            FieldInfo? fi = enumValue.GetType().GetField(enumValue.ToString());
            if (fi == null)
                return "";

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return enumValue.ToString();
        }

        public static string GetDescription(this Enum enumValue, CultureInfo culture)
        {
            FieldInfo? fi = enumValue.GetType().GetField(enumValue.ToString());
            if (fi == null)
                return "";

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null && attributes.Length > 0)
            {
                if (attributes[0] is LocalizedDescriptionAttribute localizedDescription)
                {
                    string description = localizedDescription.Description;
                    ResourceManager resourceManager = localizedDescription.ResourceManager;
                    string? localizedDesc = resourceManager.GetString(localizedDescription.ResourceKey, culture);
                    return string.IsNullOrEmpty(localizedDesc) ? description : localizedDesc;
                }
                return attributes[0].Description;
            }
            else
                return enumValue.ToString();
        }

        public static Dictionary<int, string> GetAllEnumDescriptions<T>() where T : Enum
        {
            Dictionary<int, string> descriptions = new Dictionary<int, string>();

            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                int value = Convert.ToInt32(enumValue);
                string description = GetDescription(enumValue);
                descriptions[value] = description;
            }

            return descriptions;
        }

        //returns a list that can be translated in EF sql query
        public static List<string> GetAllEnumDescriptionsList<T>() where T : Enum
        {
            List<string> descriptions = new List<string>();

            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                int value = Convert.ToInt32(enumValue);
                string description = GetDescription(enumValue);
                descriptions.Add(value.ToString() + "." + description);
            }

            return descriptions;
        }

        public static List<string> GetAllEnumDescriptionsListWithoutNumber<T>() where T : Enum
        {
            List<string> descriptions = new List<string>();

            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                string description = GetDescription(enumValue, new CultureInfo("en"));
                descriptions.Add(description);
            }

            return descriptions;
        }

        public static string GetDisplayName(this Enum e)
        {
            var rm = new ResourceManager(typeof(SharedResources));
            var resourceDisplayName = rm.GetString(e.GetType().Name + "_" + e);

            return string.IsNullOrWhiteSpace(resourceDisplayName) ? string.Format("[[{0}]]", e) : resourceDisplayName;
        }
    }
}
