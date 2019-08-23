using System;
using Prism.Navigation;
using Xamarin.Forms;

namespace InspectionApp.ViewModel
{
  public class AddNewInspectionViewModel : BaseViewModel
  {
    public Command _DetailsList;
    INavigationService _navigationService;
    public AddNewInspectionViewModel(INavigationService navigationService) : base(navigationService)
    {
      Title = "New Inspection";
      _navigationService = navigationService;
    }
    public override void OnNavigatedTo(INavigationParameters parameters)
    {
    }
    public Command DetailsList
    {
      get
      {
        return _DetailsList ?? (_DetailsList = new Command(async () => AddNew_Command()));
      }

    }
    public async void AddNew_Command()
    {
      await _navigationService.NavigateAsync("InspectionDetailslistPage");
    }
  }
}
