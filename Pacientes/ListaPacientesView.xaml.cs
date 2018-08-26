using Hmed.ApplicationComponents;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hmed.Pacientes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaPacientesView : ContentView
    {
        public ListaPacientesView()
        {
            InitializeComponent();
            ListaPacientes.ItemSelected += PacienteSelecionado;
        }

        private void PacienteSelecionado(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {

            var paciente = (Paciente)selectedItemChangedEventArgs.SelectedItem;
            if (paciente != null)
            {                
                var detalhesPacientePage = new DetalhesPacientePage();
                var viewModel = new DetalhesPacienteVIewModel(paciente);                
                detalhesPacientePage.BindingContext = viewModel;
                NavigationManager.EmpilharNavegacao(detalhesPacientePage);
                ListaPacientes.SelectedItem = null;
            }
        }
    }
}