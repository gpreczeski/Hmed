using System.Collections.Generic;
using System.Threading.Tasks;
using Hmed.ApplicationComponents;

namespace Hmed.Pacientes.Exames
{
    public class ExamesService
    {
        public static readonly ExamesService Instancia = new ExamesService();

        public async Task<IList<Exame>> GetExames(int idPaciente)
        {
            var client = new HMedHttpClient();
            var exames = await client.GetAync<IList<Exame>>(Apis.ApiExame.Replace("[idPaciente]",idPaciente.ToString()));
            return exames;
        }
    }
}