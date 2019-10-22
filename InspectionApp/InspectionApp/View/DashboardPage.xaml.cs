using System;
using System.Collections.Generic;
using InspectionApp.ViewModel;
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

    //protected override void OnAppearing()
    //{
    //  base.OnAppearing();
    //  var data = this.BindingContext as DashBoardViewModel;

    //}
  }
}
