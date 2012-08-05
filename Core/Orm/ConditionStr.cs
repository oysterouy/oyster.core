using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Oyster.Core.Orm;
using Oyster.Core.Db;
using Oyster.Core.Common;

namespace Oyster.Core.Orm
{
    public partial class Condition
    {
        public string ToString(IModel mode)
        {
            if (mode == null)
            {
                throw new Exception("IModel mode can not be null!");
            }
            string retstr = "";
            string exp = "({0} {1} {2})";
            Condition condition = this;
            if (condition.IsMulCondition)
            {
                string left = "";
                string right = "";
                if (condition.Left != null)
                {
                    left = condition.Left.ToString(mode);
                }
                if (condition.Right != null)
                {
                    right = condition.Right.ToString(mode);
                }

                if (!string.IsNullOrEmpty(left) && !string.IsNullOrEmpty(right))
                {
                    retstr = String.Format(exp, left, condition.MulOperate.Equals(ConditionOperator.And) ? "AND" : "OR", right);
                }
                else
                {
                    if (!string.IsNullOrEmpty(left))
                    {
                        retstr = left;
                    }
                    else
                    {
                        retstr = right;
                    }
                }
            }
            else
            {
                if (condition.IsExpressionCondition)
                {
                    if (!string.IsNullOrEmpty(condition.Name))
                    {
                        retstr = string.Format(condition.Expression, condition.Name);
                    }
                    else
                    {
                        retstr = condition.Expression;
                    }
                }
                else
                {
                    var diccols = MReflection.GetModelColumns(mode);
                    if (!diccols.ContainsKey(condition.Name))
                    {
                        return "";
                    }

                    object Value = condition.Value;
                    string col = diccols[condition.Name];
                    string val = "";
                    if (Value != null)
                    {
                        if (string.IsNullOrEmpty(val) && Value is IList)
                        {
                            StringBuilder sbder = new StringBuilder();
                            foreach (var v in Value as IList)
                            {
                                if (v is string || v is DateTime)
                                {
                                    sbder.AppendFormat(",'{0}'", v);
                                }
                                else
                                {
                                    sbder.AppendFormat(",{0}", v);
                                }
                            }
                            if (sbder.Length > 1)
                            {
                                val = sbder.ToString().Substring(1);
                            }
                        }
                        if (string.IsNullOrEmpty(val))
                        {
                            val = Value.ToString();
                        }
                    }

                    switch (condition.Op)
                    {
                        case ConditionOperator.Equal:
                            if (typeof(DateTime).Equals(Value.GetType())
                                || typeof(String).Equals(Value.GetType()))
                            {
                                retstr = String.Format("{0}='{1}'", col, val);
                            }
                            else
                            {
                                retstr = String.Format("{0}={1}", col, val);
                            }
                            break;
                        case ConditionOperator.NotEqual:
                            if (typeof(DateTime).Equals(Value.GetType())
                                || typeof(String).Equals(Value.GetType()))
                            {
                                retstr = String.Format("{0}!='{1}'", col, val);
                            }
                            else
                            {
                                retstr = String.Format("{0}!={1}", col, val);
                            }
                            break;
                        case ConditionOperator.LeftLike:
                            retstr = String.Format("{0} like '%{1}'", col, val);
                            break;
                        case ConditionOperator.RightLike:
                            retstr = String.Format("{0} like '{1}%'", col, val);
                            break;
                        case ConditionOperator.Like:
                            retstr = String.Format("{0} like '%{1}%'", col, val);
                            break;
                        case ConditionOperator.Greater:
                            if (typeof(DateTime).Equals(Value.GetType()))
                            {
                                if (DbEngine.Instance.Providertype == "Oracle")
                                {
                                    retstr = String.Format("{0} > to_date('{1}','yyyy-MM-dd HH24:mi:ss')", col
                                        , ((DateTime)Value).ToString("yyyy-MM-dd HH:mm:ss"));
                                }
                                else
                                {
                                    retstr = String.Format("{0} > '{1}'", col, ((DateTime)Value).ToString("yyyy-MM-dd HH:mm:ss"));
                                }
                            }
                            else
                            {
                                retstr = String.Format("{0} > {1}", col, val);
                            }
                            break;
                        case ConditionOperator.GreaterThanOrEqual:
                            if (typeof(DateTime).Equals(Value.GetType()))
                            {
                                if (DbEngine.Instance.Providertype == "Oracle")
                                {
                                    retstr = String.Format("{0} >= to_date('{1}','yyyy-MM-dd HH24:mi:ss')", col
                                        , ((DateTime)Value).ToString("yyyy-MM-dd HH:mm:ss"));
                                }
                                else
                                {
                                    retstr = String.Format("{0} >= '{1}'", col, ((DateTime)Value).ToString("yyyy-MM-dd HH:mm:ss"));
                                }
                            }
                            else
                            {
                                retstr = String.Format("{0} >= {1}", col, val);
                            }
                            break;
                        case ConditionOperator.Less:
                            if (typeof(DateTime).Equals(Value.GetType()))
                            {
                                if (DbEngine.Instance.Providertype == "Oracle")
                                {
                                    retstr = String.Format("{0} < to_date('{1}','yyyy-MM-dd HH24:mi:ss')", col
                                        , ((DateTime)Value).ToString("yyyy-MM-dd HH:mm:ss"));
                                }
                                else
                                {
                                    retstr = String.Format("{0} < '{1}'", col, ((DateTime)Value).ToString("yyyy-MM-dd HH:mm:ss"));
                                }
                            }
                            else
                            {
                                retstr = String.Format("{0} < {1}", col, val);
                            }
                            break;
                        case ConditionOperator.LessThanOrEqual:
                            if (typeof(DateTime).Equals(Value.GetType()))
                            {
                                if (DbEngine.Instance.Providertype == "Oracle")
                                {
                                    retstr = String.Format("{0} <= to_date('{1}','yyyy-MM-dd HH24:mi:ss')", col
                                        , ((DateTime)Value).ToString("yyyy-MM-dd HH:mm:ss"));
                                }
                                else
                                {
                                    retstr = String.Format("{0} <= '{1}'", col, ((DateTime)Value).ToString("yyyy-MM-dd HH:mm:ss"));
                                }
                            }
                            else
                            {
                                retstr = String.Format("{0} <= {1}", col, val);
                            }
                            break;
                        case ConditionOperator.IsNull:
                            retstr = String.Format("{0} IS NULL", col);
                            break;
                        case ConditionOperator.NotIsNull:
                            retstr = String.Format("{0} IS NOT NULL", col);
                            break;
                        case ConditionOperator.In:
                            retstr = String.Format("{0} IN ({1})", col, val);
                            break;
                        case ConditionOperator.NotIn:
                            retstr = String.Format("{0} NOT IN ({1})", col, val);
                            break;
                        default:
                            retstr = String.Format("{0} {1} {2}", col, condition.Op.ToString(), val);
                            break;
                    }
                }
            }
            return retstr;
        }

