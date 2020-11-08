using Actian.EFCore.Metadata.Internal;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// Extension methods for <see cref="IEntityType" /> for Actian-specific metadata.
    /// </summary>
    public static class ActianIndexExtensions
    {
        #region Persistent

        /// <summary>
        /// Returns a value indicating whether the index is persistent.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns><c>true</c> if the index is persistent</returns>
        public static bool IsPersistent([NotNull] this IIndex index)
            => index[ActianAnnotationNames.Persistence] as bool? ?? false;

        /// <summary>
        /// Sets whether the index is persistent.
        /// </summary>
        /// <param name="index">The index</param>
        /// <param name="isPersistent">A value indicating whether the index is persistent</param>
        public static void SetPersistence([NotNull] this IMutableIndex index, bool isPersistent)
            => index.SetOrRemoveAnnotation(ActianAnnotationNames.Persistence, isPersistent);

        /// <summary>
        /// Sets whether the index is persistent.
        /// </summary>
        /// <param name="index">The index</param>
        /// <param name="isPersistent">A value indicating whether the index is persistent</param>
        /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
        public static void SetPersistence([NotNull] this IConventionIndex index, bool isPersistent, bool fromDataAnnotation = false)
            => index.SetOrRemoveAnnotation(ActianAnnotationNames.Persistence, isPersistent, fromDataAnnotation);

        /// <summary>
        /// Gets the configuration source for the persistence setting.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The configuration source for the persistence setting</returns>
        public static ConfigurationSource? GetPersistenceConfigurationSource([NotNull] this IConventionIndex index)
            => index.FindAnnotation(ActianAnnotationNames.Persistence)?.GetConfigurationSource();

        #endregion
    }
}
