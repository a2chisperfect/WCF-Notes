using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.Pages;
using System.Windows.Input;
//using Microsoft.TeamFoundation.MVVM;
using Notes.Views;
using Notes.Model;
using Notes.NotesServiceReference;
using System.ServiceModel;

namespace Notes.ViewModel
{
    class LoginPageViewModel:ViewModelBase
    {
        public ILoginView Page { get; set; }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyPropertyChanged();
            }
        }
        
        public LoginPageViewModel(ILoginView page)
        {
            Page = page;
            Page.DataContext = this;
            LoginCommand = new RelayCommand(LoginCommand_Excute);
            RegisterCommand = new RelayCommand(RegisterCommand_Excute);
        }

        public ICommand LoginCommand { get; set; }

        private void LoginCommand_Excute()
        {
            try
            {


                if (!NotesClient.Login(UserName.Trim(), Page.GetPassword.Trim()))
                {
                    Page.ShowError("Wrong username or password");
                }
                else
                {
                    ResetData();
                    Navigation.NavigateToNotesPage();
                }
            }
            catch (CommunicationException)
            {
                Page.ShowError("Ooops no connection to server... Try again later.");
            }
            catch (Exception ex)
            {
                Page.ShowError(ex.Message);
            }

        }

        public ICommand RegisterCommand { get; set; }

        private void RegisterCommand_Excute()
        {
            ResetData();
            Navigation.NavigateToRegisterPage();
        }
        private void ResetData()
        {
            UserName = String.Empty;
            Page.ClearPassword();
        }
    }
}
