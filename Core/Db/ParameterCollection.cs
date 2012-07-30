using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace Oyster.Core.Db
{
    public class ParameterCollection : Dictionary<string, IDataParameter>
    {
        public const string MsSqlParamFormat = "@{0}_{1}";
        public const string MySqlParamFormat = "?{0}_{1}";
        public const string OracleParamFormat = ":{0}_{1}";
        public new string Add(string key, IDataParameter pam)
        {
            string pname = "";
            if (pam.Value != null && pam.Value is IList)
            {
                StringBuilder sber = new StringBuilder();
                IList values = pam.Value as IList;
                //ILIST的参数必须转换，否则需要排除掉,
                pam.Value = null;
                foreach (var v in values)
                {
                    IDataParameter d = Activator.CreateInstance(pam.GetType()) as IDataParameter;
                    d.Direction = pam.Direction;
                    d.Value = v;
                    sber.AppendFormat(",{0}", AddParameter("array_" + key, d));
                }
                if (sber.Length > 1)
                {
                    pname = sber.ToString().Substring(1);
                }
            }
            if (string.IsNullOrEmpty(pname) && pam.Value != null)
            {
                pname = AddParameter(key, pam);
            }
            return pname;
        }

        protected string AddParameter(string key, IDataParameter pam)
        {
            string format = OracleParamFormat;
            switch (DbEngine.Instance.Providertype)
            {
                case "Mssql":
                    format = MsSqlParamFormat;
                    break;
                case "Mysql":
                    format = MySqlParamFormat;
                    break;
                case "Oracle":
                    format = OracleParamFormat;
                    break;
            }
            string nk = string.Format(format, key, "0");
            int t = 1;
            while (true)
            {
                if (!base.ContainsKey(nk))
                {
                    pam.ParameterName = nk;
                    base.Add(nk, pam);
                    break;
                }
                nk = string.Format(format, key, t.ToString());
                t++;
            }
            return nk;
        }
    }
}
