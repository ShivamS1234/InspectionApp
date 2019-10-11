using System;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace InspectionApp.Helpers
{
  public class ConfigurationCommon
  {
    public enum UserRole
    {
      Driver = 1,
      Administrator = 1,
      Client = 3
    }
    public static string CurrentFilter = string.Empty;
    public static int ApplicationId => 1;

    public static List<string> FilterHeaderArray = new List<string>()
        { "Invoice", "Total Box Quantities","Temp On Caja", "Temp On Termografo" };
    public static List<string> FilterDetailsArrayStatus = new List<string>()
      { "Size Desp", "Weight","Temperature", "QualityScore" };
    public static List<string> DeleteArray = new List<string>()
        {"sure! you want to delete this?", "Delete","Okay", "Cancel" };

    public static bool App_Online
    {
      get
      {
        var current = Connectivity.NetworkAccess;
        if (current == NetworkAccess.Internet)
          return true;
        else
          return false;
      }
    }
  }
}
