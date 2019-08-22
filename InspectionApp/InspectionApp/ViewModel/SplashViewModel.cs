using System;
using System.Threading.Tasks;
using InspectionApp.Helpers;
using Prism.Navigation;
using Xamarin.Forms;

namespace InspectionApp.ViewModel
{
  public class SplashViewModel : BaseViewModel
  {
    INavigationService _navigationService;
    public SplashViewModel(INavigationService navigationService) : base(navigationService)
    {
      _navigationService = navigationService;
      Device.BeginInvokeOnMainThread(async () =>
      {
        await Task.Delay(5000);
        await _navigationService.NavigateAsync("NavigationPage/LoginPage");

      });
    }
  }
}
