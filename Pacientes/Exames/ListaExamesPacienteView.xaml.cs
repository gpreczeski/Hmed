using Hmed.ApplicationComponents;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hmed.Pacientes.Exames
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaExamesPacienteView : ContentView
	{
		public ListaExamesPacienteView ()
		{
			InitializeComponent ();
		}

	    private void OnExameClicked(object sender, SelectedItemChangedEventArgs e)
	    {
	        if (e.SelectedItem != null)
	        {
	            var vm = (DetalhesPacienteVIewModel) this.BindingContext;
	            var documento = (Exame) e.SelectedItem;	                 
                NavigationManager.EmpilharNavegacao(new ExameDetailPage((Exame)e.SelectedItem));
	            ListaExames.SelectedItem = null;
	        }
	    }

    }
}