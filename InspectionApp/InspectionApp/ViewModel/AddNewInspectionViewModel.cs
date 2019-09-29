using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs;
using Inspection.Resouces.DTO.Request;
using Inspection.Resouces.Entites;
using InspectionApp.Helpers;
//using InspectionApp.Model;
using InspectionApp.WebServices;
using Prism.Navigation;
using Xamarin.Forms;

namespace InspectionApp.ViewModel
{
    public class AddNewInspectionViewModel : BaseViewModel
    {
        public Command _DetailsList, _ClearCommand, _EditCommand, _ViewCommand;
        INavigationService _navigationService;
        WebServiceManager webServiceManager;
        InspectionApp.Model.InspectionHeaderModel _InspectionHeader;
        private IList<Company> _CompiniesList;
        public IList<Company> CompiniesList
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
        private string _Invoice;
        public string Invoice
        {
            get { return _Invoice; }
            set { SetProperty(ref _Invoice, value); }
        }
        private string _toolboxText = "Edit";
        public string ToolboxText
        {
            get { return _toolboxText; }
            set { SetProperty(ref _toolboxText, value); }
        }
        private IList<Product> _ProductList;
        public IList<Product> ProductList
        {
            get { return _ProductList; }
            set { SetProperty(ref _ProductList, value); }
        }
        private Product _SelectedProduct;
        public Product SelectedProduct
        {
            get { return _SelectedProduct; }
            set { SetProperty(ref _SelectedProduct, value); }
        }
        private IList<Variety> _VarietyList;
        public IList<Variety> VarietyList
        {
            get { return _VarietyList; }
            set { SetProperty(ref _VarietyList, value); }
        }
        private Variety _SelectedVariety;
        public Variety SelectedVariety
        {
            get { return _SelectedVariety; }
            set { SetProperty(ref _SelectedVariety, value); }
        }
        private IList<Brand> _BrandList;
        public IList<Brand> BrandList
        {
            get { return _BrandList; }
            set { SetProperty(ref _BrandList, value); }
        }
        private Brand _SelectedBrand;
        public Brand SelectedBrand
        {
            get { return _SelectedBrand; }
            set { SetProperty(ref _SelectedBrand, value); }
        }
        private IList<CountryofOrigin> _CountryofOriginList;
        public IList<CountryofOrigin> CountryofOriginList
        {
            get { return _CountryofOriginList; }
            set { SetProperty(ref _CountryofOriginList, value); }
        }
        private CountryofOrigin _SelectedCountryofOrigin;
        public CountryofOrigin SelectedCountryofOrigin
        {
            get { return _SelectedCountryofOrigin; }
            set { SetProperty(ref _SelectedCountryofOrigin, value); }
        }
        private int _TotalBoxQua;
        public int TotalBoxQua
        {
            get { return _TotalBoxQua; }
            set { SetProperty(ref _TotalBoxQua, value); }
        }
        private decimal _TempOnCaja;
        public decimal TempOnCaja
        {
            get { return _TempOnCaja; }
            set { SetProperty(ref _TempOnCaja, value); }
        }
        private decimal _TempOnTermografo;
        public decimal TempOnTermografo
        {
            get { return _TempOnTermografo; }
            set { SetProperty(ref _TempOnTermografo, value); }
        }
        private Boolean _IsEditHeader = true;
        public Boolean IsEditHeader
        {
            get { return _IsEditHeader; }
            set { SetProperty(ref _IsEditHeader, value); }
        }
        private Boolean _IsEditable = false;
        public Boolean IsEditable
        {
            get { return _IsEditable; }
            set { SetProperty(ref _IsEditable, value); }
        }
        private Boolean _IsView = true;
        public Boolean IsView
        {
            get { return _IsView; }
            set { SetProperty(ref _IsView, value); }
        }
        private PalletCondition _SelectedPalletizingCondition;
        public PalletCondition SelectedPalletizingCondition
        {
            get { return _SelectedPalletizingCondition; }
            set { SetProperty(ref _SelectedPalletizingCondition, value); }
        }

