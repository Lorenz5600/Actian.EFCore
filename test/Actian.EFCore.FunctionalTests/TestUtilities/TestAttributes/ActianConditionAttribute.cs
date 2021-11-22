using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;

namespace Actian.EFCore.TestUtilities
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class ActianConditionAttribute : Attribute, ITestCondition
    {
        public IEnumerable<ActianCondition> Conditions { get; set; }

        public ActianConditionAttribute(params ActianCondition[] conditions)
        {
            Conditions = conditions;
        }

        public ValueTask<bool> IsMetAsync()
        {
            var (isMet, _) = IsMetInternal();
            return new ValueTask<bool>(isMet);
        }

        public string SkipReason
        {
            get
            {
                var (_, reason) = IsMetInternal();
                return reason;
            }
        }

        private (bool isMet, string reason) IsMetInternal()
        {
            foreach (var condition in Conditions)
            {
                switch (condition)
                {
                    case ActianCondition.Todo:
                        return (false, "Todo");
                }
            }

            return (true, "");
        }
    }
}
