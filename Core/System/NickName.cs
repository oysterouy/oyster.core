
using Oyster.Core.Common;
using NUnit.Framework;
using Oyster.Core.Orm;
namespace System
{
    #region OyOrm

    public class OyCondition : Oyster.Core.Orm.Condition
    {
        public OyCondition(string name, object value)
            : this(name, ConditionOperator.Equal, value)
        {
        }
        public OyCondition(bool isexp, string expression, string name = "")
            : base(isexp, expression, name)
        {
        }

        public OyCondition(string name, ConditionOperator op, object value)
            : base(name, op, value)
        {
        }

        public OyCondition(OyCondition left, ConditionOperator op, OyCondition right)
            : base(left, op, right)
        {
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
    public class OyContext : ContextHelper
    {

    }

    public class OyOrderBy : Oyster.Core.Orm.OrderBy
    {
        public OyOrderBy(string name, bool isdesc = true, OrderBy right = null)
            : base(name, isdesc, right)
        {
        }
        public static OyOrderBy operator &(OyOrderBy left, OrderBy OyOrderBy)
        {
            return ((left as OrderBy) & (OyOrderBy as OrderBy)) as OyOrderBy;
        }
    }
    public class OyValue : Oyster.Core.Orm.ValuePair
    {
        public OyValue(string name, object value)
            : base(name, value)
        {
        }

        public OyValue(bool isexp, string expression, string name = "")
            : base(isexp, expression, name)
        {

        }

        public static OyValue operator &(OyValue left, OyValue right)
        {
            return ((left as ValuePair) & (right as ValuePair)) as OyValue;
        }
    }

    public class OyLogger : Oyster.Core.Logger.Logger
    {

    }

    #endregion

    #region NUnit

    public class OyTestFixtureAttribute : TestFixtureAttribute
    {

    }

    public class OyTestAttribute : TestAttribute
    {

    }
    public class OyAssert : Assert
    {

    }
    public class OyNUniting : IDisposable
    {
        public OyNUniting()
        {
            ContextHelper.Instance.SetContext<OyNUniting>(this);
        }
        public void Dispose()
        {
            ContextHelper.Instance.SetContext<OyNUniting>(null);
        }

        public static bool IsUnitRuning
        {
            get
            {
                return ContextHelper.Instance.GetContext<OyNUniting>() != null;
            }
        }
    }

    #endregion
}
