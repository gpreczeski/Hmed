using System.Collections.Generic;
using System.Threading.Tasks;
using Hmed.ApplicationComponents;

namespace Hmed.Pacientes.Medicamentos
{
    public class MedicamentosService
    {
        public static MedicamentosService Instancia = new MedicamentosService();

        private MedicamentosService()
        {            
        }

        public async Task<IList<Medicamento>> GetMedicaments(int idPaciente)
        {
            var client = new HMedHttpClient();
            var medicamentos = await client.GetAync<IList<Medicamento>>(Apis.ApiMedicamento.Replace("[idPaciente]", idPaciente.ToString()));
            return medicamentos;
        }
    }
}