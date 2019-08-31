using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace InspectionApp.View
{
    public partial class InspectionDetailsListPage : ContentPage
    {
        public InspectionDetailsListPage()
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
