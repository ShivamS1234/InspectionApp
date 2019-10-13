using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs;
using Inspection.Resouces.DTO.Request;
using Inspection.Resouces.Entites;
using InspectionApp.Helpers;
using InspectionApp.Model;
using InspectionApp.WebServices;
using Prism.Navigation;
using Xamarin.Forms;

namespace InspectionApp.ViewModel
{
    public class AddNewDetailInspectionViewModel : BaseViewModel
    {
        WebServiceManager webServiceManager;
        public Command _DetailsList, _Clear;
        INavigationService _navigationService;
        private Model.Company _SelectedCompany;
        int InspectionHeaderID = 0, InspectionDetailsID = 0;
        InspectionDetailModel _inspectionDetails;
        public Model.Company SelectedCompany
        {
            get { return _SelectedCompany; }
            set { SetProperty(ref _SelectedCompany, value); }
        }
        private IList<Model.SizeTb> _SizeList;
        public IList<Model.SizeTb> SizeList
        {
            get { return _SizeList; }
            set { SetProperty(ref _SizeList, value); }
        }
        private Model.SizeTb _SelectedSize;
        public Model.SizeTb SelectedSize
        {
            get { return _SelectedSize; }
            set { SetProperty(ref _SelectedSize, value); }
        }
        private IList<Model.OpeningApperence> _OppeningList;
        public IList<Model.OpeningApperence> OppeningList
        {
            get { return _OppeningList; }
            set { SetProperty(ref _OppeningList, value); }
        }
        private Model.OpeningApperence _SelectedOppening;
        public Model.OpeningApperence SelectedOppening
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
        private IList<Model.PackageCondition> _PackageConditionList;
        public IList<Model.PackageCondition> PackageCondition
        {
            get { return _PackageConditionList; }
            set { SetProperty(ref _PackageConditionList, value); }
        }
        private Model.PackageCondition _SelectedPackageCondition;
        public Model.PackageCondition SelectedPackageCondition
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
            try
            {
                UserDialogs.Instance.ShowLoading("Loading...");
                if (parameters != null)
                {
                    if (parameters.ContainsKey("HeaderID"))
                    {
                        InspectionHeaderID = parameters.GetValue<int>("HeaderID");
                    }
                    if (parameters.ContainsKey("ScreenRight"))
                    {
                        Title = parameters.GetValue<string>("ScreenRight");
                    }
                    if (parameters.ContainsKey("InspectionDetail"))
                    {
                        _inspectionDetails = parameters["InspectionDetail"] as InspectionDetailModel;
                        if (_inspectionDetails != null)
                        {
                            InspectionDetailsID = _inspectionDetails.Id;
                            SelectedSize = InitData.SizeTbList.Where(x => x.Id == _inspectionDetails.SizeId).FirstOrDefault();
                            SampleSize = _inspectionDetails.SampleSize;
                            Weight = _inspectionDetails.Weight;
                            PhysicalCount = _inspectionDetails.PhysicalCount;
                            SelectedOppening = InitData.OpeningApperenceList.Where(x => x.Id == _inspectionDetails.OpeningApperenceId).FirstOrDefault();
                            Temperature = _inspectionDetails.Temperature;
                            Brix = _inspectionDetails.Brix;
                            Firmness = _inspectionDetails.Firmness;
                            SkinDamage = _inspectionDetails.SkinDamage;
                            Color = _inspectionDetails.Color;
                            SelectedPackageCondition = InitData.PackageConditionList.Where(x => x.Id == _inspectionDetails.PackageConditionId).FirstOrDefault();
                            Comment = _inspectionDetails.Comment;
                            QualityScore = _inspectionDetails.QualityScore;
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
        public Command SubmitCommand
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
                return _Clear ?? (_Clear = new Command(async () => Clear_Command()));
            }
        }
        public async void AddNew_Command()
        {
            if (SampleSize == 0)
            {
                await App.Current.MainPage.DisplayAlert("Alert ", "Please Enter Sample Size!", "ok");
            }
            else if (SelectedSize == null)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please select Size Description!", "ok");
            }
            else if (SelectedOppening == null)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please select Oppening!", "ok");
            }
            else if (SelectedPackageCondition == null)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please select Package Condition!", "ok");
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
                    RememberMe.Set("isSyncDatabse", true);
                    await Xamarin.Forms.Application.Current.SavePropertiesAsync();
                    InspectionDetailsRequestDTO inspectionRequestDTO = new InspectionDetailsRequestDTO()
                    {
                        Id = InspectionDetailsID,
                        InspectionHeaderId = InspectionHeaderID,
                        SizeId = (int)SelectedSize.Id,
                        //SizeId = 3,
                        SampleSize = SampleSize,
                        Weight = Weight,
                        PhysicalCount = PhysicalCount,
                        OpeningApperenceId = (int)SelectedOppening.Id,
                        //OpeningApperenceId = 2,
                        Temperature = Temperature,
                        Brix = Brix,
                        Firmness = Firmness,
                        SkinDamage = SkinDamage,
                        Color = Color,
                        PackageConditionId = (int)SelectedPackageCondition.Id,
                        //PackageConditionId = 1,
                        Comment = Comment,
                        QualityScore = QualityScore
                    };
                    Model.InspectionDetailTable inspectionDetailTable = new Model.InspectionDetailTable
                    {
                        Id = 0,
                        InspectionHeaderId = inspectionRequestDTO.InspectionHeaderId,
                        SizeId = inspectionRequestDTO.SizeId,
                        SampleSize = inspectionRequestDTO.SampleSize,
                        Weight = inspectionRequestDTO.Weight,
                        PhysicalCount = inspectionRequestDTO.PhysicalCount,
                        OpeningApperenceId = inspectionRequestDTO.OpeningApperenceId,
                        Temperature = inspectionRequestDTO.Temperature,
                        Brix = inspectionRequestDTO.Brix,
                        Firmness = inspectionRequestDTO.Firmness,
                        SkinDamage = inspectionRequestDTO.SkinDamage,
                        Color = inspectionRequestDTO.Color,
                        PackageConditionId = inspectionRequestDTO.PackageConditionId,
                        Comment = inspectionRequestDTO.Comment,
                        QualityScore = inspectionRequestDTO.QualityScore,
                    };

                    if (ConfigurationCommon.App_Online)
                    {
                        var result = await webServiceManager.RegistrationInspectionDetailsAsync(inspectionRequestDTO).ConfigureAwait(true); ;
                        if (result.IsSuccess && result.Data.StatusCode == 0)
                        {
                            if (InspectionDetailsID > 0)
                            {
                                HeaderAsync(inspectionDetailTable, result.Data.Id, false);
                            }
                            else
                            {
                                HeaderAsync(inspectionDetailTable, result.Data.Id, true);
                            }
                            // save data
                            await App.Current.MainPage.DisplayAlert("Alert", "Inspection Details has been Saved!", "ok");

                            if (DashBoardViewModel.CheckNewInspection)
                            {
                                await _navigationService.GoBackToRootAsync();
                            }
                            else
                            {
                                await _navigationService.GoBackAsync();
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", result.Data.Status, "ok");
                        }
                    }
                    else
                    {
                        int MaxHeaderCode = 0;
                        if (InspectionDetailsID == 0)
                        {
                            var data = await InitData.HeaderRepo.GetMaxCode(x => x.Id, false);
                            MaxHeaderCode = (int)data.Id + 1;
                        }
                        else
                        {
                            MaxHeaderCode = InspectionDetailsID;
                        }
                        if (InspectionDetailsID > 0)
                        {
                            inspectionDetailTable.ModifiedAt = DateTime.Now;
                            inspectionDetailTable.Updated = true;
                            HeaderAsync(inspectionDetailTable, MaxHeaderCode, false);

                            await App.Current.MainPage.DisplayAlert("Alert", "Inspection Details has been Saved!", "ok");

                            if (DashBoardViewModel.CheckNewInspection)
                            {
                                await _navigationService.GoBackToRootAsync();
                            }
                            else
                            {
                                await _navigationService.GoBackAsync();
                            }
                        }
                        else
                        {
                            inspectionDetailTable.CreatedAt = DateTime.Now;
                            inspectionDetailTable.NewEntry = true;
                            HeaderAsync(inspectionDetailTable, MaxHeaderCode, true);
                            await App.Current.MainPage.DisplayAlert("Alert", "Inspection Details has been Saved!", "ok");

                            if (DashBoardViewModel.CheckNewInspection)
                            {
                                await _navigationService.GoBackToRootAsync();
                            }
                            else
                            {
                                await _navigationService.GoBackAsync();
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
        }
        public async void Clear_Command()
        {
            try
            {
                SelectedSize = null;
                SampleSize = 0;
                Weight = 0;
                PhysicalCount = 0;
                SelectedOppening = null;
                Temperature = 0;
                Brix = 0;
                Firmness = 0;
                SkinDamage = 0;
                Color = 0;
                SelectedPackageCondition = null;
                Comment = "";
                QualityScore = 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void HeaderAsync(Model.InspectionDetailTable inspectionRequestDTO, int MaxHeaderCode, Boolean New)
        {
            inspectionRequestDTO.Id = MaxHeaderCode;
            if (New)
            {
                inspectionRequestDTO.CreatedAt = DateTime.Now;
                inspectionRequestDTO.NewEntry = true;
                await InitData.DetailsRepo.InsertItemAsync(inspectionRequestDTO);
            }
            else
            {
                inspectionRequestDTO.ModifiedAt = DateTime.Now;
                inspectionRequestDTO.Updated = true;
                await InitData.DetailsRepo.UpdateItemAsync(inspectionRequestDTO);
            }
        }

    }
}
