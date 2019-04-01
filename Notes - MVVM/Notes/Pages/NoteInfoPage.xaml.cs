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

namespace Notes.Pages
{
    /// <summary>
    /// Interaction logic for NoteInfoPage.xaml
    /// </summary>
    public partial class NoteInfoPage : Page, INotesPage
    {
        public NoteInfoPage()
        {
            InitializeComponent();
        }
        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
