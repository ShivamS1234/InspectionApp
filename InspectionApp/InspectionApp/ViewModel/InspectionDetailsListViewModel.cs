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
using Inspection.Resouces.Entites;

namespace InspectionApp.ViewModel
{
    class InspectionDetailsListViewModel : BaseViewModel
    {
        #region binding_private_propertise
        INavigationService _navigationService;
        WebServiceManager webServiceManager;
        Command _loadMoreFreightsCommand, _AddNewDetail, _filterCommand;

        bool isMoreFreights = true;

        public string _searchTxt;
        public string SearchTxt
        {
            get { return _searchTxt; }
            set
            {
                SetProperty(ref _searchTxt, value);
                SearchAsync(_searchTxt);
            }

        }
        private ObservableCollection<InspectionDetail> _lstFreights;
        public ObservableCollection<InspectionDetail> LstDetails
        {
            get { return _lstFreights; }
            set { SetProperty(ref _lstFreights, value); }

        }
        private ObservableCollection<InspectionDetail> _TempLstDetail;
        public ObservableCollection<InspectionDetail> TempLstDetail
        {
            get { return _TempLstDetail; }
            set { SetProperty(ref _TempLstDetail, value); }
        }

        private InspectionDetail _SelectedFreights;
        public InspectionDetail SelectedFreights
        {
            get { return _SelectedFreights; }
            set { SetProperty(ref _SelectedFreights, value); }

        }
        private Boolean _IsFreightPrices;
        public Boolean IsFreightPrices
        {
            get { return _IsFreightPrices; }
            set { SetProperty(ref _IsFreightPrices, value); }

        }
        public Command LoadMoreFreightsCommand
        {
            get
            {
                return _loadMoreFreightsCommand ?? (_loadMoreFreightsCommand = new Command(() => { ExecuteLoadMoreContainerStatusCommandAsync(); }));

            }

        }
        public Command AddNewDetail
        {
            get
            {
                return _AddNewDetail ?? (_AddNewDetail = new Command(async () => AddNew_Command()));

            }

        }
        public ICommand RowTapCommand { get; set; }
        public Command FilterCommand
        {
            get
            {
                return _filterCommand ?? (_filterCommand = new Command(async () => await ShowFilterAsync()));
            }
        }
        private ICommand _deleteVolumeCommand;
        public ICommand DeleteVolumeCommand
        {
            get
            {
                {
                    if (_deleteVolumeCommand == null)
                    {
                        _deleteVolumeCommand = new Command(async (e) =>
                        {
                            var status = await UserDialogs.Instance.ConfirmAsync(ConfigurationCommon.DeleteArray[0], ConfigurationCommon.DeleteArray[1], ConfigurationCommon.DeleteArray[2], ConfigurationCommon.DeleteArray[3]);
                            if (status)
                            {
                                var item = (e as InspectionDetail);
                                LstDetails.Remove(item);
                                TempLstDetail.Remove(item);
                                await DeleteAsync(item);
                            }
                        });
                    }
                    return _deleteVolumeCommand;
                }

            }
        }
        #endregion


