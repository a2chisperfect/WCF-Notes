using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Notes.Views
{
    interface IMainView
    {
        NavigationService NavigationService { get; }
        object DataContext { get; set; }
        void Show();
        void Close();
    }
}
