using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class Person
    {
        private long _jmbg;
        private string _name;
        private string _lastName;
        private DateTime _timeOfEntry;

        [DataMember]
        public long Jmbg
        {
            get
            {
                return _jmbg;
            }

            set
            {
                _jmbg = value;
            }
        }

        [DataMember]
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        [DataMember]
        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                _lastName = value;
            }
        }

        [DataMember]
        public DateTime TimeOfEntry
        {
            get
            {
                return _timeOfEntry;
            }

            set
            {
                _timeOfEntry = value;
            }
        }

        public Person(long jmbg, string ime, string prezime)
        {
            Jmbg = jmbg;
            Name = ime;
            LastName = prezime;
            TimeOfEntry = new DateTime();
        }

        public override string ToString()
        {
            return Jmbg + " : " + Name + " " + LastName;
        }
    }
}
