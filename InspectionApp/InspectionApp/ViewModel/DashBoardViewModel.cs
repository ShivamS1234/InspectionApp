using System;
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
using System.Collections.ObjectModel;
using Inspection.Resouces.DTO.Response;
using Inspection.Resouces.Entites.CustomModel;
using System.Threading.Tasks;
using InspectionApp.Helpers;
using System.Linq;

namespace InspectionApp.ViewModel
{
    class DashBoardViewModel : BaseViewModel
    {
        #region binding_private_propertise
        INavigationService _navigationService;
        WebServiceManager webServiceManager;
        Command _AddNewInspection, _InspectionList;
        int UserRoleId;
        public Command AddNewInspection
        {
            get
            {
                return _AddNewInspection ?? (_AddNewInspection = new Command(async () => AddNew_Command()));
            }

        }
        public Command InspectionList
        {
            get
            {
                return _InspectionList ?? (_InspectionList = new Command(async () => Inspection_List()));
            }

        }
        public ICommand LoginCommand { get; set; }
        #endregion


        public DashBoardViewModel(INavigationService navigationService) : base(navigationService)
        {
            try
            {
                _navigationService = navigationService;
                webServiceManager = new WebServiceManager();
                Title = "Inspections";
                LoginCommand = new Command(async () => ExecuteLogoutCommandAsync());
                UserRoleId = Convert.ToInt32(RememberMe.Get("UserRoleId"));
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    InitData.GetAllInitData();
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
        }
        #region method
        private async void ExecuteLogoutCommandAsync()
        {
            try
            {
                //await _navigationService.NavigateAsync("/LoginPage");
                //await _navigationService.NavigateAsync("NavigationPage/LoginPage");
                var status = await _navigationService.GoBackAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async void AddNew_Command()
        {
            if (UserRoleId == (int)ConfigurationCommon.UserRole.Administrator)
            {
                await _navigationService.NavigateAsync("AddNewInspectionPage");
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("You are not authorized to create freight items!");
            }
        }
        public async void Inspection_List()
        {
            if (UserRoleId == (int)ConfigurationCommon.UserRole.Administrator)
            {
                //var parameters = new NavigationParameters();
                //parameters.Add("ScreenRight", "Add New Freight");
                //await _navigationService.NavigateAsync("RegistrationFreightPage", parameters);
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("You are not authorized to create freight items!");
            }
        }
        #endregion
    }
}