        public string ToString(IModel mode, ParameterCollection paramlist)
        {
            paramlist = paramlist == null ? new ParameterCollection() : paramlist;
            if (mode == null)
            {
                throw new Exception("IModel mode can not be null!");
            }
            string retstr = "";
            string exp = "({0} {1} {2})";
            Condition condition = this;
            if (condition.IsMulCondition)
            {
                string left = "";
                string right = "";
                if (condition.Left != null)
                {
                    left = condition.Left.ToString(mode, paramlist);
                }
                if (condition.Right != null)
                {
                    right = condition.Right.ToString(mode, paramlist);
                }
                if (!string.IsNullOrEmpty(left) && !string.IsNullOrEmpty(right))
                {
                    retstr = String.Format(exp, left, condition.MulOperate.Equals(ConditionOperator.And) ? "AND" : "OR", right);
                }
                else
                {
                    if (!string.IsNullOrEmpty(left))
                    {
                        retstr = left;
                    }
                    else
                    {
                        retstr = right;
                    }
                }
            }
            else
            {
                if (condition.IsExpressionCondition)
                {
                    if (!string.IsNullOrEmpty(condition.Name))
                    {
                        retstr = string.Format(condition.Expression, condition.Name);
                    }
                    else
                    {
                        retstr = condition.Expression;
                    }
                }
                else
                {
                    var diccols = MReflection.GetModelColumns(mode);
                    if (!diccols.ContainsKey(condition.Name))
                    {
                        return "";
                    }

                    object Value = condition.Value;
                    string col = diccols[condition.Name];
                    var pam = DbEngine.Instance.NewDataParameter(col);
                    pam.Value = condition.Value;
                    string ptname = paramlist.Add(col, pam);

                    if (!string.IsNullOrEmpty(ptname))
                    {
                        switch (condition.Op)
                        {
                            case ConditionOperator.Equal:
                                retstr = String.Format("{0}={1}", col, ptname);
                                break;
                            case ConditionOperator.NotEqual:
                                retstr = String.Format("{0}!={1}", col, ptname);
                                break;
                            case ConditionOperator.LeftLike:
                                retstr = String.Format("{0} like {1}", col, ptname);
                                pam.Value = "%" + pam.Value;
                                break;
                            case ConditionOperator.RightLike:
                                retstr = String.Format("{0} like {1}", col, ptname);
                                pam.Value = pam.Value + "%";
                                break;
                            case ConditionOperator.Like:
                                retstr = String.Format("{0} like {1}", col, ptname);
                                pam.Value = "%" + pam.Value + "%";
                                break;
                            case ConditionOperator.Greater:
                                retstr = String.Format("{0} > {1}", col, ptname);
                                break;
                            case ConditionOperator.GreaterThanOrEqual:
                                retstr = String.Format("{0} >= {1}", col, ptname);
                                break;
                            case ConditionOperator.Less:
                                retstr = String.Format("{0} < {1}", col, ptname);
                                break;
                            case ConditionOperator.LessThanOrEqual:
                                retstr = String.Format("{0} <= {1}", col, ptname);
                                break;
                            case ConditionOperator.IsNull:
                                retstr = String.Format("{0} IS NULL", col);
                                break;
                            case ConditionOperator.NotIsNull:
                                retstr = String.Format("{0} IS NOT NULL", col);
                                break;
                            case ConditionOperator.In:
                                retstr = String.Format("{0} IN ({1})", col, ptname);
                                break;
                            case ConditionOperator.NotIn:
                                retstr = String.Format("{0} NOT IN ({1})", col, ptname);
                                break;

                            default:
                                retstr = String.Format("{0} {1} {2}", col, condition.Op.ToString(), ptname);
                                break;
                        }
                    }
                }
            }

            return retstr;
        }

