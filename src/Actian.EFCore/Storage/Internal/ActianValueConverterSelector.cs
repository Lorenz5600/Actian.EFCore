using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Actian.EFCore.Storage.Internal
{
    public class ActianValueConverterSelector : ValueConverterSelector
    {
        private readonly ConcurrentDictionary<(Type ModelClrType, Type ProviderClrType), ValueConverterInfo> _converters
            = new ConcurrentDictionary<(Type, Type), ValueConverterInfo>();

        public ActianValueConverterSelector(
            [NotNull] ValueConverterSelectorDependencies dependencies
            ) : base(dependencies)
        {
        }

        public override IEnumerable<ValueConverterInfo> Select(Type modelClrType, Type providerClrType = null)
        {
            //if (modelClrType == typeof(DateTimeOffset) && providerClrType == typeof(DateTime))
            //{
            //    yield return _converters.GetOrAdd((modelClrType, providerClrType), new ValueConverterInfo(
            //        modelClrType,
            //        providerClrType,
            //        info => new ActianDateTimeOffsetConverter(info.MappingHints)
            //    ));
            //}
            foreach (var info in base.Select(modelClrType, providerClrType))
            {
                yield return info;
            }
        }
    }
}
