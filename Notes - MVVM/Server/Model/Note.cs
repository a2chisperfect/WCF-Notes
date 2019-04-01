using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using LiteDB;

namespace Server.Model
{
    [DataContract]
    class Note
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public DateTime CreationDate { get; set; }

        [BsonRef("users")]
        public User User { get; set; }
    }
}