        public bool IsMatch(IModel mode)
        {
            bool match = false;
            if (mode == null)
            {
                throw new Exception("IModel mode can not be null!");
            }
            Condition condition = this;
            if (condition.IsMulCondition)
            {
                bool lm = false, rm = false;
                if (condition.Left != null)
                {
                    lm = condition.Left.IsMatch(mode);
                }
                if (condition.Right != null)
                {
                    rm = condition.Right.IsMatch(mode);
                }
                if (condition.MulOperate == ConditionOperator.And)
                {
                    match = lm && rm;
                }
                else
                {
                    match = lm || rm;
                }
            }
            else
            {
                var ps = MReflection.GetMReflections(mode.zModelType);
                if (ps != null && ps.ContainsKey(condition.Name))
                {
                    object vd = ps[condition.Name].GetValue(mode);

                    switch (condition.Op)
                    {
                        case ConditionOperator.Equal:
                            match = condition.Value.Equals(vd);
                            break;
                        case ConditionOperator.NotEqual:
                            match = !condition.Value.Equals(vd);
                            break;
                        case ConditionOperator.LeftLike:
                            if (vd != null && condition.Value != null
                                && vd.ToString().StartsWith(condition.Value.ToString()))
                            {
                                match = true;
                            }
                            break;
                        case ConditionOperator.RightLike:
                            if (vd != null && condition.Value != null
                                 && vd.ToString().EndsWith(condition.Value.ToString()))
                            {
                                match = true;
                            }
                            break;
                        case ConditionOperator.Like:
                            if (vd != null && condition.Value != null
                                  && vd.ToString().Contains(condition.Value.ToString()))
                            {
                                match = true;
                            }
                            break;
                        case ConditionOperator.Greater:
                            if (vd is DateTime && condition.Value is DateTime
                                && (DateTime)vd > (DateTime)condition.Value)
                            {
                                match = true;
                            }
                            double dl = 0, dr = 0;
                            if (vd != null && condition.Value != null)
                            {
                                if (double.TryParse(vd.ToString(), out dl) && double.TryParse(condition.Value.ToString(), out dr))
                                {
                                    if (dl > dr)
                                    {
                                        match = true;
                                    }
                                }
                            }

                            break;
                        case ConditionOperator.GreaterThanOrEqual:
                            if (vd is DateTime && condition.Value is DateTime
                                && (DateTime)vd >= (DateTime)condition.Value)
                            {
                                match = true;
                            }
                            dl = 0; dr = 0;
                            if (vd != null && condition.Value != null)
                            {
                                if (double.TryParse(vd.ToString(), out dl) && double.TryParse(condition.Value.ToString(), out dr))
                                {
                                    if (dl >= dr)
                                    {
                                        match = true;
                                    }
                                }
                            }
                            break;
                        case ConditionOperator.Less:
                            if (vd is DateTime && condition.Value is DateTime
                                && (DateTime)vd < (DateTime)condition.Value)
                            {
                                match = true;
                            }
                            dl = 0; dr = 0;
                            if (vd != null && condition.Value != null)
                            {
                                if (double.TryParse(vd.ToString(), out dl) && double.TryParse(condition.Value.ToString(), out dr))
                                {
                                    if (dl < dr)
                                    {
                                        match = true;
                                    }
                                }
                            }
                            break;
                        case ConditionOperator.LessThanOrEqual:

                            if (vd is DateTime && condition.Value is DateTime
                                && (DateTime)vd <= (DateTime)condition.Value)
                            {
                                match = true;
                            }
                            dl = 0; dr = 0;
                            if (vd != null && condition.Value != null)
                            {
                                if (double.TryParse(vd.ToString(), out dl) && double.TryParse(condition.Value.ToString(), out dr))
                                {
                                    if (dl <= dr)
                                    {
                                        match = true;
                                    }
                                }
                            }
                            break;
                        case ConditionOperator.IsNull:
                            match = vd == null;
                            break;
                        case ConditionOperator.NotIsNull:
                            match = vd != null;
                            break;
                        case ConditionOperator.In:
                            if (condition.Value is IList)
                            {
                                var ls = condition.Value as IList;
                                match = ls.Contains(vd);
                            }
                            if (condition.Value is string && condition.Value != null && vd != null)
                            {
                                match = condition.Value.ToString().Contains(vd.ToString());
                            }
                            break;
                        case ConditionOperator.NotIn:
                            if (condition.Value is IList)
                            {
                                var ls = condition.Value as IList;
                                match = !ls.Contains(vd);
                            }
                            if (condition.Value is string && condition.Value != null && vd != null)
                            {
                                match = !condition.Value.ToString().Contains(vd.ToString());
                            }
                            break;
                        default:
                            match = vd.Equals(condition.Value);
                            break;
                    }
                }
            }
            return match;
        }
    }
}
