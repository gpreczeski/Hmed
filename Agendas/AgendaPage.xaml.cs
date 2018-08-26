using System;
using Hmed.ApplicationComponents;
using Hmed.Pacientes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hmed.Agendas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendaPage : ContentPage
    {
        public AgendaPage()
        {
            
            this.BindingContext = _viewModel = new AgendaViewModel();            
            InitializeComponent();
        }

        private void OnAgendaSelecionada(object sender, SelectedItemChangedEventArgs e)
        {
            var agenda = (Agendas.Agenda)e.SelectedItem;
            if (e.SelectedItem != null)
            {
                if (agenda.Paciente != null)
                {
                    var viewModel = new DetalhesPacienteVIewModel(agenda);
                    var pagina = new DetalhesPacientePage() {BindingContext = viewModel};
                    NavigationManager.EmpilharNavegacao(pagina);
                }
                ListaDeAgendas.SelectedItem = null;
            }
        }

        AgendaViewModel _viewModel;
        DateTime _oldDate;

        private void DatePicker_OnClicked(object sender, EventArgs e)
        {
            _oldDate = DatePicker.Date;
            DatePicker.Focus();
        }

        void Handle_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if(_oldDate == DateTime.Today && DatePicker.Date == DateTime.Today ){
                _viewModel.DataSelecionada = DateTime.Today;
            }
        }
    }
}