        public InspectionDetailsListViewModel(INavigationService navigationService) : base(navigationService)
        {
            try
            {
                _navigationService = navigationService;
                webServiceManager = new WebServiceManager();
                Title = "Inspections Details";
                LstDetails = new ObservableCollection<InspectionDetail>();
                RowTapCommand = new Command(RowTap_Command);
                ConfigurationCommon.CurrentFilter = ConfigurationCommon.FilterArray[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                UserDialogs.Instance.ShowLoading();
                if (parameters != null)
                {
                    if (parameters.ContainsKey("HeaderID"))
                    {
                        GetAllInitData(parameters.GetValue<int>("HeaderID"));
                    }
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                throw ex;
            }
        }
        #region method
        public async void GetAllInitData(int headerID)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading...");
                InspectionDetailsRequestDTO headerRequestDTO = new InspectionDetailsRequestDTO()
                {
                    InspectionHeaderId = headerID,
                };
                var result = await webServiceManager.GetHeaderDetailsbyID(headerRequestDTO).ConfigureAwait(true);
                if (result.IsSuccess)
                {
                    if (result.Data.InspectionDetail != null && result.Data.InspectionDetail.Count > 0)
                    {
                        LstDetails = new ObservableCollection<InspectionDetail>();
                        foreach (var data in result.Data.InspectionDetail)
                        {
                            LstDetails.Add(data);
                        }
                        LstDetails = new ObservableCollection<InspectionDetail>(LstDetails.OrderBy(a => a.Id));
                        TempLstDetail = LstDetails;
                    }
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                throw ex;
            }

        }

        private void ExecuteLoadMoreContainerStatusCommandAsync()
        {
            if (isMoreFreights)
            {
                GetFreights();
            }
            else
                return;
        }
        private void GetFreights()
        {
            //try
            //{
            //  Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            //  {

            //    UserDialogs.Instance.ShowLoading("Loading...");
            //    var result = await webServiceManager.GetContainerStatusAsync(containerPageNumber, containerPageSize);
            //    if (result.IsSuccess)
            //    {

            //      if (result.IsSuccess)
            //      {
            //        if (result.Data.Count < 20)
            //          isMoreContainerStatus = false;
            //        var dataContainer = result.Data.GroupBy(x => x.StatusName).ToList();
            //        foreach (var data in dataContainer)
            //        {
            //          LstContainerStatus.Add(new ContainerList() { Key = data.Key, Count = data.Count() });
            //        }
            //        UserDialogs.Instance.HideLoading();
            //        containerPageNumber++;
            //        //await UserDialogs.Instance.AlertAsync("BLE Tags successfully linked", "", "Ok");
            //      }
            //    }
            //    else
            //    {
            //      await UserDialogs.Instance.AlertAsync(result.Message, "Alert", "Ok");
            //      UserDialogs.Instance.HideLoading();
            //    }
            //    if (!isContainerDetail)
            //    {
            //      isContainerDetail = true;
            //      GetContainerStatusDetailsAsync();
            //    }
            //  });
            //}
            //catch
            //{
            //  UserDialogs.Instance.HideLoading();

            //}
        }

        public async void AddNew_Command()
        {
            //if (Convert.ToInt32(LoginPageViewModel.UserData.UserRoleId) == (int)ConfigurationCommon.UserRole.Administrator)
            //{
            var parameters = new NavigationParameters();
            parameters.Add("ScreenRight", "Add New Freight");
            await _navigationService.NavigateAsync("AddNewDetailsInspectionPage", parameters);
            //}
            //else
            //{
            //    await UserDialogs.Instance.AlertAsync("You are not authorized to create freight items!");
            //}
        }
        public async void RowTap_Command(object obj)
        {
            if ((Convert.ToInt32(LoginPageViewModel.UserData.UserRoleId) == (int)ConfigurationCommon.UserRole.Administrator) || (Convert.ToInt32(LoginPageViewModel.UserData.UserRoleId) == (int)ConfigurationCommon.UserRole.Driver))
            {
                if (obj != null)
                {
                    UserDialogs.Instance.ShowLoading();
                    var selectedItem = obj as FreightTbDetails;
                    var parameters = new NavigationParameters();
                    parameters.Add("FreightDetail", selectedItem);
                    parameters.Add("ScreenRight", "Edit Freight");
                    await _navigationService.NavigateAsync("RegistrationFreightPage", parameters);
                    UserDialogs.Instance.HideLoading();
                }
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("You are not authorized to Edit freight items!");
            }

        }

        private async Task ShowFilterAsync()
        {
            try
            {

                var actionResult = await UserDialogs.Instance.ActionSheetAsync("Filter", "Cancel", "Clear Filter", null, ConfigurationCommon.FilterArray.ToArray());
                UserDialogs.Instance.ShowLoading();
                if (LstDetails != null && LstDetails.Count > 0)
                {
                    ConfigurationCommon.CurrentFilter = actionResult;
                }

            }
            catch
            {
                UserDialogs.Instance.HideLoading();

            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
        private async Task SearchAsync(string txt)
        {
            try
            {
                if (txt.Trim().Length > 0)
                {
                    //if (ConfigurationCommon.CurrentFilter == ConfigurationCommon.FilterArray[0])
                    //{
                    //    LstFreights = new ObservableCollection<FreightTbDetails>(TempLstDetail.Where(x => x.Name.ToLower().Contains(txt.ToLower())));
                    //}
                    //else if (ConfigurationCommon.CurrentFilter == ConfigurationCommon.FilterArray[1])
                    //{
                    //    LstFreights = new ObservableCollection<FreightTbDetails>(TempLstDetail.Where(x => x.PickupOrder.ToLower().Contains(txt.ToLower())));
                    //}
                    //else if (ConfigurationCommon.CurrentFilter == ConfigurationCommon.FilterArray[2])
                    //{
                    //    LstFreights = new ObservableCollection<FreightTbDetails>(TempLstDetail.Where(x => x.ClientName.ToLower().Contains(txt.ToLower())));
                    //}
                    //else if (ConfigurationCommon.CurrentFilter == ConfigurationCommon.FilterArray[3])
                    //{
                    //    LstFreights = new ObservableCollection<FreightTbDetails>(TempLstDetail.Where(x => x.Status.ToLower().Contains(txt.ToLower())));
                    //}
                    //else
                    //{
                    //    LstFreights = TempLstFreights;
                    //}
                }
                else
                {
                    //LstFreights = TempLstFreights;
                }
            }
            catch
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private async Task DeleteAsync(InspectionDetail row)
        {
            try
            {

                UserDialogs.Instance.ShowLoading("Deleting...");
                InspectionHeadersRequestDTO inspectionTbsRequestDTO = new InspectionHeadersRequestDTO()
                {
                    Id = row.Id,
                    //IsActive = false
                };
                var result = await webServiceManager.DeleteInspectionById(inspectionTbsRequestDTO).ConfigureAwait(true);
                if (result.IsSuccess)
                {
                    //write here logic for the after deleting row.
                }

            }
            catch
            {
                UserDialogs.Instance.HideLoading();
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
        #endregion
    }
}


