using System;
using Hmed.ApplicationComponents;
using Hmed.Login;
using Hmed.Medicos;
using Xamarin.Forms;

namespace Hmed.Main
{
    public class MainPage : MasterDetailPage
    {
        public MainPageMaster MasterPage { get; set; }
        public MainPage()
        {
            MainViewModel vm;
            this.BindingContext = vm = new MainViewModel();
            this.Master = MasterPage = new MainPageMaster();

            var detailpage = new MainPageDetail()
            {
                BindingContext = vm
            };

            NavigationPage.SetBackButtonTitle(detailpage, "");

            var navi = new NavigationPage(detailpage);

            var primaria = Application.Current.Resources["Primaria"] as Color?;
            if (!primaria.HasValue)
                throw new Exception("Aplicativo sem cor primaria definida, erro ao instanciar NavigationPage");


            navi.BarBackgroundColor = primaria.Value;
            navi.BarTextColor = Color.White;


            this.Detail = navi;

            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuItemModel;
            if (item == null)
                return;

            if (item.Id == 1) // Sair
            {
                var medicoService = MedicosService.Instancia;
                medicoService.UpdateMedico();
                Configuration.LimparConfiguracoes();                
                
                var primaria = Application.Current.Resources["Primaria"] as Color?;
                if (!primaria.HasValue)
                    throw new Exception("Aplicativo sem cor primaria definida, erro ao instanciar NavigationPage");
                
                NavigationManager.MudarMainPage(new NavigationPage(new PaginaInicial()
                {
                    BindingContext = new LoginViewModel()
                })
                {
                    BackgroundColor = primaria.Value,
                    BarBackgroundColor = primaria.Value,
                    BarTextColor = Color.White
                });
                return;
            }

            if (item.Id == 2)
            {
                NavigationManager.MudarDetailPage(new MainPageDetail());
                MasterPage.ListView.SelectedItem = null;
                return;
            }

            var page = (Page)Activator.CreateInstance(item.TargetType);
            if (item.ViewModel != null)
                page.BindingContext = item.ViewModel;


            NavigationManager.MudarDetailPage(new MainPageDetail());
            NavigationManager.EmpilharNavegacao(page);
            // NavigationManager.MudarDetailPage(page);
            //Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}