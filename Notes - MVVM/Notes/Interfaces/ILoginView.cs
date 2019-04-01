using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Views
{
    interface ILoginView : IErrorShow
    {
        string GetPassword { get; }
        void ClearPassword();
        object DataContext { get; set; }
        //void ShowError(string message);
    }
}
