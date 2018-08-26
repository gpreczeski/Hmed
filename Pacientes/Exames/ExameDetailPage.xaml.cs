using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hmed.Pacientes.Exames
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExameDetailPage : ContentPage
    {

        ExamesViewModel viewModel;

        public ExameDetailPage(Exame exame)
        {
            viewModel = new ExamesViewModel(exame);
            this.BindingContext = viewModel;
            InitializeComponent();
        }

        void Handle_Navigated(object sender, Xamarin.Forms.WebNavigatedEventArgs e)
        {
            if(Device.RuntimePlatform == Device.Android)
                viewModel.Carregando = false;
        }
    }
}