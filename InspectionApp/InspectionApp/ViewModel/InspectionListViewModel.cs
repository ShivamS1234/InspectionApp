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
  class InspectionListViewModel : BaseViewModel
  {
    #region binding_private_propertise
    INavigationService _navigationService;
    WebServiceManager webServiceManager;
    Command _loadMoreFreightsCommand, _AddNewFreight, _filterCommand;
    private ObservableCollection<FreightTbDetails> _lstFreights;
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

    public ObservableCollection<FreightTbDetails> LstFreights
    {
      get { return _lstFreights; }
      set { SetProperty(ref _lstFreights, value); }

    }
    private ObservableCollection<FreightTbDetails> _TempLstFreights;
    public ObservableCollection<FreightTbDetails> TempLstFreights
    {
      get { return _TempLstFreights; }
      set { SetProperty(ref _TempLstFreights, value); }
    }

    private FreightTbDetails _SelectedFreights;
    public FreightTbDetails SelectedFreights
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
    public Command AddNewFreight
    {
      get
      {
        return _AddNewFreight ?? (_AddNewFreight = new Command(async () => AddNew_Command()));

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
                var item = (e as FreightTbDetails);
                LstFreights.Remove(item);
                TempLstFreights.Remove(item);
                await DeleteAsync(item);
              }
            });
          }
          return _deleteVolumeCommand;
        }

      }
    }
    #endregion


    public InspectionListViewModel(INavigationService navigationService) : base(navigationService)
    {
      try
      {
        _navigationService = navigationService;
        webServiceManager = new WebServiceManager();
        Title = "Inspections";
        LstFreights = new ObservableCollection<FreightTbDetails>();
        RowTapCommand = new Command(RowTap_Command);
        ConfigurationCommon.CurrentFilter = ConfigurationCommon.FilterArray[0];
        if (Convert.ToInt32(LoginPageViewModel.UserData.UserRoleId) == (int)ConfigurationCommon.UserRole.Administrator)
        {
          IsFreightPrices = true;
        }
        else
        {
          IsFreightPrices = false;
        }

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
        GetAllInitData();
      }
      catch (Exception ex)
      {
        UserDialogs.Instance.HideLoading();
        throw ex;
      }
    }
    #region method
    public async void GetAllInitData()
    {
      try
      {
        UserDialogs.Instance.ShowLoading("Loading...");
        InspectionHeadersRequestDTO headerRequestDTO = new InspectionHeadersRequestDTO()
        {
          CompanyId = (int)LoginPageViewModel.UserData.CompanyId,
          UserId = (int)LoginPageViewModel.UserData.Id,
        };
        var result = await webServiceManager.GetHeaderbyID(headerRequestDTO).ConfigureAwait(true);
        if (result.IsSuccess)
        {
          if (result.Data.InspectionHeader != null && result.Data.InspectionHeader.Count > 0)
          {
            LstFreights = new ObservableCollection<FreightTbDetails>();
            foreach (var data in result.Data.InspectionHeader)
            {
              //LstFreights.Add(data);
            }
            LstFreights = new ObservableCollection<FreightTbDetails>(LstFreights.OrderBy(a => a.Name));
            TempLstFreights = LstFreights;
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
      if (Convert.ToInt32(LoginPageViewModel.UserData.UserRoleId) == (int)ConfigurationCommon.UserRole.Administrator)
      {
        var parameters = new NavigationParameters();
        parameters.Add("ScreenRight", "Add New Freight");
        await _navigationService.NavigateAsync("RegistrationFreightPage", parameters);
      }
      else
      {
        await UserDialogs.Instance.AlertAsync("You are not authorized to create freight items!");
      }
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
        if (LstFreights != null && LstFreights.Count > 0)
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
          if (ConfigurationCommon.CurrentFilter == ConfigurationCommon.FilterArray[0])
          {
            LstFreights = new ObservableCollection<FreightTbDetails>(TempLstFreights.Where(x => x.Name.ToLower().Contains(txt.ToLower())));
          }
          else if (ConfigurationCommon.CurrentFilter == ConfigurationCommon.FilterArray[1])
          {
            LstFreights = new ObservableCollection<FreightTbDetails>(TempLstFreights.Where(x => x.PickupOrder.ToLower().Contains(txt.ToLower())));
          }
          else if (ConfigurationCommon.CurrentFilter == ConfigurationCommon.FilterArray[2])
          {
            LstFreights = new ObservableCollection<FreightTbDetails>(TempLstFreights.Where(x => x.ClientName.ToLower().Contains(txt.ToLower())));
          }
          else if (ConfigurationCommon.CurrentFilter == ConfigurationCommon.FilterArray[3])
          {
            LstFreights = new ObservableCollection<FreightTbDetails>(TempLstFreights.Where(x => x.Status.ToLower().Contains(txt.ToLower())));
          }
          else
          {
            LstFreights = TempLstFreights;
          }
        }
        else
        {
          LstFreights = TempLstFreights;
        }
      }
      catch
      {
        UserDialogs.Instance.HideLoading();
      }
    }

    private async Task DeleteAsync(FreightTbDetails row)
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


