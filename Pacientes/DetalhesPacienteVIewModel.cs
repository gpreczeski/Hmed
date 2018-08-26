using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hmed.ApplicationComponents;
using Hmed.Pacientes.DocumentosEletronicos;
using Hmed.Pacientes.Exames;
using Hmed.Pacientes.Medicamentos;
using Hmed.Agendas;

namespace Hmed.Pacientes
{
    public class DetalhesPacienteVIewModel : ViewModelBase
    {
        public Paciente Paciente { get; set; }
        public Agenda Agenda
        {
            get;
            set;
        }
        public DetalhesPacienteVIewModel ViewModel => this;

        public string LabelFormatado
        {
            get{
                return Agenda == null ? Paciente.LeitoComLabel : Agenda.HorarioFormatado;
            }
        }

        public bool TemDadosMedicos
        {
            get
            {
                return !string.IsNullOrEmpty(Paciente.Atendimento) ||
                              (!string.IsNullOrEmpty(Paciente.Prontuario) && Paciente.Prontuario != "0" ) ||
                              !string.IsNullOrEmpty(Paciente.TipoSanguineo) ||
                              !string.IsNullOrEmpty(Paciente.UnidadeInternacao);

            }
        }

        public bool TemAtendimendo
        {
            get
            {
                return !string.IsNullOrEmpty(Paciente.Atendimento);
            }
        }

        public bool TemUnidade
        {
            get
            {
                return !string.IsNullOrEmpty(Paciente.UnidadeInternacao);
            }
        }

        public bool TemProntuario
        {
            get
            {
                return !string.IsNullOrEmpty(Paciente.Prontuario);
            }
        }

        public bool TemTipagem
        {
            get
            {
                return !string.IsNullOrEmpty(Paciente.TipoSanguineo);
            }
        }

        private double _tamanhoQuartaLinha;
        public double TamanhoQuartaLinha
        {
            get
            {
                return _tamanhoQuartaLinha;
            }

            set
            {
                _tamanhoQuartaLinha = value;
                Change(nameof(TamanhoQuartaLinha));
            }
        }

        private double _tamanhoTerceiraLinha;
        public double TamanhoTerceiraLinha
        {
            get
            {
                return _tamanhoTerceiraLinha;
            }

            set
            {
                _tamanhoTerceiraLinha = value;
                Change(nameof(TamanhoTerceiraLinha));
            }
        }

        private double _tamanhoSegundaLinha;
        public double TamanhoSegundaLinha
        {
            get
            {
                return _tamanhoSegundaLinha;
            }

            set
            {
                _tamanhoSegundaLinha = value;
                Change(nameof(TamanhoSegundaLinha));
            }
        }

        private double _tamanhoPrimeiraLinha;
        public double TamanhoPrimeiraLinha
        {
            get
            {
                return _tamanhoPrimeiraLinha;
            }
            set
            {
                _tamanhoPrimeiraLinha = value;
                Change(nameof(TamanhoPrimeiraLinha));
            }

        }



        public bool MostraDocumentos { get; set; }

        public ObservableCollection<DocumentoEletronico> DocumentoEletronicos
        {
            get;
            set;
        }

        public ObservableCollection<Medicamento> Medicamentos
        {
            get;
            set;
        }

        public ObservableCollection<Exame> Exames { get; set; }


        public int ListaMedicamentosHeight //Todo: Medicacmentos.Any
        {
            get
            {
                if (Medicamentos == null || !Medicamentos.Any()) return 0;
                return (Medicamentos.Count * 60) + 5;
            }
        }

        public int ListaDocumentosHeigth
        {
            get
            {
                if (DocumentoEletronicos == null) return 0;
                if (!DocumentoEletronicos.Any()) return 0; //Todo: Documents.Any
                return (DocumentoEletronicos.Count * 40) + 5;
            }
        }

        public int ListaExamesHeight
        {
            get
            {
                if (Exames == null) return 0;
                if (!Exames.Any()) return 0;
                return (Exames.Count * 40) + 5;
            }
        }

        public bool NaoTemMedicamentos
        {
            get
            {
                if (Medicamentos == null) return false;
                return !Medicamentos.Any();
            }
        }

        public bool NaoTemExames
        {
            get
            {
                if (Exames == null) return false;
                return !Exames.Any();
            }
        }

        public bool NaoTemDocumentos
        {
            get
            {
                if (DocumentoEletronicos == null) return false;
                return !DocumentoEletronicos.Any();
            }
        }


        public bool CarregandoDocumentos => !CarregadoDocumentos;
        public bool CarregadoDocumentos { get; set; }

        public bool CarregandoExames => !CarregadoExames;
        public bool CarregadoExames { get; set; }

        public bool CarregandoMedicamentos => !CarregadoMedicamentos;
        public bool CarregadoMedicamentos { get; set; }

        public DetalhesPacienteVIewModel(Agenda agenda){
            this.Agenda = agenda;
            this.Paciente = agenda.Paciente;
            MontaListas();
        }

        public DetalhesPacienteVIewModel(Paciente paciente)
        {
            Paciente = paciente;
            MontaListas();
        }


        private void MontaListas(){
            Task.Run(() => { MostraDocumentos = Configuration.Instancia.TemDocumento; });
            Task.Run(async () =>
            {
                var docs = await DocumentosEletronicosService.Instancia.GetDocuemntosEletronicos(Paciente.Id);
                DocumentoEletronicos = new ObservableCollection<DocumentoEletronico>(docs);
                CarregadoDocumentos = true;

                Change(nameof(MostraDocumentos));
                Change(nameof(DocumentoEletronicos));
                Change(nameof(ListaDocumentosHeigth));

                Change(nameof(CarregandoDocumentos));
                Change(nameof(CarregadoDocumentos));
                Change(nameof(NaoTemDocumentos));
            });

            Task.Run(async () =>
            {
                var exames = await ExamesService.Instancia.GetExames(Paciente.Id);
                Exames = new ObservableCollection<Exame>(exames);
                CarregadoExames = true;

                Change(nameof(Exames));
                Change(nameof(ListaExamesHeight));
                Change(nameof(NaoTemExames));

                Change(nameof(CarregadoExames));
                Change(nameof(CarregandoExames));
            });

            Task.Run(async () =>
            {
                var med = await MedicamentosService.Instancia.GetMedicaments(Paciente.Id);
                Medicamentos = new ObservableCollection<Medicamento>(med);
                CarregadoMedicamentos = true;

                Change(nameof(Medicamentos));
                Change(nameof(ListaMedicamentosHeight));
                Change(nameof(NaoTemMedicamentos));

                Change(nameof(CarregandoMedicamentos));
                Change(nameof(CarregadoMedicamentos));
            });
        }
    }
}