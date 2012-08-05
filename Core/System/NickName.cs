
using Oyster.Core.Common;
using NUnit.Framework;
using Oyster.Core.Orm;
namespace System
{
    #region OyOrm

    public class OyCondition : Oyster.Core.Orm.Condition
    {
        public OyCondition(bool isexp, string expression, string name = "")
            : base(isexp, expression, name)
        {
        }

        public OyCondition(string name, ConditionOperator op, object value)
            : base(name, op, value)
        {
        }

        public OyCondition(Condition left, ConditionOperator op, Condition right)
            : base(left, op, right)
        {
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
