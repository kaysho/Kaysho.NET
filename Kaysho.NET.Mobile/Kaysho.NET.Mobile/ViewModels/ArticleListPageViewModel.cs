using Kaysho.NET.Mobile.Models.Navigation;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Kaysho.NET.Mobile.ViewModels
{
    public class ArticleListPageViewModel : BaseViewModel
    {
        private DelegateCommand _delegateCommand;

        public DelegateCommand NavigateCommand => _delegateCommand ?? (_delegateCommand = new DelegateCommand(ExecuteNavigateCommand));
        public ArticleListPageViewModel(INavigationService navigationService) : base(navigationService)
        {

            NavigationList = new ObservableCollection<NavigationModel>
            {
                new NavigationModel{
                    ItemDescription = "Classic hamburger with Angus beef grilled to perfection",
                    ItemRating = 4.5,
                    ItemImage =  "Recipe19.png",
                    ItemName = "Hamburger"

                },

                new NavigationModel{
                    ItemDescription = "Classic hamburger with Angus beef grilled to perfection",
                    ItemRating = 4.5,
                    ItemImage =  "Recipe19.png",
                    ItemName = "Hamburger"

                }
            };
        }

        async void ExecuteNavigateCommand()
        {
            await _navigationService.NavigateAsync("ArticleWithCommentsPage");

        }

        [DataMember(Name = "navigationList")]
        public ObservableCollection<NavigationModel> NavigationList { get; set; }

        private static T PopulateData<T>(string fileName)
        {
            var file = "Kaysho.NET.Mobile.Data." + fileName;

            var assembly = typeof(App).GetTypeInfo().Assembly;

            T obj;

            using (var stream = assembly.GetManifestResourceStream(file))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                obj = (T)serializer.ReadObject(stream);
            }

            return obj;
        }
    }
}
