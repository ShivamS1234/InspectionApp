using System;
using Prism.Navigation;

namespace InspectionApp.ViewModel
{
  public class AddNewInspectionViewModel : BaseViewModel
  {
    public AddNewInspectionViewModel(INavigationService navigationService) : base(navigationService)
    {
      Title = "New Inspection";
    }
  }
}
