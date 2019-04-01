using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Notes.NotesServiceReference;
using Notes.Views;
using Notes.Model;
using System.Windows.Input;
using System.ServiceModel;

namespace Notes.ViewModel
{
    class NotesPageViewModel:ViewModelBase
    {
        private ObservableCollection<Note> _notes;

        public ObservableCollection<Note> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                NotifyPropertyChanged();
            }
        }
        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                NotifyPropertyChanged();
            }
        }
        
        private bool _selection;

        public bool Selection
        {
            get { return _selection; }
            set
            {
                _selection = value;
                NotifyPropertyChanged();
            }
        }
        
        private bool _filter;
        public bool Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                Selection = !Filter;
                UpdateNotes();
            }
        }
        
        public async void UpdateNotes()
        {
            try
            {
                if (_filter)
                {
                    Notes = await NotesClient.GetLastNotes(StartDate, EndDate);
                }
                else
                {
                    Notes = await NotesClient.GetLastNotes(20);
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
        public Note SelectedItem { get; set; }

        public INotesPage Page { get; set; }
        public NotesPageViewModel(INotesPage page)
        {
            Page = page;
            _filter = false;
            _selection = true;
            Page.DataContext = this;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            AddCommand = new RelayCommand(AddCommand_Execute);
            DeleteCommand = new RelayCommand(DeleteCommand_Execute, DeleteCommand_CanExecute);
            EditCommand = new RelayCommand(EditCommand_Execute, EditCommand_CanExecute);
            ReadCommand = new RelayCommand(ReadCommand_Execute);
        }

        public ICommand ReadCommand { get; set; }
        private void ReadCommand_Execute()
        {
            Navigation.NavigateToInfoPage();
        }
        public ICommand AddCommand { get; set; }
        private void AddCommand_Execute()
        {
            Navigation.NavigateToAddPage();
        }
        public ICommand DeleteCommand { get; set; }
        private void DeleteCommand_Execute()
        {
            try
            {
                NotesClient.DeleteNote(SelectedItem.Id);
                Notes.Remove(SelectedItem);
                SelectedItem = null;
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
        private bool DeleteCommand_CanExecute()
        {
            if(SelectedItem !=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ICommand EditCommand { get; set; }
        private void EditCommand_Execute()
        {
            Navigation.NavigateToEditPage();
        }
        private bool EditCommand_CanExecute()
        {
            if (SelectedItem != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
