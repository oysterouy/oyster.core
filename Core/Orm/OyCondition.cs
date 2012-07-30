using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public partial class OyCondition
    {
        public string Name;
        public object Value;
        public bool IsExpressionCondition { get { return !string.IsNullOrEmpty(Expression); } }
        public string Expression;

        public ConditionOperator Op = ConditionOperator.Equal;
        public OyCondition Left;
        public OyCondition Right;

        public ConditionOperator MulOperate;

        public bool IsMulCondition
        {
            get
            {
                return Left != null && Right != null;
            }
        }


        public OyCondition(string name, object value)
            : this(name, ConditionOperator.Equal, value)
        {
        }
        public OyCondition(bool isexp, string expression, string name = "")
        {
            Expression = expression;
            Name = name;
        }
        public OyCondition(string name, ConditionOperator op, object value)
        {
            Name = name;
            Value = value;
            Op = op;
        }

        public OyCondition(OyCondition left, ConditionOperator op, OyCondition right)
        {
            if (op == ConditionOperator.And
                || op == ConditionOperator.Or)
            {
                Left = left;
                Right = right;
                MulOperate = op;
            }
            else
            {
                throw new Exception("two condition just can link by and ,or oper!");
            }
        }

        public static OyCondition operator |(OyCondition left, OyCondition right)
        {
            return new OyCondition(left, ConditionOperator.Or, right);
        }

        public static OyCondition operator &(OyCondition left, OyCondition right)
        {
            return new OyCondition(left, ConditionOperator.And, right);
        }
    }
}
