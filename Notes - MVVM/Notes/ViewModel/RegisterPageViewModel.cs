using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.Views;
using Notes.Model;
using System.Windows.Input;
using System.ServiceModel;

namespace Notes.ViewModel
{
    class RegisterPageViewModel:ViewModelBase
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

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyPropertyChanged();
            }
        }
        
        public RegisterPageViewModel(ILoginView page)
        {
            Page = page;
            Page.DataContext = this;
            RegisterCommand = new RelayCommand(RegisterCommand_Excute, RegisterCommand_CanExcute);
            CancelCommand = new RelayCommand(CancelCommand_Excute);
        }

        public ICommand RegisterCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private  void RegisterCommand_Excute()
        {
            try
            {

                if (NotesClient.Register(UserName.Trim(), Page.GetPassword.Trim(), Email.Trim()))
                {
                    ResetData();
                    Navigation.NavigateToLoginPage();
                }
                else
                {
                    Page.ShowError("This username or email already used");
                }
            }
            catch (CommunicationException)
            {
                Page.ShowError("Ooops no connection to server... Try again later.");
            }
            catch(Exception ex)
            {
                Page.ShowError(ex.Message);
            }
        }
        public bool RegisterCommand_CanExcute()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Page.GetPassword))
                return false;
            else
                return true;
        }
        private void CancelCommand_Excute()
        {
            ResetData();
            Navigation.NavigateToLoginPage();
        }
        private void ResetData()
        {
            UserName = String.Empty;
            Email = String.Empty;
            Page.ClearPassword();
        }
    }
}
