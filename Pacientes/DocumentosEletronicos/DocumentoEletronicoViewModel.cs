using System.Threading.Tasks;
using Hmed.ApplicationComponents;
using Xamarin.Forms;

namespace Hmed.Pacientes.DocumentosEletronicos
{
    public class DocumentoEletronicoViewModel : ViewModelBase
    {
        public DocumentoEletronico Documento
        {
            get;
            set;
        }

        public DocumentoEletronicoViewModel(DocumentoEletronico documento)
        {
            Carregando = true;
            Documento = documento;
            Task.Run(async () =>
            {
                var client = new HMedHttpClient();
                var url = Apis.ApiDocumentoCaminhoArquivo.Replace("[idDocumento]", Documento.Id.ToString());
                var resposta = await client.GetAync<string>(url);

                PdfUrl = Device.RuntimePlatform != Device.Android ?
                               resposta :
                               "https://docs.google.com/viewer?url=" + resposta;

            //    if (Device.RuntimePlatform != Device.Android)
                    Carregando = false;
                Change(nameof(PdfUrl));
                Change(nameof(Carregando));
                Change(nameof(Carregado)); 
            });
        }
        public string PdfUrl { get; set; }
    }
}
