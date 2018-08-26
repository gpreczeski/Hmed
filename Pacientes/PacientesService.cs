using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hmed.ApplicationComponents;

namespace Hmed.Pacientes
{
    public class PacientesService
    {
        public static readonly PacientesService Instancia = new PacientesService();
       
        
        public Paciente GetPaciente(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Paciente>> GetPacientes()
        {
            var client = new HMedHttpClient();
            var pacientes = await client.GetAync<IList<Paciente>>(Apis.ApiPaciente);
            return pacientes.OrderBy(p=>p.Nome).ToList();
        }

        public async Task<int?> GetCountPacienteAsync()
        {
            var client = new HMedHttpClient();
            return await client.GetAync<int>(Apis.ApiPacienteCount);
        }
    }
}