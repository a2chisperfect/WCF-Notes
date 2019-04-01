using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.Views;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Notes.Model;
using Notes.Pages;

namespace Notes.ViewModel
{
    class MainWindowViewModel: ViewModelBase
    {
        IMainView _view;
        private Visibility _menuVisibility;

        public Visibility MenuVisibility
        {
            get { return _menuVisibility; }
            set
            {
                _menuVisibility = value;
                NotifyPropertyChanged();
            }
        }
        public MainWindowViewModel(IMainView view)
        {
            _view = view;
            _view.DataContext = this;
            NotesClient.LoginHandler += NotesClient_LoginHandler;
            Navigation.NavService = _view.NavigationService;
            MenuVisibility = Visibility.Visible;    
            GoToLoginCommand = new RelayCommand(GoToLoginCommandExecute);
            GoToNotesCommand = new RelayCommand(GoToNotesCommandExecute);
            GoToLoginCommand.Execute(null);
        }

        void NotesClient_LoginHandler(bool result)
        {
            MenuVisibility = result ? Visibility.Visible : Visibility.Collapsed;
        }
        public void Show()
        {
            _view.Show();
        }
        public void Close()
        {
            _view.Close();
        }

        public ICommand GoToLoginCommand { get; set; }

        private void GoToLoginCommandExecute()
        {
            NotesClient.NotesUser = null;
            Navigation.NavigateToLoginPage();
        }

        public ICommand GoToNotesCommand { get; set; }

        private void GoToNotesCommandExecute()
        {
            Navigation.NavigateToNotesPage();
        }

    }
}
