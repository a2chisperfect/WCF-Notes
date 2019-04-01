using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Views
{
    interface INotesPage:IErrorShow
    {
        object DataContext { get; set; }
    }
}
