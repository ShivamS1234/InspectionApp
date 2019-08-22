using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace InspectionApp.View
{
  public partial class DashboardPage : ContentPage
  {
    public DashboardPage()
    {
      InitializeComponent();
      NavigationPage.SetHasNavigationBar(this, false);
    }
  }
}
