using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Hmed.Agendas;
using Hmed.ApplicationComponents;
using Hmed.Medicos;
using Hmed.Pacientes;
using Hmed.Notificacoes;
using Xamarin.Forms;
using Hmed.ComunicacaoCorpoClinico;
using Hmed.RepassesMedicos;
using Hmed.Parecer;

namespace Hmed.Main
{
    public class MainViewModel : ViewModelBase
    {
        #region Propriedades Publicas
        public ICommand NavegarParaOPerfil { get; set; }
        public ICommand NavegarParaAAgenda { get; set; }
        public ICommand NavegarParaOsPacientes { get; set; }
        public ICommand NavegarParaAsNotificacoes { get; set; }
        public ObservableCollection<MenuItemModel> MenuItems { get; set; }

        public string NomeHospital { get; set; }
        public MedicoMicro Medico { get; set; }


        private int _notificacoesCount;
        public bool MostraVerMais
        {
            get
            {
                return _notificacoesCount > 4;
            }
        }

        private IList<Notificacao> _notificacoes;
        public IList<Notificacao> Notificacoes { 
            get{
                var noti =  _notificacoes;
                if (noti == null) return null;
                if(noti.Count > NumeroDeItensLista + 1){
                    noti = noti.Take(NumeroDeItensLista).ToList();
                    noti.Add(Notificacao.VerMais);
                }
                return noti;
            } 
        }

        public int? NumeroPacientes
        {
            get;
            set;
        }

        public int? NumeroAgendas { get; set; }


        #endregion

        public override bool Carregado => (_carregouNotificacoes && _carregouPacientes && _carregouAgendas && _carregouLocal);
        public override bool Carregando => !Carregado;

        bool _carregouLocal;
        bool _carregouNotificacoes;
        bool _carregouAgendas;
        bool _carregouPacientes;

        #region Construtores            
        public MainViewModel()
        {
            var agendasService = NotificacoesService.Instancia;
            
            Task.Run(async () =>
            {
                var noti = await agendasService.GetNotificacoes();
                _notificacoesCount = noti.Count;
                _notificacoes = noti.OrderByDescending(p => p.Data).ToList(); //.Take(3).ToList();
                _carregouNotificacoes = true;
                Change(nameof(Carregando));
                Change(nameof(Carregado));
            });

            Task.Run(() =>
            {
                NomeHospital = Configuration.Instancia.Hospital;
                Medico = MedicosService.Instancia.MedicoLogado;
                Change(nameof(Medico));
                Change(nameof(NomeHospital));                
                _carregouLocal = true;
                Change(nameof(Carregando));
                Change(nameof(Carregado));
            });

            Task.Run(async () =>
            {
                NumeroAgendas = await AgendasService.Instancia.GetCountAgendasAsync();
                Change(nameof(NumeroAgendas));                
                _carregouAgendas = true;
                Change(nameof(Carregando));
                Change(nameof(Carregado));
            });

            Task.Run(async () =>
            {
                NumeroPacientes = await PacientesService.Instancia.GetCountPacienteAsync();
                Change(nameof(NumeroPacientes));
                _carregouPacientes = true;
                Change(nameof(Carregando));
                Change(nameof(Carregado));
            });

            MenuItems = new ObservableCollection<MenuItemModel>(new[]
            {
                    new MenuItemModel { Id = 2, Title = "Inicio", Image = "ic_home_white.png" , TargetType = typeof(MainPageDetail), ViewModel = this},
                    new MenuItemModel { Id = 0, Title = "Pacientes", Image = "ic_people_white.png", TargetType = typeof(PacientesPage)},
                    new MenuItemModel { Id = 0, Title = "Agenda", Image = "ic_date_range_white.png", TargetType = typeof(AgendaPage)},
                    //new MenuItemModel { Id = 0, Title = "Corpo Clínico", Image = "ic_message_white.png", TargetType = typeof(ComunicacaoCorpoClinicoPage)},
                    new MenuItemModel { Id = 0, Title = "Fale com o Hospital", Image = "ic_email_white.png", TargetType = typeof(FaleConoscoPage)},
                    new MenuItemModel { Id = 3, Title = "Repasse Médico", Image = "ic_credit_card_white.png", TargetType = typeof(RepasseMedicoPage)},
                    new MenuItemModel { Id = 4, Title = "Parecer", Image = "ic_parecer_white.png", TargetType = typeof(ListaParecerPage) },
                    new MenuItemModel { Id = 1, Title = "Sair", Image = "ic_close_white.png" }
                   
            });

                if (!Configuration.Instancia.TemRepasse)
                {
                    var repasse = MenuItems.SingleOrDefault(p => p.Id == 3);
                    MenuItems.Remove(repasse);
                }
                if (!Configuration.Instancia.TemParecer)
                {
                    var parecer = MenuItems.SingleOrDefault(p => p.Id == 4);
                    MenuItems.Remove(parecer);
                }

            NavegarParaOPerfil = new Command(ExecutarNavegarParaOPerfil);
            NavegarParaAAgenda = new Command(ExecutarNavegarParaAAgenda);
            NavegarParaOsPacientes = new Command(ExecutarNavegarParaOsPacientes);
            NavegarParaAsNotificacoes = new Command(ExecuteNavegarParaAsNotificacoes);
        }
        #endregion         

        #region Metodos Privados
        private void ExecutarNavegarParaOsPacientes()
        {
            //NavigationManager.MudarDetailPage(new MainPageDetail());
            NavigationManager.EmpilharNavegacao(new PacientesPage());
        }
        private void ExecutarNavegarParaAAgenda()
        {
            //NavigationManager.MudarDetailPage(new MainPageDetail());
            NavigationManager.EmpilharNavegacao(new AgendaPage());
        }

        private void ExecuteNavegarParaAsNotificacoes()
        {
            NavigationManager.EmpilharNavegacao(new NotificacoesPage());
        }

        private void ExecutarNavegarParaOPerfil()
        {
            //NavigationManager.MudarDetailPage(new MainPageDetail());
            NavigationManager.EmpilharNavegacao(new PerfilPage());
        }

        //ver mais 

        private double _tamanholistaview;
        public double TamanhoListView { get{
                return _tamanholistaview;
            } set{
                _tamanholistaview = value;
                Change(nameof(Notificacoes));
            } 
        }
        public double TamanhoCadaItem = 55;

        public int NumeroDeItensLista {
            get{
                return (int)(TamanhoListView / TamanhoCadaItem) - 1;
            }
        }

        #endregion
    }
}
