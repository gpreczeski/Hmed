using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hmed.Pacientes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PacientesPage : ContentPage
	{
	    private PacientesViewModel _vm;
		public PacientesPage ()
		{
            this.BindingContext = _vm = new PacientesViewModel();
		    NavigationPage.SetBackButtonTitle(this, string.Empty);
            InitializeComponent ();
		}
    }
}