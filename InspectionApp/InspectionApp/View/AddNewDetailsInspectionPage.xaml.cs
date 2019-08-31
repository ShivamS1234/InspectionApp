using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace InspectionApp.View
{
    public partial class AddNewDetailsInspectionPage : ContentPage
    {
        public AddNewDetailsInspectionPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
