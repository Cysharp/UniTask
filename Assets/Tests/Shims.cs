using NUnit.Framework;
using System;

namespace Xunit
{
    public class FactAttribute : TestAttribute
    {

    }
}

// Shims of FluentAssertions
namespace FluentAssertions
{
    public static class FluentAssertionsExtensions
    {
        public static Int Should(this int value)
        {
            return new Int(value);
        }

        public static Bool Should(this bool value)
        {
            return new Bool(value);
        }

        public static ExceptionAssertion Should(this Exception value)
        {
            return new ExceptionAssertion(value);
        }

        public static Generic<T> Should<T>(this T value)
        {
            return new Generic<T>(value);
        }

        public class Generic<T>
        {
            readonly T actual;

            public Generic(T value)
            {
                actual = value;
            }

            public void Be(T expected)
            {
                Assert.AreEqual(expected, actual);
            }

            public void NotBe(T expected)
            {
                Assert.AreNotEqual(expected, actual);
            }

            public void BeNull()
            {
                Assert.IsNull(actual);
            }

            public void NotBeNull()
            {
                Assert.IsNotNull(actual);
            }
        }

        public class Bool
        {
            readonly bool actual;

            public Bool(bool value)
            {
                actual = value;
            }

            public void Be(bool expected)
            {
                Assert.AreEqual(expected, actual);
            }

            public void NotBe(bool expected)
            {
                Assert.AreNotEqual(expected, actual);
            }

            public void BeTrue()
            {
                Assert.AreEqual(true, actual);
            }

            public void BeFalse()
            {
                Assert.AreEqual(false, actual);
            }
        }

        public class Int
        {
            readonly int actual;

            public Int(int value)
            {
                actual = value;
            }

            public void Be(int expected)
            {
                Assert.AreEqual(expected, actual);
            }

            public void NotBe(int expected)
            {
                Assert.AreNotEqual(expected, actual);
            }

            public void BeCloseTo(int expected, int delta)
            {
                if (expected - delta <= actual && actual <= expected + delta)
                {
                    // OK.
                }
                else
                {
                    Assert.Fail($"Fail BeCloseTo, actual {actual} but expected:{expected} +- {delta}");
                }
            }
        }

        public class ExceptionAssertion
        {
            readonly Exception actual;

            public ExceptionAssertion(Exception actual)
            {
                this.actual = actual;
            }

            public void BeAssignableTo<T>()
            {
                typeof(T).IsAssignableFrom(actual.GetType());
            }
        }
    }
}