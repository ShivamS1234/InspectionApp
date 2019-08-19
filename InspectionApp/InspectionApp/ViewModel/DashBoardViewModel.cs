using System;
using System.Windows.Input;
using Acr.UserDialogs;
using InspectionApp.Helpers;
using InspectionApp.WebServices;
using Prism.Navigation;
using Xamarin.Forms;

namespace InspectionApp.ViewModel
{
  public class DashBoardViewModel : BaseViewModel
  {
    #region propertise
    WebServiceManager webServiceManager;
    INavigationService _navigationService;
    Command _FreightCommand, _StatusCommand, _WarehouseCommand, _UsersCommand, _logoutCommand;

    public ICommand LoginCommand { get; set; }

    public Command FreightCommand
    {
      get
      {
        return _FreightCommand ?? (_FreightCommand = new Command(() => { ExecuteFreightCommandAsync(); }));
      }
    }
    public Command StatusCommand
    {
      get
      {
        return _StatusCommand ?? (_StatusCommand = new Command(() => { ExecuteStatusCommandAsync(); }));
      }
    }
    public Command WarehouseCommand
    {
      get
      {
        return _WarehouseCommand ?? (_WarehouseCommand = new Command(() => { ExecuteWarehouseCommandAsync(); }));
      }
    }
    public Command UsersCommand
    {
      get
      {
        return _UsersCommand ?? (_UsersCommand = new Command(() => { ExecuteUsersCommandAsync(); }));
      }
    }

    #endregion

    public DashBoardViewModel(INavigationService navigationService) : base(navigationService)
    {
      _navigationService = navigationService;
      LoginCommand = new Command(async () => ExecuteLogoutCommandAsync());
      Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
      {
        //InitData.GetAllInitData();
      });
    }
    private async void ExecuteLogoutCommandAsync()
    {
      try
      {
        //await _navigationService.NavigateAsync("/LoginPage");
        var status = await _navigationService.GoBackToRootAsync();
      }
      catch (Exception ex)
      {
        throw ex;
      }

    }
    private async void ExecuteFreightCommandAsync()
    {
      await _navigationService.NavigateAsync("FreightListPage");

    }
    private async void ExecuteStatusCommandAsync()
    {

      if (Convert.ToInt32(LoginPageViewModel.UserData.UserRoleId) == (int)ConfigurationCommon.UserRole.Administrator)
      {
        await _navigationService.NavigateAsync("StatusListPage");
      }
      else
      {
        await UserDialogs.Instance.AlertAsync("You are not authorized to access this screen!");
      }
    }
    private async void ExecuteWarehouseCommandAsync()
    {
      if (Convert.ToInt32(LoginPageViewModel.UserData.UserRoleId) == (int)ConfigurationCommon.UserRole.Administrator)
      {
        await _navigationService.NavigateAsync("WarehosueListPage");
      }
      else
      {
        await UserDialogs.Instance.AlertAsync("You are not authorized to access this screen!");
      }

    }
    private async void ExecuteUsersCommandAsync()
    {
      //await _navigationService.NavigateAsync("/NavigationPage/Login");
    }

  }
}
