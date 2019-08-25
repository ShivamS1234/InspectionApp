using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Inspection.Resouces.DTO.Request;
using Inspection.Resouces.Entites;
using InspectionApp.Helpers;
using InspectionApp.WebServices;
using Prism.Navigation;
using Xamarin.Forms;

namespace InspectionApp.ViewModel
{
    public class AddNewInspectionViewModel : BaseViewModel
    {
        public Command _DetailsList;
        INavigationService _navigationService;
        WebServiceManager webServiceManager;
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
        private int _PalletizingCondition;
        public int PalletizingCondition
        {
            get { return _PalletizingCondition; }
            set { SetProperty(ref _PalletizingCondition, value); }
        }

        public AddNewInspectionViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "New Inspection";
            _navigationService = navigationService;
            webServiceManager = new WebServiceManager();
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
        }
        public Command DetailsList
        {
            get
            {
                return _DetailsList ?? (_DetailsList = new Command(async () => AddNew_Command()));
            }
        }
        public async void AddNew_Command()
        {
            //if (SelectedCompany == null)
            //{
            //    await App.Current.MainPage.DisplayAlert("Alert ", "Please Select Company Name!", "ok");
            //}
            //if (SelectedProduct == null)
            //{
            //    await App.Current.MainPage.DisplayAlert("Alert ", "Please Select Product Name!", "ok");
            //}
            //if (SelectedVariety == null)
            //{
            //    await App.Current.MainPage.DisplayAlert("Alert ", "Please Select Variety Name!", "ok");
            //}
            //else if (SelectedBrand == null)
            //{
            //    await App.Current.MainPage.DisplayAlert("Alert", "Please Select Brand Name!", "ok");
            //}
            //else if (SelectedCountryofOrigin == null)
            //{
            //    await App.Current.MainPage.DisplayAlert("Alert", "Please Select CountryofOrigin Name!", "ok");
            //}
            if (TotalBoxQua == 0)
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
            else if (PalletizingCondition == 0)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please Enter Palletizing Condition!", "ok");
            }
            else
            {
                try
                {
                    UserDialogs.Instance.ShowLoading("Loading...");
                    InspectionHeadersRequestDTO inspectionHeaderRequestDTO = new InspectionHeadersRequestDTO()
                    {
                        CompanyId = Convert.ToInt32(RememberMe.Get("CmpID")),
                        InspectionDate = DateTime.Now,
                        UserId = Convert.ToInt32(RememberMe.Get("userID")),
                        Invoice = Invoice,
                        ProducerId = 6,
                        //ProducerId = SelectedProduct.Id,
                        VarietyId = 7,
                        //VarietyId = SelectedVariety.Id,
                        BrandId = 8,
                        //BrandId =SelectedBrand.Id,
                        CountryofOriginId = 9,
                        //CountryofOriginId = SelectedCountryofOrigin.Id,
                        TotalBoxQuantities = TotalBoxQua,
                        TempOnCaja = TempOnCaja,
                        TempOnTermografo = TempOnTermografo,
                        PalletizingConditionId = PalletizingCondition
                    };
                    var result = await webServiceManager.RegistrationInspectionHeaderAsync(inspectionHeaderRequestDTO).ConfigureAwait(true); ;
                    if (result.IsSuccess && result.Data.StatusCode == 0)
                    {
                        await _navigationService.NavigateAsync("InspectionDetailsListPage");
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
    }
}
