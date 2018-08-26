using System;
using System.Threading.Tasks;
using Hmed.ApplicationComponents;
using Xamarin.Forms;

namespace Hmed.Pacientes.Exames
{
    public class ExamesViewModel : ViewModelBase
    {
        public Exame Exame
        {
            get;
            set;
        }

        public ExamesViewModel(Exame exame)
        {
            Carregando = true;
            Exame = exame;
            Task.Run(async () =>
            {
                var client = new HMedHttpClient();
                var url = Apis.ApiExameCaminhoArquivo.Replace("[idExame]", Exame.Id.ToString());
                var resposta = await client.GetAync<string>(url);

                PdfUrl = Device.RuntimePlatform != Device.Android ? 
                               resposta : 
                               "https://docs.google.com/viewer?url=" + resposta;
                                   
              //  if(Device.RuntimePlatform != Device.Android)
                    Carregando = false;
                Change(nameof(PdfUrl));
                Change(nameof(Carregando));
                Change(nameof(Carregado));
            });
        }
        public string PdfUrl { get; set; }
    }
}
