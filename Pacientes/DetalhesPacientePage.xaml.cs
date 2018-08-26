using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hmed.Pacientes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalhesPacientePage : ContentPage
    {
        public DetalhesPacientePage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
        }

        private void SizeChanged1(object sender, System.EventArgs e)
        {
            DetalhesPacienteVIewModel vm = this.BindingContext as DetalhesPacienteVIewModel;
            var label = sender as Label;
            if (vm.TamanhoPrimeiraLinha < label.Height)
                vm.TamanhoPrimeiraLinha = label.Height;

            if (lblC2L1.Height > -1)
                if (lblC1L1.Height > lblC2L1.Height)
                    lblC2L1.HeightRequest = lblC1L1.Height;
                else
                    lblC1L1.HeightRequest = lblC2L1.Height;
        }

        private void SizeChanged2(object sender, System.EventArgs e)
        {
            DetalhesPacienteVIewModel vm = this.BindingContext as DetalhesPacienteVIewModel;
            var label = sender as Label;
            if (vm.TamanhoSegundaLinha < label.Height)
                vm.TamanhoSegundaLinha = label.Height;
            if (lblC2L2.Height > -1)
                if (lblC1L2.Height > lblC2L2.Height)
                    lblC2L2.HeightRequest = lblC1L2.Height;
                else
                    lblC1L2.HeightRequest = lblC2L2.Height;
        }

        private void SizeChanged3(object sender, System.EventArgs e)
        {
            DetalhesPacienteVIewModel vm = this.BindingContext as DetalhesPacienteVIewModel;
            var label = sender as Label;
            if (vm.TamanhoTerceiraLinha < label.Height)
                vm.TamanhoTerceiraLinha = label.Height;
            if (lblC2L3.Height > -1)
                if (lblC1L3.Height > lblC2L3.Height)
                    lblC2L3.HeightRequest = lblC1L3.Height;
                else
                    lblC1L3.HeightRequest = lblC2L3.Height;
        }

        private void SizeChanged4(object sender, System.EventArgs e)
        {
            DetalhesPacienteVIewModel vm = this.BindingContext as DetalhesPacienteVIewModel;
            var label = sender as Label;
            if (vm.TamanhoQuartaLinha < label.Height)
                vm.TamanhoQuartaLinha = label.Height;
            if (lblC2L4.Height > -1)
                if (lblC1L4.Height > lblC2L4.Height)
                    lblC2L4.HeightRequest = lblC1L4.Height;
                else
                    lblC1L4.HeightRequest = lblC2L4.Height;
        }
    }
}