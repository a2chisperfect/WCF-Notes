using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using LiteDB;

namespace Server.Model
{
    [DataContract]
    class User
    {
        [DataMember]
        public Guid  Id { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Email { get; set; }
        public List<Note> Notes { get; set; }
        public User()
        {
            Notes = new List<Note>();
            Id = Guid.NewGuid();
        }
    }
   
    

    
}
