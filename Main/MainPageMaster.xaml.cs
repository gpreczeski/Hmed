using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hmed.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public ListView ListView;

        public MainPageMaster()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                Icon = new FileImageSource() { File = "ic_menu_white_2.png" };
            ListView = MenuItemsListView;
        }
    }
}