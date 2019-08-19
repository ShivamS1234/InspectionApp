using System;
namespace InspectionApp.Helpers
{
  public class RememberMe
  {
    public static object Get(string key, bool TypeBool = false)
    {
      var properties = Xamarin.Forms.Application.Current.Properties;
      if (properties.ContainsKey(key))
      {
        return properties[key];
      }
      else
      {
        if (TypeBool)
          return false;
        else
          return "";
      }
    }

    public static void Set(string key, object value)
    {
      var properties = Xamarin.Forms.Application.Current.Properties;
      if (!properties.ContainsKey(key))
      {
        properties.Add(key, value);
      }
      else
      {
        properties[key] = value;
      }
    }
  }
}
