using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Kaysho.NET.Mobile.Views.Profile
{
    /// <summary>
    /// Page to show chat profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatProfilePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatProfilePage" /> class.
        /// </summary>
        public ChatProfilePage()
        {
            InitializeComponent();
            this.ProfileImage.Source = App.BaseImageUrl + "ProfileImage11.png";
        }
    }
}