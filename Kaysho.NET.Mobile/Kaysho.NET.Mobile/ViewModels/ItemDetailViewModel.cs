using Kaysho.NET.Mobile.Models;
using Prism.Navigation;

namespace Kaysho.NET.Mobile.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }

        //public ItemDetailViewModel(Item item = null)
        //{
        //    Title = item?.Text;
        //    Item = item;
        //}

        public ItemDetailViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
    }
}