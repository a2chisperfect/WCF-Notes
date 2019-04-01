using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.NotesServiceReference;
using Notes.Views;
using System.Windows.Input;
using Notes.Model;
using System.Windows;
using System.ServiceModel;

namespace Notes.ViewModel
{
    class EditPageViewModel : ViewModelBase
    {
        public bool isTextEnabled { get; set; }
        public string PageTitle { get; set; }

        private string _oldString; 
        
        private Note _selectedItem;
        public Note SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                _oldString = _selectedItem.Text;
                NotifyPropertyChanged();
            }
        }


        private Visibility _saveVisibility;

        public Visibility SaveVisibility
        {
            get { return _saveVisibility; }
            set
            {
                _saveVisibility = value;
                NotifyPropertyChanged();
            }
        }

        public INotesPage Page { get; set; }

        public EditPageViewModel(INotesPage page)
        {
            Page = page;
            SaveVisibility = Visibility.Visible;
            isTextEnabled = true;
            page.DataContext = this;
            PageTitle = "Edit note:";
            SaveCommand = new RelayCommand(SaveCommand_Execute, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelCommand_Execute);
        }

        public ICommand SaveCommand { get; set; }
        private void SaveCommand_Execute()
        {
            try
            {
                NotesClient.EditNote(SelectedItem);
                Navigation.NavigateToNotesPage();
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
        private bool SaveCommand_CanExecute()
        {
            if (SelectedItem !=null && String.IsNullOrEmpty(SelectedItem.Text))
                return false;
            else
                return true;
        }

        public ICommand CancelCommand { get; set; }
        private void CancelCommand_Execute()
        {
            SelectedItem.Text = _oldString;
            Navigation.NavigateToNotesPage();
        }
    }
}
