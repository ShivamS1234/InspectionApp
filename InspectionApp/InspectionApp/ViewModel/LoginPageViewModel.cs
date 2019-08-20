
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using Prism.Ioc;
using System.Collections;
using Acr.UserDialogs;
using Inspection.Resouces.DTO.Request;
using InspectionApp.WebServices;
using Inspection.Resouces.Entites;
using InspectionApp.Helpers;
using Inspection.Resouces.Entites.CustomModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace InspectionApp.ViewModel
{
  class LoginPageViewModel : BaseViewModel
  {
    #region binding_private_propertise
    INavigationService _navigationService;
    WebServiceManager webServiceManager;
    public static User UserData = new User();
    private string userid { get; set; }
    public string UserId
    {
      get { return userid; }
      set { userid = value; OnPropertyChanged(UserId); }
    }

    private string password { get; set; }
    public string Password
    {
      get { return password; }
      set { password = value; OnPropertyChanged(Password); }
    }

    private bool _isSwitchToggled = false;
    public bool IsSwitchedToggled
    {
      get { return _isSwitchToggled; }
      set
      {
        _isSwitchToggled = value;
        OnPropertyChanged(nameof(IsSwitchedToggled));
      }
    }
    private Color emailColor;
    public Color EmailColor
    {
      get { return emailColor; }
      set { SetProperty(ref emailColor, value); }
    }

    public ICommand LoginCommand { get; set; }

    public ICommand ForgetCommand { get; set; }

    public ICommand RegisterCommand { get; set; }

    #endregion

    public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
    {
      _navigationService = navigationService;
      webServiceManager = new WebServiceManager();
      //Title = "Login";

      UserId = Convert.ToString(RememberMe.Get("username"));
      Password = Convert.ToString(RememberMe.Get("password"));
      IsSwitchedToggled = (Boolean)RememberMe.Get("isSwitchedToggled", true);
      LoginCommand = new Command(async () => Login_Command());
      ForgetCommand = new Command(async () => Forget_Command());
      RegisterCommand = new Command(async () => Register_Command());
      //UserId = "Shivammcu@gmail.com";
      //Password = "Shivam@1234";
    }

    public async void Login_Command()
    {
      if (UserId == null || userid == "")
      {
        App.Current.MainPage.DisplayAlert("Alert ", "Please Enter EmailID", "ok");
      }
      else if (Password == null || Password == "")
      {
        App.Current.MainPage.DisplayAlert("Alert", "Please Enter Password", "ok");
      }
      else if (Color.Red == EmailColor)
      {
        await App.Current.MainPage.DisplayAlert("Alert", "Email is not in correct format!", "ok");
      }
      else
      {
        try
        {
          UserDialogs.Instance.ShowLoading("Loading...");
          UsersRequestDTO clientRequestDTO = new UsersRequestDTO()
          {
            Email = UserId,
            UserPwd = Password,
            //ApplicationId = ConfigurationCommon.ApplicationId,
            //DeviceToken = RememberMe.Get("PushDeviceToken").ToString(),
            //DeviceName = Device.RuntimePlatform,
          };
          var result = await webServiceManager.LoginAsync(clientRequestDTO).ConfigureAwait(true); ;
          if (result.IsSuccess && string.IsNullOrEmpty(result.Data.Status))
          {
            UserData = new User()
            {
              CompanyId = result.Data._Users.CompanyId,
              //ApplicationId = result.Data._Users.ApplicationId,
              UserRoleId = result.Data._Users.UserRoleId,
              Id = result.Data._Users.Id
            };
            RememberMe.Set("userID", result.Data._Users.Id);
            RememberMe.Set("CmpID", result.Data._Users.CompanyId);
            if (IsSwitchedToggled)
            {
              RememberMe.Set("username", this.UserId);
              RememberMe.Set("password", this.Password);
            }
            RememberMe.Set("isSwitchedToggled", IsSwitchedToggled);
            await _navigationService.NavigateAsync("DashboardPage");
          }
          else
          {
            App.Current.MainPage.DisplayAlert("Alert ", result.Data.Status, "ok");
          }
        }
        catch (Exception ex)
        {
          UserDialogs.Instance.HideLoading();
          throw ex;
        }
        finally
        {
          UserDialogs.Instance.HideLoading();
        }
      }
    }

    public async void Forget_Command()
    {
      App.Current.MainPage.DisplayAlert("Alert ", "Forgot password is under working..!", "ok");
    }

    public async void Register_Command()
    {
      await _navigationService.NavigateAsync("RegistrationPage");
    }
  }
}

