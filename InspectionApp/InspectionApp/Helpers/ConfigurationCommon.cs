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

    public static List<string> FilterArray = new List<string>()
        { "Assigned To", "Picker Order","Client", "Status" };
    public static List<string> FilterArrayStatus = new List<string>()
        { "Status Name", "Status Color","Status"};
    public static List<string> FilterArrayWharehouse = new List<string>()
        { "Warehouse", "Address","Email","Telephone"};
    public static List<string> DeleteArray = new List<string>()
        {"sure! you want to delete this?", "Delete","Okay", "Cancel" };
  }
}
