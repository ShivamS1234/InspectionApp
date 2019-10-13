using Acr.UserDialogs;
using Inspection.Resouces.DTO.Request;
using InspectionApp.Database;
using InspectionApp.Helpers;
using InspectionApp.View;
using InspectionApp.ViewModel;
using InspectionApp.WebServices;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace InspectionApp
{
    public partial class App1 : PrismApplication
    {
        public static double ScreenHeight;
        public static double ScreenWidth;
        public static bool IsInDevelopment = false;
        //static InspectionAppDatabase<Entity> database;
        #region Notification
        public static bool IsActiveNotification { get; set; }
        public static bool IsActiveApp { get; set; }
        public static string MSG_Notification { get; set; }
        #endregion

        public App1() : this(null) { }

        public App1(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            try
            {
                IsActiveApp = true;
                Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
                await NavigationService.NavigateAsync("SplashPage");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (ConfigurationCommon.App_Online)
            {
                InitData.SyncHeaderDetailsInServer();
            }
        }
        protected override void OnResume()
        {
            base.OnResume();
            App1.IsActiveApp = true;
            // TODO: Refresh network data, perform UI updates, and reacquire resources like cameras, I/O devices, etc.

        }

        protected override void OnSleep()
        {
            base.OnSleep();
            App1.IsActiveApp = false;
            // TODO: This is the time to save app data in case the process is terminated.
            // This is the perfect timing to release exclusive resources (camera, I/O devices, etc...)

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SplashPage, SplashViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RegistrationPage, RegistrationViewModel>();
            containerRegistry.RegisterForNavigation<DashboardPage, DashBoardViewModel>();
            containerRegistry.RegisterForNavigation<AddNewInspectionPage, AddNewInspectionViewModel>();
            containerRegistry.RegisterForNavigation<InspectionListPage, InspectionListViewModel>();
            containerRegistry.RegisterForNavigation<InspectionDetailsListPage, InspectionDetailsListViewModel>();
            containerRegistry.RegisterForNavigation<AddNewDetailsInspectionPage, AddNewDetailInspectionViewModel>();
        }

        #region databse
        //public static InspectionAppDatabase<Entity> Database
        //{
        //  get
        //  {
        //    if (database == null)
        //    {
        //      database = new InspectionAppDatabase<Entity>(DBPath);
        //    }
        //    return database;
        //  }
        //}
        public static string DBPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "InspectionAppDatabase.db3");
        #endregion
    }
}
