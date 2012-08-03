using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oyster.Core.Common;

namespace Oyster.Core.Orm
{
    public partial class Condition
    {
        public string Name;
        public object Value;
        public bool IsExpressionCondition { get { return !string.IsNullOrEmpty(Expression); } }
        public string Expression;

        public ConditionOperator Op = ConditionOperator.Equal;
        public Condition Left;
        public Condition Right;

        public ConditionOperator MulOperate;

        public bool IsMulCondition
        {
            get
            {
                return Left != null && Right != null;
            }
        }


        public Condition(string name, object value)
            : this(name, ConditionOperator.Equal, value)
        {
        }
        public Condition(bool isexp, string expression, string name = "")
        {
            Expression = expression;
            Name = name;
        }
        public Condition(string name, ConditionOperator op, object value)
        {
            Name = name;
            Value = value;
            Op = op;
        }

        public Condition(Condition left, ConditionOperator op, Condition right)
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

        public static Condition operator |(Condition left, Condition right)
        {
            return new Condition(left, ConditionOperator.Or, right);
        }

        public static Condition operator &(Condition left, Condition right)
        {
            return new Condition(left, ConditionOperator.And, right);
        }
    }
}
