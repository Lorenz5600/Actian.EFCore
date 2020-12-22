using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Ingres.Client;
using JetBrains.Annotations;

namespace Actian.EFCore.Scaffolding.Internal
{
    public static class DbDataReaderExtension
    {
        public static T GetValueOrDefault<T>([NotNull] this DbDataReader reader, [NotNull] string name, ActianCasing casing)
        {
            return reader.GetValueOrDefault<T>(reader.GetOrdinal(casing.Normalize(name)));
        }

        public static T GetValueOrDefault<T>([NotNull] this DbDataReader reader, int idx)
        {
            return reader.IsDBNull(idx)
                ? default
                : reader.GetFieldValue<T>(idx);
        }

        public static T GetValueOrDefault<T>([NotNull] this DbDataRecord record, [NotNull] string name, ActianCasing casing)
        {
            return record.GetValueOrDefault<T>(record.GetOrdinal(casing.Normalize(name)));
        }

        public static T GetValueOrDefault<T>([NotNull] this DbDataRecord record, int idx)
        {
            return record.IsDBNull(idx)
                ? default
                : (T)record.GetValue(idx);
        }

        public static string GetTrimmedChar([NotNull] this DbDataReader reader, [NotNull] string name, ActianCasing casing)
        {
            return reader.GetTrimmedChar(reader.GetOrdinal(casing.Normalize(name)));
        }

        public static string GetTrimmedChar([NotNull] this DbDataReader reader, int idx)
        {
            return reader.IsDBNull(idx)
                ? default
                : reader.GetFieldValue<string>(idx).TrimEnd(' ');
        }

        public static T ExecuteReader<T>([NotNull] this IngresConnection connection, string sql, [NotNull] Func<DbDataReader, T> get)
        {
            using var command = new IngresCommand(sql, connection);
            using var reader = command.ExecuteReader();
            return get(reader);
        }

        public static IEnumerable<T> Select<T>([NotNull] this IngresConnection connection, string sql, [NotNull] Func<DbDataReader, T> get)
        {
            using var command = new IngresCommand(sql, connection);
            using var reader = command.ExecuteReader();
            return reader.Select(get).ToList();
        }

        public static IEnumerable<T> Select<T>([NotNull] this DbDataReader reader, [NotNull] Func<DbDataReader, T> get)
        {
            while (reader.Read())
            {
                yield return get(reader);
            }
        }
    }
}
