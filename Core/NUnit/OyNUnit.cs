using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Oyster.Core.Common;

namespace Oyster.Core.Comm
{
    public class OyTestFixtureAttribute : TestFixtureAttribute
    {

    }

    public class OyTestAttribute : TestAttribute
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
}
