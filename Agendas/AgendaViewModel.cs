using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Hmed.ApplicationComponents;

namespace Hmed.Agendas
{
    public class AgendaViewModel : ViewModelBase
    {
        #region Propriedades Publicas       

        public Boolean NaoTemAgendas
        {
            get
            {
                if (Agendas == null || !_dataSelecionada.HasValue) return false;
                return !Agendas.Any();
            }
        }
        private DateTime? _dataSelecionada;

        bool primeiraVez;

        public DateTime? DataSelecionada
        {
            get
            {
                if (!_dataSelecionada.HasValue)
                    return DateTime.Now;
                return _dataSelecionada;
            }
            set
            {
                Agendas = null;
                _dataSelecionada = value;
                Change(nameof(NaoTemAgendas));
                Change(nameof(DataFormatada));
                Carregando = true;
                Task.Run(async () =>
                {
                    IList<Agenda> agendas = null;
                    if (primeiraVez)
                    {
                        agendas = await AgendasService.Instancia.GetAgendas();
                    }
                    else agendas = await AgendasService.Instancia.GetAgendas(value.Value);
                    Agendas = new ObservableCollection<Agenda>(agendas);
                    Change(nameof(NaoTemAgendas));
                    Change(nameof(Agendas));
                    Carregando = false;
                    primeiraVez = false;
                });

            }
        }

        public string DataFormatada => _dataSelecionada?.ToString("dd/MM/yyyy");
        public IList<Agenda> Agendas { get; set; }

        #endregion

        #region Construtores

        public AgendaViewModel()
        {
            _dataSelecionada = null;
            Carregando = true;
            primeiraVez = true;

            //Task.Run(async () =>
            //{
            //    var agendas = await AgendasService.Instancia.GetAgendas();
            //    Agendas = new ObservableCollection<Agenda>(agendas);
            //    Change(nameof(NaoTemAgendas));
            //    Change(nameof(Agendas));
            //    Carregando = false;
            //});
        }

        #endregion
    }
}