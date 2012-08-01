using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace System
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
            Oyster.Core.Tool.OyContext.Instance.SetContext<OyNUniting>(this);
        }
        public void Dispose()
        {
            Oyster.Core.Tool.OyContext.Instance.SetContext<OyNUniting>(null);
        }

        public static bool IsUnitRuning
        {
            get
            {
                return Oyster.Core.Tool.OyContext.Instance.GetContext<OyNUniting>() != null;
            }
        }
    }
}
