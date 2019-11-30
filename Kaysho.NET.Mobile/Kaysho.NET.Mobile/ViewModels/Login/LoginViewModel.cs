﻿using Prism.Navigation;
using Xamarin.Forms.Internals;

namespace Kaysho.NET.Mobile.ViewModels.Login
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginViewModel : BaseViewModel
    {
        #region Fields

        // private string email;

        private bool isInvalidEmail;

        #endregion

        public LoginViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        #region Event

        /// <summary>
        /// The declaration of property changed event.
        /// </summary>

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the email ID from user in the login page.
        /// </summary>
        //public string Email
        //{
        //    get
        //    {
        //        return this.email;
        //    }

        //    set
        //    {
        //        if (this.email == value)
        //        {
        //            return;
        //        }

        //        this.email = value;
        //        this.OnPropertyChanged();
        //    }
        //}

        /// <summary>
        /// Gets or sets a value indicating whether the entered email is valid or invalid.
        /// </summary>
        public bool IsInvalidEmail
        {
            get
            {
                return this.isInvalidEmail;
            }

            set
            {
                if (this.isInvalidEmail == value)
                {
                    return;
                }

                this.isInvalidEmail = value;
                this.OnPropertyChanged();
            }
        }

        #endregion

        #region Method

        #endregion
    }
}
