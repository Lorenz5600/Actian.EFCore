using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class ActianComplianceTest : RelationalComplianceTestBase
    {
        public ActianComplianceTest(ITestOutputHelper testOutputHelper)
        {
            TestEnvironment.Log(this, testOutputHelper);
        }

        protected override Assembly TargetAssembly { get; } = typeof(ActianComplianceTest).Assembly;
        private bool OriginalImplementation => true;

        public override void All_test_bases_must_be_implemented()
        {
            if (OriginalImplementation)
            {
                base.All_test_bases_must_be_implemented();
                return;
            }

            var allConcreteTests = (
                from c in TargetAssembly.GetTypes()
                where c.BaseType != typeof(object)
                   && !c.IsAbstract
                   && (c.IsPublic || c.IsNestedPublic)
                select c
            ).OrderBy(c => c.FullName).ToList();

            var implemented = new List<ImplementedTest>();
            var notImplemented = new List<Type>();

            foreach (var baseType in GetBaseTestClasses().OrderBy(t => t.FullName))
            {
                if (IgnoredTestBases.Contains(baseType))
                    continue;

                var concreteTests = allConcreteTests
                    .Where(c => Implements(c, baseType))
                    .ToList();

                if (concreteTests.Any())
                {
                    implemented.Add(new ImplementedTest(baseType, concreteTests));
                }
                else
                {
                    notImplemented.Add(baseType);
                }
            }

            if (notImplemented.Any())
            {
                var text = new StringBuilder();
                text.AppendLine($"{implemented.Count()} test bases have been implemented:");
                text.AppendLine("");
                foreach (var t in implemented)
                {
                    text.AppendLine($"✓ {t.BaseType.PrettyName(true)}");
                    foreach (var c in t.ConcreteTests)
                    {
                        text.AppendLine($"    {c.PrettyName(true)}");
                    }
                }
                text.AppendLine("");
                text.AppendLine($"{notImplemented.Count()} test bases have not been implemented:");
                text.AppendLine("");
                foreach (var t in notImplemented)
                {
                    text.AppendLine($"✗ {t.PrettyName(true)}");
                }
                Assert.True(false, text.ToString());
            }
        }

        private class ImplementedTest
        {
            public ImplementedTest(Type baseType, IEnumerable<Type> concreteTests)
            {
                BaseType = baseType;
                ConcreteTests = concreteTests;
            }

            public Type BaseType { get; }
            public IEnumerable<Type> ConcreteTests { get; }
        }

        private static bool Implements(Type type, Type interfaceOrBaseType)
            => (type.IsPublic || type.IsNestedPublic) && interfaceOrBaseType.IsGenericTypeDefinition
                ? GetGenericTypeImplementations(type, interfaceOrBaseType).Any()
                : interfaceOrBaseType.IsAssignableFrom(type);

        private static IEnumerable<Type> GetGenericTypeImplementations(Type type, Type interfaceOrBaseType)
        {
            var typeInfo = type.GetTypeInfo();
            if (!typeInfo.IsGenericTypeDefinition)
            {
                var baseTypes = interfaceOrBaseType.GetTypeInfo().IsInterface
                    ? typeInfo.ImplementedInterfaces
                    : GetBaseTypes(type);
                foreach (var baseType in baseTypes)
                {
                    if (baseType.GetTypeInfo().IsGenericType
                        && baseType.GetGenericTypeDefinition() == interfaceOrBaseType)
                    {
                        yield return baseType;
                    }
                }

                if (type.GetTypeInfo().IsGenericType
                    && type.GetGenericTypeDefinition() == interfaceOrBaseType)
                {
                    yield return type;
                }
            }
        }

        private static IEnumerable<Type> GetBaseTypes(Type type)
        {
            type = type.GetTypeInfo().BaseType;

            while (type != null)
            {
                yield return type;

                type = type.GetTypeInfo().BaseType;
            }
        }
    }
}
