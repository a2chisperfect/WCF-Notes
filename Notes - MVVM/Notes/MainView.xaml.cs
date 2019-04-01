using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Notes.Views;

namespace Notes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window,IMainView
    {
        public MainView()
        {
            InitializeComponent();
            //try
            //{
            //    NotesServiceReference.User user;
            //    using (NotesServiceReference.LoginServiceClient s = new NotesServiceReference.LoginServiceClient())
            //    {
            //        var res = s.Register("Alex", "123", "123");
            //        user = s.Login("Alex", "123");
            //    }


            //    using (NotesServiceReference.NotesDataServiceClient s2 = new NotesServiceReference.NotesDataServiceClient())
            //    {

            //        var note = new NotesServiceReference.Note() { Text = "abc", CreationDate = DateTime.Now };
            //        note.Id = s2.AddNote(note, user.Id);
            //        var notes = s2.GetLastNotes(5, user.Id);
            //        var day = s2.GetLastNotes(DateTime.Now, user.Id);
            //        //s2.DeleteNote(note.Id);
            //        note.Text = "edit";
            //        s2.EditNote(note);
            //        var notes2 = s2.GetLastNotes(5, user.Id);

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }
        public NavigationService NavigationService
        {
            get { return Frame.NavigationService; }
        }
    }
}
