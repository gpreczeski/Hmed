using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hmed.Pacientes.Medicamentos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaMedicamentosPacienteView : ContentView
	{
		public ListaMedicamentosPacienteView ()
		{
			InitializeComponent ();
		}

	    private void OnMedicamentoClicked(object sender, SelectedItemChangedEventArgs e)
	    {
	        if (e.SelectedItem != null)
	        {
	            ListaMedicamentos.SelectedItem = null;
	        }
	    }
    }
}