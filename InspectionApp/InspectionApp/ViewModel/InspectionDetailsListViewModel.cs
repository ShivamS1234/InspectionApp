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
using InspectionApp.Model;

namespace InspectionApp.ViewModel
{
    class InspectionDetailsListViewModel : BaseViewModel
    {
        #region binding_private_propertise
        INavigationService _navigationService;
        WebServiceManager webServiceManager;
        Command _loadMoreDetailsCommand, _AddNewDetail, _filterCommand;
        NavigationParameters NavigationParameters = new NavigationParameters();
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
        private ObservableCollection<InspectionDetailModel> _lstFreights;
        public ObservableCollection<InspectionDetailModel> LstDetails
        {
            get { return _lstFreights; }
            set { SetProperty(ref _lstFreights, value); }

        }
        private ObservableCollection<InspectionDetailModel> _TempLstDetail;
        public ObservableCollection<InspectionDetailModel> TempLstDetail
        {
            get { return _TempLstDetail; }
            set { SetProperty(ref _TempLstDetail, value); }
        }

        private InspectionDetail _SelectedDetails;
        public InspectionDetail SelectedDetails
        {
            get { return _SelectedDetails; }
            set { SetProperty(ref _SelectedDetails, value); }

        }

        public Command LoadMoreDetailsCommand
        {
            get
            {
                return _loadMoreDetailsCommand ?? (_loadMoreDetailsCommand = new Command(() => { ExecuteLoadMoreContainerDetailsCommandAsync(); }));

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
                                var item = (e as InspectionDetailModel);
                                LstDetails.Remove(item);
                                TempLstDetail.Remove(item);
                                await DeleteAsync(item);
                                await InitData.DetailsRepo.DeleteItemAsync(x => x.Id == item.Id);
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
                LstDetails = new ObservableCollection<InspectionDetailModel>();
                RowTapCommand = new Command(RowTap_Command);
                ConfigurationCommon.CurrentFilter = ConfigurationCommon.FilterDetailsArrayStatus[0];

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
                    if ((NavigationParameters.ContainsKey("HeaderID") || parameters.ContainsKey("HeaderID")))
                    {
                        if (parameters.ContainsKey("HeaderID"))
                        {
                            NavigationParameters = (Prism.Navigation.NavigationParameters)parameters;
                            GetAllInitData(NavigationParameters.GetValue<int>("HeaderID"));
                        }
                        else
                        {
                            GetAllInitData(NavigationParameters.GetValue<int>("HeaderID"));
                        }
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
                //InspectionDetailsRequestDTO headerRequestDTO = new InspectionDetailsRequestDTO()
                //{
                //  InspectionHeaderId = headerID,
                //};
                var result = await InitData.DetailsRepo.Get<InspectionDetailTable>(x => x.InspectionHeaderId == headerID);
                //var result = await webServiceManager.GetHeaderDetailsAll().ConfigureAwait(true);
                if (result != null)
                {
                    if (result.Count > 0)
                    {
                        LstDetails = new ObservableCollection<InspectionDetailModel>();
                        foreach (var data in result)
                        {
                            LstDetails.Add(new InspectionDetailModel
                            {
                                Id = (int)data.Id,
                                InspectionHeaderId = data.InspectionHeaderId,
                                SizeId = data.SizeId,
                                SampleSize = data.SampleSize,
                                Weight = data.Weight,
                                PhysicalCount = data.PhysicalCount,
                                OpeningApperenceId = data.OpeningApperenceId,
                                Temperature = data.Temperature,
                                Brix = data.Brix,
                                Firmness = data.Firmness,
                                SkinDamage = data.SkinDamage,
                                Color = data.Color,
                                PackageConditionId = data.PackageConditionId,
                                Comment = data.Comment,
                                QualityScore = data.QualityScore,
                            });
                        }
                        LstDetails = new ObservableCollection<InspectionDetailModel>(LstDetails.OrderBy(a => a.Id));
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

        private void ExecuteLoadMoreContainerDetailsCommandAsync()
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
            try
            {
                //Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                //{

                //  UserDialogs.Instance.ShowLoading("Loading...");
                //  var result = await webServiceManager.GetContainerStatusAsync(containerPageNumber, containerPageSize);
                //  if (result.IsSuccess)
                //  {

                //    if (result.IsSuccess)
                //    {
                //      if (result.Data.Count < 20)
                //        isMoreContainerStatus = false;
                //      var dataContainer = result.Data.GroupBy(x => x.StatusName).ToList();
                //      foreach (var data in dataContainer)
                //      {
                //        LstContainerStatus.Add(new ContainerList() { Key = data.Key, Count = data.Count() });
                //      }
                //      UserDialogs.Instance.HideLoading();
                //      containerPageNumber++;
                //      //await UserDialogs.Instance.AlertAsync("BLE Tags successfully linked", "", "Ok");
                //    }
                //  }
                //  else
                //  {
                //    await UserDialogs.Instance.AlertAsync(result.Message, "Alert", "Ok");
                //    UserDialogs.Instance.HideLoading();
                //  }
                //  if (!isContainerDetail)
                //  {
                //    isContainerDetail = true;
                //    GetContainerStatusDetailsAsync();
                //  }
                //});
            }
            catch
            {
                UserDialogs.Instance.HideLoading();

            }
        }

        public async void AddNew_Command()
        {
            //if (Convert.ToInt32(LoginPageViewModel.UserData.UserRoleId) == (int)ConfigurationCommon.UserRole.Administrator)
            //{
            UserDialogs.Instance.ShowLoading();
            int headerValue = NavigationParameters.GetValue<int>("HeaderID");
            NavigationParameters = new NavigationParameters();
            NavigationParameters.Add("HeaderID", headerValue);
            NavigationParameters.Add("ScreenRight", "New Detail Inspection");
            await _navigationService.NavigateAsync("AddNewDetailsInspectionPage", NavigationParameters);
            UserDialogs.Instance.HideLoading();
            //}
            //else
            //{
            //    await UserDialogs.Instance.AlertAsync("You are not authorized to create freight items!");
            //}
        }
        public async void RowTap_Command(object obj)
        {
            //if ((Convert.ToInt32(LoginPageViewModel.UserData.UserRoleId) == (int)ConfigurationCommon.UserRole.Administrator) || (Convert.ToInt32(LoginPageViewModel.UserData.UserRoleId) == (int)ConfigurationCommon.UserRole.Driver))
            //{
            if (obj != null)
            {
                UserDialogs.Instance.ShowLoading();
                int headerValue = NavigationParameters.GetValue<int>("HeaderID");
                NavigationParameters = new NavigationParameters();
                var selectedItem = obj as InspectionDetailModel;
                NavigationParameters.Add("HeaderID", headerValue);
                NavigationParameters.Add("InspectionDetail", selectedItem);
                NavigationParameters.Add("ScreenRight", "Edit Detail Inspection");
                await _navigationService.NavigateAsync("AddNewDetailsInspectionPage", NavigationParameters);
                UserDialogs.Instance.HideLoading();
            }
            //}
            //else
            //{
            //  await UserDialogs.Instance.AlertAsync("You are not authorized to Edit freight items!");
            //}

        }

        private async Task ShowFilterAsync()
        {
            try
            {

                var actionResult = await UserDialogs.Instance.ActionSheetAsync("Filter", "Cancel", "Clear Filter", null, ConfigurationCommon.FilterDetailsArrayStatus.ToArray());
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
                    if (ConfigurationCommon.CurrentFilter == ConfigurationCommon.FilterDetailsArrayStatus[0])
                    {
                        LstDetails = new ObservableCollection<InspectionDetailModel>(TempLstDetail.Where(x => x.SizeDescriptionName.ToLower().Contains(txt.ToLower())));
                    }
                    else if (ConfigurationCommon.CurrentFilter == ConfigurationCommon.FilterDetailsArrayStatus[1])
                    {
                        LstDetails = new ObservableCollection<InspectionDetailModel>(TempLstDetail.Where(x => x.Weight >= Convert.ToInt32(txt.ToLower())));
                    }
                    else if (ConfigurationCommon.CurrentFilter == ConfigurationCommon.FilterDetailsArrayStatus[2])
                    {
                        LstDetails = new ObservableCollection<InspectionDetailModel>(TempLstDetail.Where(x => x.Temperature >= Convert.ToInt32(txt.ToLower())));
                    }
                    else if (ConfigurationCommon.CurrentFilter == ConfigurationCommon.FilterDetailsArrayStatus[3])
                    {
                        LstDetails = new ObservableCollection<InspectionDetailModel>(TempLstDetail.Where(x => x.QualityScore >= Convert.ToInt32(txt.ToLower())));
                    }
                    else
                    {
                        LstDetails = TempLstDetail;
                    }
                }
                else
                {
                    LstDetails = TempLstDetail;
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
                var result = await webServiceManager.DeleteInspectionDetailsById(inspectionTbsRequestDTO).ConfigureAwait(true);
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


