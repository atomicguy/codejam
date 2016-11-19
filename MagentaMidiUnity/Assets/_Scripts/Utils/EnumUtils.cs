using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Linq;

/// <summary>
/// Helper class for enums
/// </summary>
public static class EnumUtils
{
    public static string stringValueOf(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (attributes.Length > 0)
        {
            return attributes[0].Description;
        }
        else
        {
            return value.ToString();
        }
    }

    public static object enumValueOf(string value, Type enumType)
    {
        string[] names = Enum.GetNames(enumType);
        foreach (string name in names)
        {
            if (stringValueOf((Enum)Enum.Parse(enumType, name)).Equals(value))
            {
                return Enum.Parse(enumType, name);
            }
        }

        throw new ArgumentException("The string is not a description or value of the specified enum.");
    }

    public static IEnumerable<T> GetValues<T>()
    {
        return (T[])Enum.GetValues(typeof(T));
    }

    public static bool IsDefined<T>(T value)
    {
        return Enum.IsDefined(typeof(T), value);
    }
}