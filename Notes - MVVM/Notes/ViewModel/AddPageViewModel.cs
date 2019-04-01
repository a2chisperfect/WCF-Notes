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
using System.Collections.ObjectModel;
using System.ServiceModel;


namespace Notes.ViewModel
{
    class AddPageViewModel: ViewModelBase
    {
        public bool isTextEnabled { get; set; }
        public string PageTitle { get; set; }
        public ObservableCollection<Note> Notes { get; set; }

        private Note _selectedItem;

        public Note SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged();
            }
        }
        
        public INotesPage Page { get; set; }

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

        public AddPageViewModel(INotesPage page)
        {
            Page = page;
            isTextEnabled = true;
            SaveVisibility = Visibility.Visible;
            page.DataContext = this;
            PageTitle = "Add new note:";
            SaveCommand = new RelayCommand(SaveCommand_Execute, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelCommand_Execute);
        }

        public ICommand SaveCommand { get; set; }
        private void SaveCommand_Execute()
        {
            try
            {
                SelectedItem.CreationDate = DateTime.Now.Date;
                SelectedItem.Id = NotesClient.AddNote(SelectedItem);
                Notes.Add(SelectedItem);
                Navigation.NavigateToNotesPage();
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
            Navigation.NavigateToNotesPage();
        }
    }
}
