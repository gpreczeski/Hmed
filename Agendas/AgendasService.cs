using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hmed.ApplicationComponents;

namespace Hmed.Agendas
{
    public class AgendasService
        {
        #region Singleton

        public static readonly AgendasService Instancia = new AgendasService();

        #endregion

        #region Construtores

        public AgendasService()
        {
            
        }
        
        #endregion

        #region Propriedades Privadas
        

        #endregion

        #region Metodos Públicos

        public async Task<IList<Agenda>> GetAgendas()
        {
            var client = new HMedHttpClient();
            var agendas = await client.GetAync<IList<Agenda>>(Apis.ApiAgenda);
            return agendas.OrderBy(p => p.Horario).ToList();
        }

        public async Task<IList<Agenda>> GetAgendas(DateTime dia)
        {
            var dataFormatada = dia.ToString("dd-MM-yyyy");            
            var client = new HMedHttpClient();
            var agendas = await client.GetAync<IList<Agenda>>(Apis.ApiAgendaFiltro.Replace("[data]", dataFormatada));            
            return agendas.OrderBy(p => p.Horario).ToList();
        }

        #endregion

        public async Task<int?> GetCountAgendasAsync()
        {
            var client = new HMedHttpClient();
            return await client.GetAync<int>(Apis.ApiAgendaCount);
        }
    }
}