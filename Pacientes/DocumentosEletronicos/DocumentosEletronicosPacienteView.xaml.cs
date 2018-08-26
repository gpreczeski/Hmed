using Hmed.ApplicationComponents;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hmed.Pacientes.DocumentosEletronicos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DocumentosEletronicosPacienteView : ContentView
	{
		public DocumentosEletronicosPacienteView()
		{
			InitializeComponent ();
		}

	    private void ListaDocumentos_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        if (e.SelectedItem != null)
	        {
	            var vm = (DetalhesPacienteVIewModel) this.BindingContext;
	            var documento = (DocumentoEletronico) e.SelectedItem;
                NavigationManager.EmpilharNavegacao(new DocumentoEletronicoDetailPage()
                {
                    BindingContext = new DocumentoEletronicoViewModel((DocumentoEletronico) e.SelectedItem)
                });
                ListaDocumentos.SelectedItem = null;
	        }
        }
	}
}