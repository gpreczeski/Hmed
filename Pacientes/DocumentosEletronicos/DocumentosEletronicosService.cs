using System.Collections.Generic;
using System.Threading.Tasks;
using Hmed.ApplicationComponents;

namespace Hmed.Pacientes.DocumentosEletronicos
{
    public class DocumentosEletronicosService
    {
        public static DocumentosEletronicosService Instancia = new DocumentosEletronicosService();
        private DocumentosEletronicosService()
        {
        }

        public async Task<IList<DocumentoEletronico>> GetDocuemntosEletronicos(int id)
        {
            var client = new HMedHttpClient();
            var documentos = await client.GetAync<IList<DocumentoEletronico>>(Apis.ApiDocumento.Replace("[idPaciente]", id.ToString()));
            return documentos;
        }
    }
}