using System;
using System.Collections.Generic;
using System.Windows.Input;
using Acr.UserDialogs;
using Inspection.Resouces.DTO.Request;
using Inspection.Resouces.DTO.Response;
using InspectionApp.WebServices;
using Prism.Navigation;
using Xamarin.Forms;
using Inspection.Resouces.Entites;
using InspectionApp.Helpers;

namespace InspectionApp.ViewModel
{
  public class RegistrationViewModel : BaseViewModel
  {
    #region propertise
    WebServiceManager webServiceManager;
    INavigationService navigationService;
    private string name;
    public string Name
    {
      get { return name; }
      set { SetProperty(ref name, value); }
    }
    private string address;
    public string Address
    {
      get { return address; }
      set { SetProperty(ref address, value); }
    }
    private string password;
    public string Password
    {
      get { return password; }
      set { SetProperty(ref password, value); }
    }
    private string phoneNo;
    public string PhoneNo
    {
      get { return phoneNo; }
      set { SetProperty(ref phoneNo, value); }
    }
    private string email;
    public string Email
    {
      get { return email; }
      set { SetProperty(ref email, value); }
    }
    private Color passwordColor;
    public Color PasswordColor
    {
      get { return passwordColor; }
      set { SetProperty(ref passwordColor, value); }
    }
    private Color emailColor;
    public Color EmailColor
    {
      get { return emailColor; }
      set { SetProperty(ref emailColor, value); }
    }
    public ICommand OkayCommand { get; set; }

    CompaniesResponseDTO companiesResponseDTO = new CompaniesResponseDTO();
    private IList<Inspection.Resouces.Entites.Company> _CompiniesList;
    public IList<Inspection.Resouces.Entites.Company> CompiniesList
    {
      get { return _CompiniesList; }
      set { SetProperty(ref _CompiniesList, value); }
    }
    private Company _SelectedCompany;
    public Company SelectedCompany
    {
      get { return _SelectedCompany; }
      set { SetProperty(ref _SelectedCompany, value); }
    }
    #endregion

    public RegistrationViewModel(INavigationService _navigationService) : base(_navigationService)
    {
      navigationService = _navigationService;
      webServiceManager = new WebServiceManager();
      Title = "Registration Page";
      OkayCommand = new Command(async () => Okay_Command());
      GetAllInitData();
    }

    public async void Okay_Command()
    {
      if (Name == null || Name == "")
      {
        await App.Current.MainPage.DisplayAlert("Alert ", "Please Enter user name!", "ok");
      }
      else if (Password == null || Password == "")
      {
        await App.Current.MainPage.DisplayAlert("Alert", "Please Enter Password!", "ok");
      }
      else if (PhoneNo == null || PhoneNo == "")
      {
        await App.Current.MainPage.DisplayAlert("Alert", "Please Enter PhoneNo!", "ok");
      }
      else if (Email == null || Email == "")
      {
        await App.Current.MainPage.DisplayAlert("Alert", "Please Enter Email!", "ok");
      }
      //else if (Address == null || Address == "")
      //{
      //    await App.Current.MainPage.DisplayAlert("Alert", "Please Enter Address!", "ok");
      //}
      else if (Color.Red == EmailColor)
      {
        await App.Current.MainPage.DisplayAlert("Alert", "Email is not in correct format!", "ok");
      }
      else if (Color.Red == PasswordColor)
      {
        await App.Current.MainPage.DisplayAlert("Alert", "Password is not in correct format!", "ok");
      }
      else
      {
        try
        {
          UserDialogs.Instance.ShowLoading("Loading...");
          UsersRequestDTO userRequestDTO = new UsersRequestDTO()
          {
            CompanyId = SelectedCompany.Id,
            UserName = Name,
            Email = Email,
            UserRoleId = 0,
            PhoneNumber = PhoneNo,
            UserPwd = Password,
            IsActive = true
          };
          var result = await webServiceManager.RegistrationAsync(userRequestDTO).ConfigureAwait(true); ;
          if (result.IsSuccess && result.Data.StatusCode == 0)
          {
            //await navigationService.NavigateAsync("LoginPage");
            await navigationService.GoBackAsync();
          }
          else
          {
            await App.Current.MainPage.DisplayAlert("Alert", result.Data.Status, "ok");
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

    public virtual void OnNavigatedFrom(NavigationParameters parameters)
    {
      // here is the place you would require to handle the Back button event, 
      // this is fired every-time, user tries to leave the view. 
    }

    public virtual void OnNavigatedTo(NavigationParameters parameters)
    {
      // fired upon view load
    }
    public virtual bool OnBackButtonPressed()
    {
      //false is default value when system call back press
      return false;
    }
    /// <summary>
    /// called when page need override soft back button
    /// </summary>
    public virtual void OnSoftBackButtonPressed() { }

    #region method
    public async void GetAllInitData()
    {
      try
      {
        UserDialogs.Instance.ShowLoading("Loading...");

        CompaniesRequestDTO companiesRequestDTO = new CompaniesRequestDTO()
        {
          ApplicationId = 0
        };
        var resultApplication = await webServiceManager.GetCompaniesbyAppIdAsync(companiesRequestDTO).ConfigureAwait(true); ;
        CompiniesList = resultApplication.Data.Companies;

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
    #endregion

  }
}
