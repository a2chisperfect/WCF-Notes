using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.ViewModel;
using Notes.NotesServiceReference;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Notes.Model
{
    static class NotesClient
    {
        private static User _user;
        //public static ObservableCollection<Note> Notes { get; set; }

        public static User NotesUser
        {
            get { return _user; }
            set
            {
                _user = value;
                if (LoginHandler != null)
                {
                    LoginHandler(_user == null ? false : true);
                }
            }
        }

        public static event Action<bool> LoginHandler;
        public static bool Login(string username , string password)
        {
            using (LoginServiceClient s = new LoginServiceClient())
            {
                NotesUser =  s.Login(username, password);
                if (NotesUser == null)
                    return false;
                else
                    return true;
            }
        }
        public static bool Register(string username, string password, string email)
        {
            using (LoginServiceClient s = new LoginServiceClient())
            {
                return  s.Register(username, password,email);
            }
        }
        public static async Task<ObservableCollection<Note>> GetLastNotes(int count)
        {
            using (NotesDataServiceClient s = new NotesDataServiceClient())
            {
                return await s.GetLastNotesAsync(30, NotesUser.Id);
            }
        }
        public static async Task<ObservableCollection<Note>> GetLastNotes(DateTime from, DateTime to)
        {
            using (NotesDataServiceClient s = new NotesDataServiceClient())
            {
                
                return await s.GetLastNotesAsync(from, to, NotesUser.Id);
            }
        }
        public static Guid AddNote(Note note)
        {
            using (NotesDataServiceClient s = new NotesDataServiceClient())
            {
                return s.AddNote(note,NotesUser.Id);
            }
        }
        public  static void DeleteNote(Guid Id)
        {
            using (NotesDataServiceClient s = new NotesDataServiceClient())
            {

                s.DeleteNote(Id);

            }
        }
        public static void EditNote(Note note)
        {
            using (NotesDataServiceClient s = new NotesDataServiceClient())
            {
                s.EditNote(note);
            }
        }
      

    }
}
