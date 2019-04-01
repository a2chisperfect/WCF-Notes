using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Notes.NotesServiceReference;
using System.Windows.Controls;
using Notes.Pages;
using Notes.ViewModel;

namespace Notes
{
    static class Navigation
    {
        public static NavigationService NavService { get; set; }
        private static LoginPageViewModel LoginViewModel { get; set; }
        private static RegisterPageViewModel RegisterViewModel { get; set; }
        private static NotesPageViewModel NotesViewModel { get; set; }
        private static AddPageViewModel AddViewModel { get; set; }
        public static EditPageViewModel EditViewModel { get; set; }
        public static NoteInfoViewModel InfoViewModel { get; set; }
        static Navigation()
        {
            LoginViewModel = new LoginPageViewModel(new LoginPage());
            RegisterViewModel = new RegisterPageViewModel(new RegisterPage());
            AddViewModel = new AddPageViewModel(new NoteInfoPage());
            EditViewModel = new EditPageViewModel(new NoteInfoPage());
            InfoViewModel = new NoteInfoViewModel(new NoteInfoPage());
        }
        public static void NavigateToLoginPage()
        {
            NavService.Navigate(LoginViewModel.Page);
        }
        public static void NavigateToRegisterPage()
        {
            NavService.Navigate(RegisterViewModel.Page);
        }
        public static void NavigateToNotesPage()
        {
            if (NotesViewModel == null)
            {
                NotesViewModel = new NotesPageViewModel(new NotesPage());
            }
            NotesViewModel.UpdateNotes();
            NavService.Navigate(NotesViewModel.Page);
        }
        public static void NavigateToAddPage()
        {
            AddViewModel.SelectedItem = new Note();
            AddViewModel.Notes = NotesViewModel.Notes;
            NavService.Navigate(AddViewModel.Page);  
        }
        public static void NavigateToEditPage()
        {
            EditViewModel.SelectedItem = NotesViewModel.SelectedItem;
            NavService.Navigate(EditViewModel.Page);
        }
        public static void NavigateToInfoPage()
        {
            InfoViewModel.SelectedItem = NotesViewModel.SelectedItem;
            NavService.Navigate(InfoViewModel.Page);
        }
    }
}
