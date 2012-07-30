using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public class OyOrderBy
    {
        public string Name;
        public bool Desc = true;
        public OyOrderBy Right;
        public OyOrderBy(string name, bool isdesc = true, OyOrderBy right = null)
        {
            Name = name;
            Desc = isdesc;
            Right = right;
        }
        public static OyOrderBy operator &(OyOrderBy left, OyOrderBy right)
        {
            left.Right = right;
            return left;
        }

        public string ToString(IModel m)
        {
            StringBuilder sb = new StringBuilder();
            var ps = Oyster.Core.Orm.MReflection.GetModelColumns(m);
            if (ps != null)
            {
                OyOrderBy odby = this;
                do
                {
                    string nm = odby.Name;
                    if (m != null && ps.ContainsKey(nm))
                    {
                        nm = ps[nm];
                    }
                    sb.AppendFormat(",{0} {1}", nm, odby.Desc ? "desc" : "asc");
                    odby = odby.Right;
                }
                while (odby != null);
                if (sb.Length > 0)
                {
                    return " order by " + sb.ToString().Substring(1);
                }
            }
            return "";
        }
    }
}
