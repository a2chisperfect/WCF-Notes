using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Server.Model
{
    class NotesService:ILoginService, INotesDataService
    {
        LiteDatabase db;
        BsonMapper mapper;
        public NotesService()
        {
            mapper = new BsonMapper();
            mapper.Entity<User>().DbRef(x => x.Notes, "Note");
            mapper.Entity<Note>().DbRef(x => x.User, "User");
        }
        public bool Register(string username, string password, string email)
        {
            using(db = new LiteDatabase("NotesDb",mapper))
            {
                var users = db.GetCollection<User>();
                if (users.Exists(x => x.Username == username))
                {
                    return false;
                }
                else
                {

                    users.Insert(new User() { Username = username, Password = password, Email = email });
                    return true;
                }
            }
        }

        public User Login(string username, string password)
        {
            using (db = new LiteDatabase("NotesDb",mapper))
            {
                var users = db.GetCollection<User>();
                var user = users.Find(u => u.Username == username && u.Password == password).FirstOrDefault();
                return user;
            }
        }

        public List<Note> GetLastNotes(int count, Guid userId)
        {
             using (db = new LiteDatabase("NotesDb",mapper))
             {
                 var user = db.GetCollection<User>().Include(x=>x.Notes).FindById(userId);

                 return user.Notes.OrderBy(x=>x.CreationDate).Take(count).ToList();
             }
            
        }
        public List<Note> GetLastNotes(DateTime from, DateTime to, Guid userId)
        {
            using (db = new LiteDatabase("NotesDb", mapper))
            {
                var user = db.GetCollection<User>().Include(x => x.Notes).FindById(userId);

                return user.Notes.OrderBy(x => x.CreationDate).Where(x => x.CreationDate.Date >= from.Date && x.CreationDate <= to.Date).ToList();
            }
        }

        public Guid AddNote(Note note, Guid userId)
        {
            using (db = new LiteDatabase("NotesDb", mapper))
            {
                var users = db.GetCollection<User>().Include(x => x.Notes);
                var user = users.FindById(userId);
                note.User = user;
                note.Id = Guid.NewGuid();
                db.GetCollection<Note>().Insert(note);
                user.Notes.Add(note);
                users.Update(user);
                return note.Id;
            }
        }

        public void DeleteNote(Guid idNote)
        {
            using (db = new LiteDatabase("NotesDb", mapper))
            {
                var note = db.GetCollection<Note>().Include(x=>x.User).FindById(idNote);
                db.GetCollection<Note>().Delete(idNote);
                note.User.Notes.Remove(note);
                db.GetCollection<User>().Update(note.User);
            }

        }
        public void EditNote(Note note)
        {
            using (db = new LiteDatabase("NotesDb", mapper))
            {
                var t = db.GetCollection<Note>().FindAll();
                var tmp = db.GetCollection<Note>().FindById(note.Id);
                tmp.Text = note.Text;
                db.GetCollection<Note>().Update(tmp);
            }

        }

    }

}
