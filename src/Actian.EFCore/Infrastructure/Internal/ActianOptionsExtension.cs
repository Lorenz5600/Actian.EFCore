using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Actian.EFCore.Infrastructure.Internal
{
    public class ActianOptionsExtension : RelationalOptionsExtension
    {
        private DbContextOptionsExtensionInfo _info;

        public ActianOptionsExtension()
        {
        }

        // NB: When adding new options, make sure to update the copy ctor below.

        protected ActianOptionsExtension([NotNull] ActianOptionsExtension copyFrom)
            : base(copyFrom)
        {
        }

        /// <inheritdoc />
        public override DbContextOptionsExtensionInfo Info
            => _info ??= new ExtensionInfo(this);

        /// <inheritdoc />
        protected override RelationalOptionsExtension Clone()
            => new ActianOptionsExtension(this);

        /// <inheritdoc />
        public override void ApplyServices(IServiceCollection services)
            => services.AddEntityFrameworkActian();

        private sealed class ExtensionInfo : RelationalExtensionInfo
        {
            private long? _serviceProviderHash;
            private string _logFragment;

            public ExtensionInfo(IDbContextOptionsExtension extension)
                : base(extension)
            {
            }

            private new ActianOptionsExtension Extension
                => (ActianOptionsExtension)base.Extension;

            public override bool IsDatabaseProvider => true;

            public override string LogFragment
            {
                get
                {
                    if (_logFragment == null)
                    {
                        var builder = new StringBuilder();

                        builder.Append(base.LogFragment);

                        _logFragment = builder.ToString();
                    }

                    return _logFragment;
                }
            }

            public override long GetServiceProviderHashCode()
            {
                if (_serviceProviderHash == null)
                {
                    _serviceProviderHash = base.GetServiceProviderHashCode();
                }

                return _serviceProviderHash.Value;
            }

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            {
                //debugInfo[$"Actian:{nameof(ActianDbContextOptionsBuilder.UseRowNumberForPaging)}"]
                //    = (Extension._rowNumberPaging?.GetHashCode() ?? 0L).ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
