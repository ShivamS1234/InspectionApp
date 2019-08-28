using System;
using System.Collections.Generic;

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
        { "Size Desp", "Weight","Temperature", "QualityScore" };
    public static List<string> FilterDetailsArrayStatus = new List<string>()
      { "Size Desp", "Weight","Temperature", "QualityScore" };
    public static List<string> DeleteArray = new List<string>()
        {"sure! you want to delete this?", "Delete","Okay", "Cancel" };
  }
}
