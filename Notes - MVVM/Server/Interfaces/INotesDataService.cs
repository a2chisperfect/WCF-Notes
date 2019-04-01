using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using System.ServiceModel;

namespace Server.Model
{
    [ServiceContract]
    interface INotesDataService
    {
        [OperationContract(Name = "GetLastNotesByCount")]
        List<Note> GetLastNotes(int count, Guid userId);

        [OperationContract(Name = "GetLastNotesByDate")]
        List<Note> GetLastNotes(DateTime from, DateTime to , Guid userId);
        [OperationContract]
        Guid AddNote(Note note, Guid userId);
        [OperationContract]
        void DeleteNote(Guid idNote);
        [OperationContract]
        void EditNote(Note note);

    }
}
