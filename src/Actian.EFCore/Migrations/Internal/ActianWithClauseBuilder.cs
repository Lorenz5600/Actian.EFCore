using System;
using System.Collections.Generic;
using System.Linq;
using Actian.EFCore.Utilities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Actian.EFCore.Migrations.Internal
{
    /// <summary>
    /// </summary>
    public class ActianWithClauseBuilder
    {
        private readonly MigrationCommandListBuilder _builder;
        private readonly List<string> _options = new List<string>();

        public ActianWithClauseBuilder([NotNull] MigrationCommandListBuilder builder)
        {
            _builder = Check.NotNull(builder, nameof(builder));
        }

        private ActianWithClauseBuilder With(string option)
        {
            if (!string.IsNullOrWhiteSpace(option))
                _options.Add(option);
            return this;
        }

        private ActianWithClauseBuilder With(Func<string> getOption, bool? condition)
            => condition == true ? With(getOption()) : this;

        public MigrationCommandListBuilder Build()
        {
            var first = true;
            foreach (var option in _options)
            {
                if (first)
                {
                    _builder.AppendLine();
                    _builder.Append("WITH ");
                }
                else
                {
                    _builder.AppendLine(",");
                    _builder.Append("     ");
                }
                _builder.Append(option);
                first = false;
            }
            return _builder;
        }

        public ActianWithClauseBuilder Locations(IEnumerable<string> locations)
            => With(() => $"LOCATION = ({string.Join(", ", locations)})", locations?.Any());

        public ActianWithClauseBuilder Journaling(bool? journalingEnabled)
            => With(() => journalingEnabled.Value ? "JOURNALING" : "NOJOURNALING", journalingEnabled != null);

        public ActianWithClauseBuilder Duplicates(bool? duplicates)
            => With(() => duplicates.Value ? "DUPLICATES" : "NODUPLICATES", duplicates != null);

        public ActianWithClauseBuilder Persistence(bool? persistence)
            => With(() => persistence.Value ? "PERSISTENCE" : "NOPERSISTENCE", persistence != null);
    }
}