        public AddNewInspectionViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "New Inspection";
            _navigationService = navigationService;
            webServiceManager = new WebServiceManager();
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading...");
                if (parameters != null)
                {
                    if (parameters.ContainsKey("ScreenRight"))
                    {
                        Title = parameters.GetValue<string>("ScreenRight");
                        if (Title.Contains("New"))
                        {
                            IsEditHeader = false;
                            IsEditable = true;
                            IsView = false;
                        }
                    }
                    if (parameters.ContainsKey("InspectionHeader"))
                    {
                        _InspectionHeader = parameters["InspectionHeader"] as InspectionApp.Model.InspectionHeaderModel;
                        if (_InspectionHeader != null)
                        {
                            SelectedCompany = InitData.CmpList.Where(x => x.Id == _InspectionHeader.CompanyId).FirstOrDefault();
                            Invoice = _InspectionHeader.Invoice;
                            SelectedProduct = InitData.ProductList.Where(x => x.Id == _InspectionHeader.ProducerId).FirstOrDefault();
                            SelectedVariety = InitData.VarietyList.Where(x => x.Id == _InspectionHeader.VarietyId).FirstOrDefault();
                            SelectedBrand = InitData.BrandList.Where(x => x.Id == _InspectionHeader.BrandId).FirstOrDefault();
                            SelectedCountryofOrigin = InitData.CountryofOriginList.Where(x => x.Id == _InspectionHeader.CountryofOriginId).FirstOrDefault();
                            TotalBoxQua = _InspectionHeader.TotalBoxQuantities;
                            TempOnCaja = _InspectionHeader.TempOnCaja;
                            TempOnTermografo = _InspectionHeader.TempOnTermografo;
                            SelectedPalletizingCondition = InitData.PalletConditionList.Where(x => x.Id == _InspectionHeader.PalletizingConditionId).FirstOrDefault();
                        }
                    }
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

        #region Declare_Command
        public Command DetailsList
        {
            get
            {
                return _DetailsList ?? (_DetailsList = new Command(async () => AddNew_Command()));
            }
        }
        public Command ClearCommand
        {
            get
            {
                return _ClearCommand ?? (_ClearCommand = new Command(async () => Clear_Command()));
            }
        }
        public Command EditCommand
        {
            get
            {
                return _EditCommand ?? (_EditCommand = new Command(async () => Edit_Command()));
            }
        }
        public Command ViewCommand
        {
            get
            {
                return _ViewCommand ?? (_ViewCommand = new Command(async () => View_Command()));
            }
        }
        #endregion

        public async void AddNew_Command()
        {
            if (SelectedCompany == null)
            {
                await App.Current.MainPage.DisplayAlert("Alert ", "Please Select Company Name!", "ok");
            }
            else if (string.IsNullOrEmpty(Invoice))
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please Enter Invoice!", "ok");
            }
            else if (SelectedProduct == null)
            {
                await App.Current.MainPage.DisplayAlert("Alert ", "Please Select Product Name!", "ok");
            }
            else if (SelectedVariety == null)
            {
                await App.Current.MainPage.DisplayAlert("Alert ", "Please Select Variety Name!", "ok");
            }
            else if (SelectedBrand == null)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please Select Brand Name!", "ok");
            }
            else if (SelectedCountryofOrigin == null)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please Select CountryofOrigin Name!", "ok");
            }
            else if (TotalBoxQua == 0)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please Enter Total Box Quantities!", "ok");
            }
            else if (TempOnCaja == 0)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please Enter Temp On Caja!", "ok");
            }
            else if (TempOnTermografo == 0)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please Enter Temp On Termografo!", "ok");
            }
            else if (SelectedPalletizingCondition == null)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please Select Palletizing Condition!", "ok");
            }
            else
            {
                try
                {
                    UserDialogs.Instance.ShowLoading("Loading...");
                    InspectionHeadersRequestDTO inspectionHeaderRequestDTO = new InspectionHeadersRequestDTO()
                    {
                        Id = _InspectionHeader != null ? _InspectionHeader.Id : 0,
                        CompanyId = SelectedCompany.Id,
                        InspectionDate = DateTime.Now,
                        UserId = Convert.ToInt32(RememberMe.Get("userID")),
                        Invoice = Invoice,
                        ProducerId = SelectedProduct.Id,
                        //ProducerId = SelectedProduct.Id,
                        //VarietyId = 7,
                        VarietyId = SelectedVariety.Id,
                        //BrandId = 8,
                        BrandId = SelectedBrand.Id,
                        //CountryofOriginId = 9,
                        CountryofOriginId = SelectedCountryofOrigin.Id,
                        TotalBoxQuantities = TotalBoxQua,
                        TempOnCaja = TempOnCaja,
                        TempOnTermografo = TempOnTermografo,
                        PalletizingConditionId = SelectedPalletizingCondition.Id,

                    };
                    var result = await webServiceManager.RegistrationInspectionHeaderAsync(inspectionHeaderRequestDTO).ConfigureAwait(true); ;
                    if (result.IsSuccess && result.Data.StatusCode == 0)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Inspection Header has been Saved!", "ok");
                        int InspectionID = _InspectionHeader != null ? _InspectionHeader.Id : 0;
                        var parameters = new NavigationParameters();
                        if (InspectionID > 0)
                        {
                            parameters.Add("HeaderID", result.Data.Id);
                            await _navigationService.NavigateAsync("InspectionDetailsListPage", parameters);
                        }
                        else
                        {
                            UserDialogs.Instance.ShowLoading();
                            parameters.Add("HeaderID", result.Data.Id);
                            parameters.Add("ScreenRight", "New Detail Inspection");
                            await _navigationService.NavigateAsync("AddNewDetailsInspectionPage", parameters);
                            UserDialogs.Instance.HideLoading();

                        }

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
        public async void Clear_Command()
        {
            try
            {
                SelectedCompany = null;
                Invoice = "";
                SelectedProduct = null;
                SelectedVariety = null;
                SelectedBrand = null;
                SelectedCountryofOrigin = null;
                TotalBoxQua = 0;
                TempOnCaja = 0;
                TempOnTermografo = 0;
                SelectedPalletizingCondition = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async void Edit_Command()
        {
            IsEditable = !IsEditable;
            IsView = !IsView;
            if (IsView)
            {
                ToolboxText = "Edit";
            }
            else
            {
                ToolboxText = "Cancel";
            }
        }
        public async void View_Command()
        {
            if (_InspectionHeader != null && _InspectionHeader.Id > 0)
            {
                var parameters = new NavigationParameters();
                parameters.Add("HeaderID", _InspectionHeader.Id);
                await _navigationService.NavigateAsync("InspectionDetailsListPage", parameters);
            }
        }
    }
}
