using Kaysho.NET.Mobile.Contracts.Services.Data;
using Kaysho.NET.Mobile.Contracts.Services.General;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XF.Material.Forms.UI.Dialogs;

namespace Kaysho.NET.Mobile.ViewModels.Login
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginPageViewModel : LoginViewModel
    {
        #region Fields

        private string password;
        private string email;


        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsService _settingsService;

        #endregion

        #region Constructor


        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public LoginPageViewModel(INavigationService navigationService,
            IAuthenticationService authenticationService,
            ISettingsService settingsService

            ) : base(navigationService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;
            this.LoginCommand = new DelegateCommand(ExecuteLogin, CanExecuteLogin);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);

        }



        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool CanExecuteLogin()
        {
            if (!string.IsNullOrWhiteSpace(Password) && IsValidEmail(Email))
            {
                return true;
            }
            return false;
        }

        private async void ExecuteLogin()
        {

            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                // Connection to internet is available
                var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Authenticating...");

                IsBusy = true;

                var login = new DamilolaShopeyin.Core.Models.Login
                {
                    Email = Email,
                    Password = Password
                };


                var authenticationResponse = await _authenticationService.Authenticate(login);

                if (authenticationResponse.IsAuthenticated)
                {
                    // we store the Id to know if the user is already logged in to the application
                    // _settingsService.UserIdSetting = authenticationResponse.User.Id;
                    //_settingsService.UserNameSetting = authenticationResponse.User.FirstName;
                    await loadingDialog.DismissAsync();

                    IsBusy = false;
                    Application.Current.MainPage = new AppShell();
                }
            }
            else
            {
                //No internet
                await MaterialDialog.Instance.SnackbarAsync(message: "No Intenet Connection",
                                            actionButtonText: "Got It",
                                            msDuration: 3000);

            }

        }

        #endregion

        #region property

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;
                this.OnPropertyChanged();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                if (this.email == value)
                {
                    return;
                }

                this.email = value;
                this.OnPropertyChanged();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public DelegateCommand LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the social media login button is clicked.
        /// </summary>
        public Command SocialMediaLoginCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        //private async void LoginClicked(object obj)
        //{
        //    // Do something


        //    IsBusy = true;

        //    var login = new DamilolaShopeyin.Core.Models.Login
        //    {
        //        Email = Email,
        //        Password = Password
        //    };


        //    var authenticationResponse = await _authenticationService.Authenticate(login);

        //    if (authenticationResponse.IsAuthenticated)
        //    {
        //        // we store the Id to know if the user is already logged in to the application
        //        // _settingsService.UserIdSetting = authenticationResponse.User.Id;
        //        //_settingsService.UserNameSetting = authenticationResponse.User.FirstName;

        //        IsBusy = false;
        //        Application.Current.MainPage = new AppShell();
        //    }

        //}

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SignUpClicked(object obj)
        {
            // Do something
            await _navigationService.NavigateAsync("SignUpPage");

        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void ForgotPasswordClicked(object obj)
        {
            var label = obj as Label;
            label.BackgroundColor = Color.FromHex("#70FFFFFF");
            await Task.Delay(100);
            label.BackgroundColor = Color.Transparent;
        }

        /// <summary>
        /// Invoked when social media login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SocialLoggedIn(object obj)
        {
            // Do something
        }

        #endregion
    }
}