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

namespace Notes.ViewModel
{
    class NoteInfoViewModel : ViewModelBase
    {
        public string PageTitle { get; set; }
        public bool isTextEnabled { get; set; }

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

        public NoteInfoViewModel(INotesPage page)
        {
            Page = page;
            _saveVisibility = Visibility.Collapsed;

            isTextEnabled = false;
            page.DataContext = this;
            PageTitle = "Note:";
            CancelCommand = new RelayCommand(CancelCommand_Execute);
        }


        public ICommand CancelCommand { get; set; }
        private void CancelCommand_Execute()
        {
            Navigation.NavigateToNotesPage();
        }
    }

}
