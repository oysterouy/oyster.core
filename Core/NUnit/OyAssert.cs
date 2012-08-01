using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections;
using System.ComponentModel;

namespace System
{
    public class OyAssert //: Assert
    {
        private static int counter;
        public static int Counter
        {
            get
            {
                int counter = OyAssert.counter;
                OyAssert.counter = 0;
                return counter;
            }
        }

        public static void AreEqual(decimal expected, decimal actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), null, null);
        }

        public static void AreEqual(int expected, int actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), null, null);
        }

        public static void AreEqual(long expected, long actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), null, null);
        }

        public static void AreEqual(object expected, object actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), null, null);
        }

        public static void AreEqual(uint expected, uint actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), null, null);
        }

        public static void AreEqual(ulong expected, ulong actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), null, null);
        }

        public static void AreEqual(decimal expected, decimal actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), message, null);
        }

        public static void AreEqual(double expected, double? actual, double delta)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            AssertDoublesAreEqual(expected, actual.Value, delta, null, null);
        }

        public static void AreEqual(double expected, double actual, double delta)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            AssertDoublesAreEqual(expected, actual, delta, null, null);
        }

        public static void AreEqual(int expected, int actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), message, null);
        }

        public static void AreEqual(long expected, long actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), message, null);
        }

        public static void AreEqual(object expected, object actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), message, null);
        }

        public static void AreEqual(uint expected, uint actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), message, null);
        }

        public static void AreEqual(ulong expected, ulong actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), message, null);
        }

        public static void AreEqual(decimal expected, decimal actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), message, args);
        }

        public static void AreEqual(double expected, double? actual, double delta, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            AssertDoublesAreEqual(expected, actual.Value, delta, message, null);
        }

        public static void AreEqual(double expected, double actual, double delta, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            AssertDoublesAreEqual(expected, actual, delta, message, null);
        }

        public static void AreEqual(int expected, int actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), message, args);
        }

        public static void AreEqual(long expected, long actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), message, args);
        }

        public static void AreEqual(object expected, object actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), message, args);
        }

        public static void AreEqual(uint expected, uint actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), message, args);
        }

        public static void AreEqual(ulong expected, ulong actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.EqualTo(expected), message, args);
        }

        public static void AreEqual(double expected, double? actual, double delta, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            AssertDoublesAreEqual(expected, actual.Value, delta, message, args);
        }

        public static void AreEqual(double expected, double actual, double delta, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            AssertDoublesAreEqual(expected, actual, delta, message, args);
        }

        public static void AreNotEqual(decimal expected, decimal actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), null, null);
        }

        public static void AreNotEqual(double expected, double actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), null, null);
        }

        public static void AreNotEqual(int expected, int actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), null, null);
        }

        public static void AreNotEqual(long expected, long actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), null, null);
        }

        public static void AreNotEqual(object expected, object actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), null, null);
        }

        public static void AreNotEqual(float expected, float actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), null, null);
        }

        public static void AreNotEqual(uint expected, uint actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), null, null);
        }

        public static void AreNotEqual(ulong expected, ulong actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), null, null);
        }

        public static void AreNotEqual(decimal expected, decimal actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), message, null);
        }

        public static void AreNotEqual(double expected, double actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), message, null);
        }

        public static void AreNotEqual(int expected, int actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), message, null);
        }

        public static void AreNotEqual(long expected, long actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), message, null);
        }

        public static void AreNotEqual(object expected, object actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), message, null);
        }

        public static void AreNotEqual(float expected, float actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), message, null);
        }

        public static void AreNotEqual(uint expected, uint actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), message, null);
        }

        public static void AreNotEqual(ulong expected, ulong actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), message, null);
        }

        public static void AreNotEqual(decimal expected, decimal actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), message, args);
        }

        public static void AreNotEqual(double expected, double actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), message, args);
        }

        public static void AreNotEqual(int expected, int actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), message, args);
        }

        public static void AreNotEqual(long expected, long actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), message, args);
        }

        public static void AreNotEqual(object expected, object actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), message, args);
        }

        public static void AreNotEqual(float expected, float actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), message, args);
        }

        public static void AreNotEqual(uint expected, uint actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), message, args);
        }

        public static void AreNotEqual(ulong expected, ulong actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.EqualTo(expected), message, args);
        }

        public static void AreNotSame(object expected, object actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.SameAs(expected), null, null);
        }

        public static void AreNotSame(object expected, object actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.SameAs(expected), message, null);
        }

        public static void AreNotSame(object expected, object actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.SameAs(expected), message, args);
        }

        public static void AreSame(object expected, object actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.SameAs(expected), null, null);
        }

        public static void AreSame(object expected, object actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.SameAs(expected), message, null);
        }

        public static void AreSame(object expected, object actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.SameAs(expected), message, args);
        }

        protected static void AssertDoublesAreEqual(double expected, double actual, double delta, string message, object[] args)
        {
            if (double.IsNaN(expected) || double.IsInfinity(expected))
            {
                That(actual, Is.EqualTo(expected), message, args);
            }
            else
            {
                That(actual, Is.EqualTo(expected).Within(delta), message, args);
            }
        }

        public static void ByVal(object actual, IResolveConstraint expression)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, expression, null, null);
        }

        public static void ByVal(object actual, IResolveConstraint expression, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, expression, message, null);
        }

        public static void ByVal(object actual, IResolveConstraint expression, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, expression, message, args);
        }

        public static Exception Catch(TestDelegate code)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return null;
            return Throws(new InstanceOfTypeConstraint(typeof(Exception)), code);
        }

        public static T Catch<T>(TestDelegate code) where T : Exception
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return default(T);
            return (T)Throws(new InstanceOfTypeConstraint(typeof(T)), code);
        }

        public static T Catch<T>(TestDelegate code, string message) where T : Exception
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return default(T);
            return (T)Throws(new InstanceOfTypeConstraint(typeof(T)), code, message);
        }

        public static Exception Catch(TestDelegate code, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return null;
            return Throws(new InstanceOfTypeConstraint(typeof(Exception)), code, message);
        }

        public static Exception Catch(Type expectedExceptionType, TestDelegate code)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return null;
            return Throws(new InstanceOfTypeConstraint(expectedExceptionType), code);
        }

        public static Exception Catch(TestDelegate code, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return null;
            return Throws(new InstanceOfTypeConstraint(typeof(Exception)), code, message, args);
        }

        public static T Catch<T>(TestDelegate code, string message, params object[] args) where T : Exception
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return default(T);
            return (T)Throws(new InstanceOfTypeConstraint(typeof(T)), code, message, args);
        }

        public static Exception Catch(Type expectedExceptionType, TestDelegate code, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return null;
            return Throws(new InstanceOfTypeConstraint(expectedExceptionType), code, message);
        }

        public static Exception Catch(Type expectedExceptionType, TestDelegate code, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return null;
            return Throws(new InstanceOfTypeConstraint(expectedExceptionType), code, message, args);
        }

        public static void Contains(object expected, ICollection actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, new CollectionContainsConstraint(expected), null, null);
        }

        public static void Contains(object expected, ICollection actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, new CollectionContainsConstraint(expected), message, null);
        }

        public static void Contains(object expected, ICollection actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, new CollectionContainsConstraint(expected), message, args);
        }

        public static void DoesNotThrow(TestDelegate code)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            DoesNotThrow(code, string.Empty, null);
        }

        public static void DoesNotThrow(TestDelegate code, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            DoesNotThrow(code, message, null);
        }

        public static void DoesNotThrow(TestDelegate code, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            try
            {
                code();
            }
            catch (Exception exception)
            {
                TextMessageWriter writer = new TextMessageWriter(message, args);
                writer.WriteLine("Unexpected exception: {0}", exception.GetType());
                Fail(writer.ToString());
            }
        }

        public static void Fail()
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            Fail(string.Empty, null);
        }

        public static void Fail(string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            Fail(message, null);
        }

        public static void Fail(string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            if (message == null)
            {
                message = string.Empty;
            }
            else if ((args != null) && (args.Length > 0))
            {
                message = string.Format(message, args);
            }
            throw new AssertionException(message);
        }

        public static void False(bool condition)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(condition, Is.False, null, null);
        }

        public static void False(bool condition, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(condition, Is.False, message, null);
        }

        public static void False(bool condition, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(condition, Is.False, message, args);
        }

        public static void Greater(decimal arg1, decimal arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), null, null);
        }

        public static void Greater(double arg1, double arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), null, null);
        }

        public static void Greater(IComparable arg1, IComparable arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), null, null);
        }

        public static void Greater(int arg1, int arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), null, null);
        }

        public static void Greater(long arg1, long arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), null, null);
        }

        public static void Greater(float arg1, float arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), null, null);
        }

        public static void Greater(uint arg1, uint arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), null, null);
        }

        public static void Greater(ulong arg1, ulong arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), null, null);
        }

        public static void Greater(decimal arg1, decimal arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), message, null);
        }

        public static void Greater(double arg1, double arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), message, null);
        }

        public static void Greater(IComparable arg1, IComparable arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), message, null);
        }

        public static void Greater(int arg1, int arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), message, null);
        }

        public static void Greater(long arg1, long arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), message, null);
        }

        public static void Greater(float arg1, float arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), message, null);
        }

        public static void Greater(uint arg1, uint arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), message, null);
        }

        public static void Greater(ulong arg1, ulong arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), message, null);
        }

        public static void Greater(decimal arg1, decimal arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), message, args);
        }

        public static void Greater(double arg1, double arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), message, args);
        }

        public static void Greater(IComparable arg1, IComparable arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), message, args);
        }

        public static void Greater(int arg1, int arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), message, args);
        }

        public static void Greater(long arg1, long arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), message, args);
        }

        public static void Greater(float arg1, float arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), message, args);
        }

        public static void Greater(uint arg1, uint arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), message, args);
        }

        public static void Greater(ulong arg1, ulong arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThan(arg2), message, args);
        }

        public static void GreaterOrEqual(decimal arg1, decimal arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), null, null);
        }

        public static void GreaterOrEqual(double arg1, double arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), null, null);
        }

        public static void GreaterOrEqual(IComparable arg1, IComparable arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), null, null);
        }

        public static void GreaterOrEqual(int arg1, int arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), null, null);
        }

        public static void GreaterOrEqual(long arg1, long arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), null, null);
        }

        public static void GreaterOrEqual(float arg1, float arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), null, null);
        }

        public static void GreaterOrEqual(uint arg1, uint arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), null, null);
        }

        public static void GreaterOrEqual(ulong arg1, ulong arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), null, null);
        }

        public static void GreaterOrEqual(decimal arg1, decimal arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, null);
        }

        public static void GreaterOrEqual(double arg1, double arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, null);
        }

        public static void GreaterOrEqual(IComparable arg1, IComparable arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, null);
        }

        public static void GreaterOrEqual(int arg1, int arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, null);
        }

        public static void GreaterOrEqual(long arg1, long arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, null);
        }

        public static void GreaterOrEqual(float arg1, float arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, null);
        }

        public static void GreaterOrEqual(uint arg1, uint arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, null);
        }

        public static void GreaterOrEqual(ulong arg1, ulong arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, null);
        }

        public static void GreaterOrEqual(decimal arg1, decimal arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
        }

        public static void GreaterOrEqual(double arg1, double arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
        }

        public static void GreaterOrEqual(IComparable arg1, IComparable arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
        }

        public static void GreaterOrEqual(int arg1, int arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
        }

        public static void GreaterOrEqual(long arg1, long arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
        }

        public static void GreaterOrEqual(float arg1, float arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
        }

        public static void GreaterOrEqual(uint arg1, uint arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
        }

        public static void GreaterOrEqual(ulong arg1, ulong arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
        }

        public static void Ignore()
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            Ignore(string.Empty, null);
        }

        public static void Ignore(string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            Ignore(message, null);
        }

        public static void Ignore(string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            if (message == null)
            {
                message = string.Empty;
            }
            else if ((args != null) && (args.Length > 0))
            {
                message = string.Format(message, args);
            }
            throw new IgnoreException(message);
        }

        public static void Inconclusive()
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            Inconclusive(string.Empty, null);
        }

        public static void Inconclusive(string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            Inconclusive(message, null);
        }

        public static void Inconclusive(string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            if (message == null)
            {
                message = string.Empty;
            }
            else if ((args != null) && (args.Length > 0))
            {
                message = string.Format(message, args);
            }
            throw new InconclusiveException(message);
        }

        private static void IncrementAssertCount()
        {
            counter++;
        }

        public static void IsAssignableFrom<T>(object actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.AssignableFrom(typeof(T)), null, null);
        }

        public static void IsAssignableFrom<T>(object actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.AssignableFrom(typeof(T)), message, null);
        }

        public static void IsAssignableFrom(Type expected, object actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.AssignableFrom(expected), null, null);
        }

        public static void IsAssignableFrom<T>(object actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.AssignableFrom(typeof(T)), message, args);
        }

        public static void IsAssignableFrom(Type expected, object actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.AssignableFrom(expected), message, null);
        }

        public static void IsAssignableFrom(Type expected, object actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.AssignableFrom(expected), message, args);
        }

        public static void IsEmpty(IEnumerable collection)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(collection, new EmptyCollectionConstraint(), null, null);
        }

        public static void IsEmpty(string aString)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aString, new EmptyStringConstraint(), null, null);
        }

        public static void IsEmpty(IEnumerable collection, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(collection, new EmptyCollectionConstraint(), message, null);
        }

        public static void IsEmpty(string aString, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aString, new EmptyStringConstraint(), message, null);
        }

        public static void IsEmpty(IEnumerable collection, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(collection, new EmptyCollectionConstraint(), message, args);
        }

        public static void IsEmpty(string aString, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aString, new EmptyStringConstraint(), message, args);
        }

        public static void IsFalse(bool condition)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(condition, Is.False, null, null);
        }

        public static void IsFalse(bool condition, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(condition, Is.False, message, null);
        }

        public static void IsFalse(bool condition, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(condition, Is.False, message, args);
        }

        public static void IsInstanceOf<T>(object actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.InstanceOf(typeof(T)), null, null);
        }

        public static void IsInstanceOf<T>(object actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.InstanceOf(typeof(T)), message, null);
        }

        public static void IsInstanceOf(Type expected, object actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.InstanceOf(expected), null, null);
        }

        public static void IsInstanceOf<T>(object actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.InstanceOf(typeof(T)), message, args);
        }

        public static void IsInstanceOf(Type expected, object actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.InstanceOf(expected), message, null);
        }

        public static void IsInstanceOf(Type expected, object actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.InstanceOf(expected), message, args);
        }

        public static void IsNaN(double? aDouble)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aDouble, Is.NaN, null, null);
        }

        public static void IsNaN(double aDouble)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aDouble, Is.NaN, null, null);
        }

        public static void IsNaN(double? aDouble, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aDouble, Is.NaN, message, null);
        }

        public static void IsNaN(double aDouble, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aDouble, Is.NaN, message, null);
        }

        public static void IsNaN(double aDouble, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aDouble, Is.NaN, message, args);
        }

        public static void IsNaN(double? aDouble, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aDouble, Is.NaN, message, args);
        }

        public static void IsNotAssignableFrom<T>(object actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.AssignableFrom(typeof(T)), null, null);
        }

        public static void IsNotAssignableFrom<T>(object actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.AssignableFrom(typeof(T)), message, null);
        }

        public static void IsNotAssignableFrom(Type expected, object actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.AssignableFrom(expected), null, null);
        }

        public static void IsNotAssignableFrom<T>(object actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.AssignableFrom(typeof(T)), message, args);
        }

        public static void IsNotAssignableFrom(Type expected, object actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.AssignableFrom(expected), message, null);
        }

        public static void IsNotAssignableFrom(Type expected, object actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.AssignableFrom(expected), message, args);
        }

        public static void IsNotEmpty(IEnumerable collection)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(collection, Is.Not.Empty, null, null);
        }

        public static void IsNotEmpty(string aString)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aString, Is.Not.Empty, null, null);
        }

        public static void IsNotEmpty(IEnumerable collection, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(collection, Is.Not.Empty, message, null);
        }

        public static void IsNotEmpty(string aString, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aString, Is.Not.Empty, message, null);
        }

        public static void IsNotEmpty(IEnumerable collection, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(collection, Is.Not.Empty, message, args);
        }

        public static void IsNotEmpty(string aString, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aString, Is.Not.Empty, message, args);
        }

        public static void IsNotInstanceOf<T>(object actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.InstanceOf(typeof(T)), null, null);
        }

        public static void IsNotInstanceOf<T>(object actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.InstanceOf(typeof(T)), message, null);
        }

        public static void IsNotInstanceOf(Type expected, object actual)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.InstanceOf(expected), null, null);
        }

        public static void IsNotInstanceOf<T>(object actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.InstanceOf(typeof(T)), message, args);
        }

        public static void IsNotInstanceOf(Type expected, object actual, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.InstanceOf(expected), message, null);
        }

        public static void IsNotInstanceOf(Type expected, object actual, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, Is.Not.InstanceOf(expected), message, args);
        }

        public static void IsNotNull(object anObject)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(anObject, Is.Not.Null, null, null);
        }

        public static void IsNotNull(object anObject, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(anObject, Is.Not.Null, message, null);
        }

        public static void IsNotNull(object anObject, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(anObject, Is.Not.Null, message, args);
        }

        public static void IsNotNullOrEmpty(string aString)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aString, new NotConstraint(new NullOrEmptyStringConstraint()), null, null);
        }

        public static void IsNotNullOrEmpty(string aString, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aString, new NotConstraint(new NullOrEmptyStringConstraint()), message, null);
        }

        public static void IsNotNullOrEmpty(string aString, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aString, new NotConstraint(new NullOrEmptyStringConstraint()), message, args);
        }

        public static void IsNull(object anObject)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(anObject, Is.Null, null, null);
        }

        public static void IsNull(object anObject, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(anObject, Is.Null, message, null);
        }

        public static void IsNull(object anObject, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(anObject, Is.Null, message, args);
        }

        public static void IsNullOrEmpty(string aString)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aString, new NullOrEmptyStringConstraint(), null, null);
        }

        public static void IsNullOrEmpty(string aString, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aString, new NullOrEmptyStringConstraint(), message, null);
        }

        public static void IsNullOrEmpty(string aString, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(aString, new NullOrEmptyStringConstraint(), message, args);
        }

        public static void IsTrue(bool condition)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(condition, Is.True, null, null);
        }

        public static void IsTrue(bool condition, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(condition, Is.True, message, null);
        }

        public static void IsTrue(bool condition, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(condition, Is.True, message, args);
        }

        public static void Less(decimal arg1, decimal arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), null, null);
        }

        public static void Less(double arg1, double arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), null, null);
        }

        public static void Less(IComparable arg1, IComparable arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), null, null);
        }

        public static void Less(int arg1, int arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), null, null);
        }

        public static void Less(long arg1, long arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), null, null);
        }

        public static void Less(float arg1, float arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), null, null);
        }

        public static void Less(uint arg1, uint arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), null, null);
        }

        public static void Less(ulong arg1, ulong arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), null, null);
        }

        public static void Less(decimal arg1, decimal arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), message, null);
        }

        public static void Less(double arg1, double arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), message, null);
        }

        public static void Less(IComparable arg1, IComparable arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), message, null);
        }

        public static void Less(int arg1, int arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), message, null);
        }

        public static void Less(long arg1, long arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), message, null);
        }

        public static void Less(float arg1, float arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), message, null);
        }

        public static void Less(uint arg1, uint arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), message, null);
        }

        public static void Less(ulong arg1, ulong arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), message, null);
        }

        public static void Less(decimal arg1, decimal arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), message, args);
        }

        public static void Less(double arg1, double arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), message, args);
        }

        public static void Less(IComparable arg1, IComparable arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), message, args);
        }

        public static void Less(int arg1, int arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), message, args);
        }

        public static void Less(long arg1, long arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), message, args);
        }

        public static void Less(float arg1, float arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), message, args);
        }

        public static void Less(uint arg1, uint arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), message, args);
        }

        public static void Less(ulong arg1, ulong arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThan(arg2), message, args);
        }

        public static void LessOrEqual(decimal arg1, decimal arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), null, null);
        }

        public static void LessOrEqual(double arg1, double arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), null, null);
        }

        public static void LessOrEqual(IComparable arg1, IComparable arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), null, null);
        }

        public static void LessOrEqual(int arg1, int arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), null, null);
        }

        public static void LessOrEqual(long arg1, long arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), null, null);
        }

        public static void LessOrEqual(float arg1, float arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), null, null);
        }

        public static void LessOrEqual(uint arg1, uint arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), null, null);
        }

        public static void LessOrEqual(ulong arg1, ulong arg2)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), null, null);
        }

        public static void LessOrEqual(decimal arg1, decimal arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), message, null);
        }

        public static void LessOrEqual(double arg1, double arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), message, null);
        }

        public static void LessOrEqual(IComparable arg1, IComparable arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), message, null);
        }

        public static void LessOrEqual(int arg1, int arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), message, null);
        }

        public static void LessOrEqual(long arg1, long arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), message, null);
        }

        public static void LessOrEqual(float arg1, float arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), message, null);
        }

        public static void LessOrEqual(uint arg1, uint arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), message, null);
        }

        public static void LessOrEqual(ulong arg1, ulong arg2, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), message, null);
        }

        public static void LessOrEqual(decimal arg1, decimal arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), message, args);
        }

        public static void LessOrEqual(double arg1, double arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), message, args);
        }

        public static void LessOrEqual(IComparable arg1, IComparable arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), message, args);
        }

        public static void LessOrEqual(int arg1, int arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), message, args);
        }

        public static void LessOrEqual(long arg1, long arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), message, args);
        }

        public static void LessOrEqual(float arg1, float arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), message, args);
        }

        public static void LessOrEqual(uint arg1, uint arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), message, args);
        }

        public static void LessOrEqual(ulong arg1, ulong arg2, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(arg1, Is.LessThanOrEqualTo(arg2), message, args);
        }

        public static void NotNull(object anObject)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(anObject, Is.Not.Null, null, null);
        }

        public static void NotNull(object anObject, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(anObject, Is.Not.Null, message, null);
        }

        public static void NotNull(object anObject, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(anObject, Is.Not.Null, message, args);
        }

        public static void Null(object anObject)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(anObject, Is.Null, null, null);
        }

        public static void Null(object anObject, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(anObject, Is.Null, message, null);
        }

        public static void Null(object anObject, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(anObject, Is.Null, message, args);
        }

        public static void Pass()
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            Pass(string.Empty, null);
        }

        public static void Pass(string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            Pass(message, null);
        }

        public static void Pass(string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            if (message == null)
            {
                message = string.Empty;
            }
            else if ((args != null) && (args.Length > 0))
            {
                message = string.Format(message, args);
            }
            throw new SuccessException(message);
        }

        public static void That(bool condition)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(condition, Is.True, null, null);
        }

        public static void That(ActualValueDelegate del, IResolveConstraint expr)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(del, expr.Resolve(), null, null);
        }

        public static void That<T>(ref T actual, IResolveConstraint expression)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That<T>(ref actual, expression.Resolve(), null, null);
        }

        public static void That(TestDelegate code, IResolveConstraint constraint)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(code, constraint);
        }

        public static void That(bool condition, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(condition, Is.True, message, null);
        }

        public static void That(object actual, IResolveConstraint expression)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, expression, null, null);
        }

        public static void That<T>(ref T actual, IResolveConstraint expression, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That<T>(ref actual, expression.Resolve(), message, null);
        }

        public static void That(ActualValueDelegate del, IResolveConstraint expr, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(del, expr.Resolve(), message, null);
        }

        public static void That(bool condition, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(condition, Is.True, message, args);
        }

        public static void That(object actual, IResolveConstraint expression, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(actual, expression, message, null);
        }

        public static void That(ActualValueDelegate del, IResolveConstraint expr, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;

            Constraint constraint = expr.Resolve();
            IncrementAssertCount();
            if (!constraint.Matches(del))
            {
                MessageWriter writer = new TextMessageWriter(message, args);
                constraint.WriteMessageTo(writer);
                throw new AssertionException(writer.ToString());
            }
        }

        public static void That(object actual, IResolveConstraint expression, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;

            Constraint constraint = expression.Resolve();
            IncrementAssertCount();
            if (!constraint.Matches(actual))
            {
                MessageWriter writer = new TextMessageWriter(message, args);
                constraint.WriteMessageTo(writer);
                throw new AssertionException(writer.ToString());
            }
        }

        public static void That<T>(ref T actual, IResolveConstraint expression, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;


            Constraint constraint = expression.Resolve();
            IncrementAssertCount();
            if (!constraint.Matches<T>(ref actual))
            {
                MessageWriter writer = new TextMessageWriter(message, args);
                constraint.WriteMessageTo(writer);
                throw new AssertionException(writer.ToString());
            }
        }

        public static T Throws<T>(TestDelegate code) where T : Exception
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return default(T);
            return Throws<T>(code, string.Empty, null);
        }

        public static Exception Throws(IResolveConstraint expression, TestDelegate code)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return null;
            return Throws(expression, code, string.Empty, null);
        }

        public static T Throws<T>(TestDelegate code, string message) where T : Exception
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return default(T);
            return Throws<T>(code, message, null);
        }

        public static Exception Throws(Type expectedExceptionType, TestDelegate code)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return null;
            return Throws(new ExceptionTypeConstraint(expectedExceptionType), code, string.Empty, null);
        }

        public static Exception Throws(IResolveConstraint expression, TestDelegate code, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return null;
            return Throws(expression, code, message, null);
        }

        public static T Throws<T>(TestDelegate code, string message, params object[] args) where T : Exception
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return default(T);
            return (T)Throws(typeof(T), code, message, args);
        }

        public static Exception Throws(Type expectedExceptionType, TestDelegate code, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return null;
            return Throws(new ExceptionTypeConstraint(expectedExceptionType), code, message, null);
        }

        public static Exception Throws(IResolveConstraint expression, TestDelegate code, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return null;
            Exception actual = null;
            try
            {
                code();
            }
            catch (Exception exception2)
            {
                actual = exception2;
            }
            That(actual, expression, message, args);
            return actual;
        }

        public static Exception Throws(Type expectedExceptionType, TestDelegate code, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return null;
            return Throws(new ExceptionTypeConstraint(expectedExceptionType), code, message, args);
        }

        public static void True(bool condition)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(condition, Is.True, null, null);
        }

        public static void True(bool condition, string message)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(condition, Is.True, message, null);
        }

        public static void True(bool condition, string message, params object[] args)
        {

            //非NUnit测试时，不执行
            if (!OyNUniting.IsUnitRuning)
                return;
            That(condition, Is.True, message, args);
        }
    }
}