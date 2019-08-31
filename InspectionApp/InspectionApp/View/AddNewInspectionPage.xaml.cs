using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace InspectionApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewInspectionPage : ContentPage
    {
        public AddNewInspectionPage()
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
