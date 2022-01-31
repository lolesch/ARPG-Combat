using System;
using NUnit.Framework;
using TeppichsTools.Runtime.Reflection;
using TeppichsTools.Tests.TestObjects;

namespace TeppichsTools.Editor.Tests
{
    public class ReflectionTests
    {
        [Test]
        public void GetSubtypesFromSameAssembly()
        {
            Type[] expected = {typeof(MockA), typeof(MockB), typeof(MockC)};

            Type[] real = TypeReflectionUtility.GetSubtypesFromSameAssembly(typeof(MockAbstract));

            Assert.AreEqual(expected, real);
        }

        [Test]
        public void GetSubtypesFromAllAssembliesButActuallyJustSame()
        {
            Type[] expected = {typeof(MockA), typeof(MockB), typeof(MockC)};
            Type[] real     = TypeReflectionUtility.GetSubtypesFromAllAssemblies(typeof(MockAbstract));

            Assert.AreEqual(expected, real);
        }

        [Test]
        public void GetSubtypesFromAllAssemblies()
        {
            Type[] expected = {typeof(Mock3), typeof(Mock1), typeof(Mock2)};
            Type[] real     = TypeReflectionUtility.GetSubtypesFromAllAssemblies(typeof(MockSuperType));

            Assert.AreEqual(expected, real);
        }

        public abstract class MockAbstract : IComparable<MockAbstract>
        {
            public int CompareTo(MockAbstract other) =>
                string.Compare(GetType().Name, other.GetType().Name, StringComparison.CurrentCulture);
        }

        private class MockA : MockAbstract { }

        private class MockB : MockAbstract { }

        private class MockC : MockA { }

        private class Mock3 : MockSuperType { }
    }
}