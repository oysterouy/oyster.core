using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oyster.Core.Orm;

namespace Oyster.Core.Orm
{
    public class OrderBy
    {
        public string Name;
        public bool Desc = true;
        public OrderBy Right;
        public OrderBy(string name, bool isdesc = true, OrderBy right = null)
        {
            Name = name;
            Desc = isdesc;
            Right = right;
        }
        public static OrderBy operator &(OrderBy left, OrderBy right)
        {
            left.Right = right;
            return left;
        }

        public string ToString(IModel m)
        {
            StringBuilder sb = new StringBuilder();
            var ps = MReflection.GetModelColumns(m);
            if (ps != null)
            {
                OrderBy odby = this;
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
