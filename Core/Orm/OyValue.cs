using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oyster.Core.Db;
using Oyster.Core.Orm;

namespace System
{
    public class OyValue
    {
        public string Name;
        public object Value;
        public bool IsExpressionCondition { get { return !string.IsNullOrEmpty(Expression); } }
        public string Expression;

        public OyValue Right;

        public OyValue(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public OyValue(bool isexp, string expression, string name = "")
        {
            Expression = expression;
            Name = name;
        }

        public static OyValue operator &(OyValue left, OyValue right)
        {
            var lr = left;
            while (lr.Right != null)
            {
                lr = lr.Right;
            }
            lr.Right = right;
            return left;
        }

        public string ToString(IModel mode, ParameterCollection paramlist)
        {
            OyValue val = this;
            StringBuilder sbder = new StringBuilder();
            var diccols = MReflection.GetModelColumns(mode);
            if (!diccols.ContainsKey(val.Name))
            {
                return "";
            }
            do
            {
                string colname = diccols[val.Name];
                if (!string.IsNullOrEmpty(colname))
                {
                    var pam = DbEngine.Instance.NewDataParameter(colname);
                    switch (colname)
                    {
                        case "lastchange_time":
                            pam.Value = DateTime.Now;
                            break;
                        default:
                            pam.Value = val.Value;
                            if (pam.Value == null)
                            {
                                pam.Value = "";
                            }
                            break;
                    }
                    string ptname = paramlist.Add(colname, pam);
                    pam.ParameterName = ptname;

                    sbder.AppendFormat(",{0}={1}", colname, pam.ParameterName);
                }
                val = val.Right;
            }
            while (val != null);
            if (sbder.Length > 0)
            {
                return sbder.ToString().Substring(1);
            }
            return "";
        }

        public IModel Update(IModel mode)
        {
            var ps = MReflection.GetMReflections(mode.zModelType);
            OyValue val = this;
            if (ps != null)
            {
                do
                {
                    if (ps.ContainsKey(val.Name))
                    {
                        var mr = ps[val.Name];
                        mr.SetValue(mode, val.Value);
                    }
                    val = val.Right;
                }
                while (val != null);
            }

            return mode;
        }
    }
}
