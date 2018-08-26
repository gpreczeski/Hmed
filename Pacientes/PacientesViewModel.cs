using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Hmed.Annotations;
using Xamarin.Forms;
using Hmed.ApplicationComponents;

namespace Hmed.Pacientes
{
    public class PacientesViewModel : ViewModelBase
    {
        public ICommand MostraPesquisaSwitcher { get; set; }
        public bool MostraPesquisa { get; set; }
        public string Text => "View Lista!";

        public bool NaoTemPaciente
        {
            get
            {
                if (Pacientes == null) return false;
                return !Pacientes.Any();
            }
        }

        private string _pesquisa;
        public string Pesquisa
        {
            get
            {                
                return _pesquisa;
            }
            set
            {
                _pesquisa = value;
                Pacientes = new ObservableCollection<Paciente>(listaPacientes.Where(p => p.Stringfyed.ToLower().Contains(value.ToLower())));
                //_pacientes.Where(p => p.Stringfyed.ToLower().Contains(value.ToLower()));
                Change(nameof(Pacientes));
                Change(nameof(NaoTemPaciente));
            }
        }

        private ObservableCollection<Paciente> _pacientes;

        public ObservableCollection<Paciente> Pacientes
        {
            get => _pacientes;
            set
            {
                _pacientes = value;
                Change(nameof(Pacientes));
            }
        }

        IList<Paciente> listaPacientes = null;

        public PacientesViewModel()
        {
            Carregando = true;
            Task.Run(async () =>
            {                
                listaPacientes = await PacientesService.Instancia.GetPacientes();
                Pacientes = new ObservableCollection<Paciente>(listaPacientes);                
                Carregando = false;
                Change(nameof(NaoTemPaciente));
            });
            MostraPesquisaSwitcher = new Command(ExecuteMostraPesquisa);
        }

        private void ExecuteMostraPesquisa()
        {
            MostraPesquisa = !MostraPesquisa;
            Change(nameof(MostraPesquisa));
        }        
    }
}