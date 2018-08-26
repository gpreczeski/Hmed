using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hmed.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDetail : ContentPage
    {
        public MainPageDetail()
        {
            InitializeComponent();

            Lista.SizeChanged += Lista_BindingContextChanged;
        }

        protected override void OnAppearing()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            base.OnAppearing();
        }

        void Lista_BindingContextChanged(object sender, System.EventArgs e)
        {
            var vm = (MainViewModel)this.BindingContext;
            vm.TamanhoListView = Lista.Height;
        }

    }
}
