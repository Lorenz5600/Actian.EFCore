using System.Collections.Generic;
using System.Linq;

namespace Actian.TestLoggers
{
    public static class ActianTestOutcomeExtensions
    {
        public static string ToHtml(this ActianTestOutcome outcome) => outcome switch
        {
            ActianTestOutcome.Passed => "✔️&nbsp;Passed",
            ActianTestOutcome.Skipped => "⚠️&nbsp;Skipped",
            ActianTestOutcome.Failed => "❌&nbsp;Failed",
            ActianTestOutcome.Todo => "⚠️&nbsp;Todo",
            _ => ""
        };

        public static ActianTestOutcome Outcome(this IEnumerable<ActianTestOutcome> outcomes)
        {
            if (!outcomes.Any())
                return ActianTestOutcome.Passed;

            if (outcomes.Any(outcome => outcome == ActianTestOutcome.Failed))
                return ActianTestOutcome.Failed;

            if (outcomes.All(outcome => outcome == ActianTestOutcome.Skipped))
                return ActianTestOutcome.Skipped;

            if (outcomes.Any(outcome => outcome == ActianTestOutcome.Todo))
                return ActianTestOutcome.Todo;

            return ActianTestOutcome.Passed;
        }
    }
}
