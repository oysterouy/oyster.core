using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class ExUsers : IModel
    {
        public System.Int64 Id { get; set; }
        public System.String Name { get; set; }
        public System.Int32 Age { get; set; }
        public System.String Email { get; set; }

        public const string iD = "Id";
        public const string nAme = "Name";
        public const string aGe = "Age";
        public const string eMail = "Email";

        public Type zModelType
        {
            get { return typeof(ExUsers); }
        }

        public string zTableName
        {
            get { return "ex_users"; }
        }
    }
}
