using System;
namespace Hmed.Main
{

    public class MenuItemModel
    {
        public MenuItemModel()
        {
            TargetType = typeof(MainPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public Type TargetType { get; set; }
        public object ViewModel { get; set; }
    }
}