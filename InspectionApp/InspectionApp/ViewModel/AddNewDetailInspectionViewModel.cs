using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Inspection.Resouces.DTO.Request;
using Inspection.Resouces.Entites;
using InspectionApp.WebServices;
using Prism.Navigation;
using Xamarin.Forms;

namespace InspectionApp.ViewModel
{
    public class AddNewDetailInspectionViewModel : BaseViewModel
    {
        WebServiceManager webServiceManager;
        public Command _DetailsList;
        INavigationService _navigationService;
        private Company _SelectedCompany;
        public Company SelectedCompany
        {
            get { return _SelectedCompany; }
            set { SetProperty(ref _SelectedCompany, value); }
        }
        private IList<SizeTb> _SizeList;
        public IList<SizeTb> SizeList
        {
            get { return _SizeList; }
            set { SetProperty(ref _SizeList, value); }
        }
        private SizeTb _SelectedSize;
        public SizeTb SelectedSize
        {
            get { return _SelectedSize; }
            set { SetProperty(ref _SelectedSize, value); }
        }
        private IList<OpeningApperence> _OppeningList;
        public IList<OpeningApperence> OppeningList
        {
            get { return _OppeningList; }
            set { SetProperty(ref _OppeningList, value); }
        }
        private OpeningApperence _SelectedOppening;
        public OpeningApperence SelectedOppening
        {
            get { return _SelectedOppening; }
            set { SetProperty(ref _SelectedOppening, value); }
        }
        private int _SampleSize;
        public int SampleSize
        {
            get { return _SampleSize; }
            set { SetProperty(ref _SampleSize, value); }
        }
        private decimal _Weight;
        public decimal Weight
        {
            get { return _Weight; }
            set { SetProperty(ref _Weight, value); }
        }
        private int _PhysicalCount;
        public int PhysicalCount
        {
            get { return _PhysicalCount; }
            set { SetProperty(ref _PhysicalCount, value); }
        }
        private decimal _Temperature;
        public decimal Temperature
        {
            get { return _Temperature; }
            set { SetProperty(ref _Temperature, value); }
        }
        private decimal _Brix;
        public decimal Brix
        {
            get { return _Brix; }
            set { SetProperty(ref _Brix, value); }
        }
        private decimal _Firmness;
        public decimal Firmness
        {
            get { return _Firmness; }
            set { SetProperty(ref _Firmness, value); }
        }
        private decimal _SkinDamage;
        public decimal SkinDamage
        {
            get { return _SkinDamage; }
            set { SetProperty(ref _SkinDamage, value); }
        }
        private decimal _Color;
        public decimal Color
        {
            get { return _Color; }
            set { SetProperty(ref _Color, value); }
        }
        private IList<PackageCondition> _PackageConditionList;
        public IList<PackageCondition> PackageCondition
        {
            get { return _PackageConditionList; }
            set { SetProperty(ref _PackageConditionList, value); }
        }
        private PackageCondition _SelectedPackageCondition;
        public PackageCondition SelectedPackageCondition
        {
            get { return _SelectedPackageCondition; }
            set { SetProperty(ref _SelectedPackageCondition, value); }
        }
        private string _Comment;
        public string Comment
        {
            get { return _Comment; }
            set { SetProperty(ref _Comment, value); }
        }
        private decimal _QualityScore;
        public decimal QualityScore
        {
            get { return _QualityScore; }
            set { SetProperty(ref _QualityScore, value); }
        }


        public AddNewDetailInspectionViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "New Detail Inspection";
            webServiceManager = new WebServiceManager();
            _navigationService = navigationService;
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
        }
        public Command SubmitCommand
        {
            get
            {
                return _DetailsList ?? (_DetailsList = new Command(async () => AddNew_Command()));
            }
        }
        public async void AddNew_Command()
        {
            if (SampleSize == 0)
            {
                await App.Current.MainPage.DisplayAlert("Alert ", "Please Enter Sample Size!", "ok");
            }
            else if (Weight == 0)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please Enter Weight!", "ok");
            }
            else if (PhysicalCount == 0)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please Enter Physical Count!", "ok");
            }
            else if (Temperature == 0)
            {

                await App.Current.MainPage.DisplayAlert("Alert", "Please Enter Temperature!", "ok");
            }
            else if (Brix == 0)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please Enter Brix!", "ok");
            }
            else if (Firmness == 0)
            {

                await App.Current.MainPage.DisplayAlert("Alert", "Please Enter Firmness!", "ok");
            }
            else
            {
                try
                {
                    UserDialogs.Instance.ShowLoading("Loading...");
                    InspectionDetailsRequestDTO inspectionRequestDTO = new InspectionDetailsRequestDTO()
                    {
                        InspectionHeaderId = 2,
                        //SizeId = SelectedSize.ID,
                        SizeId = 3,
                        SampleSize = SampleSize,
                        Weight = Weight,
                        PhysicalCount = PhysicalCount,
                        //OpeningApperenceId = SelectedOppening.Id,
                        OpeningApperenceId = 2,
                        Temperature = Temperature,
                        Brix = Brix,
                        Firmness = Firmness,
                        SkinDamage = SkinDamage,
                        Color = Color,
                        //PackageConditionId = SelectedPackageCondition.Id,
                        PackageConditionId = 1,
                        Comment = Comment,
                        QualityScore = QualityScore
                    };
                    var result = await webServiceManager.RegistrationInspectionDetailsAsync(inspectionRequestDTO).ConfigureAwait(true); ;
                    if (result.IsSuccess && result.Data.StatusCode == 0)
                    {
                        //await navigationService.NavigateAsync("LoginPage");
                        await _navigationService.GoBackAsync();
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
