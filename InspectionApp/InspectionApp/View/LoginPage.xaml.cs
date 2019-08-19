using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace InspectionApp.View
{
  public partial class LoginPage : ContentPage
  {
    public LoginPage()
    {
      InitializeComponent();
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
      Password.IsPassword = Password.IsPassword ? false : true;

    }
  }
}
