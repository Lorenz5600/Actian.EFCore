﻿using Microsoft.EntityFrameworkCore.Diagnostics;
#nullable enable
namespace Actian.EFCore.Diagnostics.Internal
{
    /// <summary>
    /// This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    /// the same compatibility standards as public APIs. It may be changed or removed without notice in
    /// any release. You should only use it directly in your code with extreme caution and knowing that
    /// doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public class ActianLoggingDefinitions : RelationalLoggingDefinitions
    {
        /// <summary>
        /// This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        /// the same compatibility standards as public APIs. It may be changed or removed without notice in
        /// any release. You should only use it directly in your code with extreme caution and knowing that
        /// doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogDefaultDecimalTypeColumn;

        /// <summary>
        /// This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        /// the same compatibility standards as public APIs. It may be changed or removed without notice in
        /// any release. You should only use it directly in your code with extreme caution and knowing that
        /// doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogByteIdentityColumn;

        /// <summary>
        /// This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        /// the same compatibility standards as public APIs. It may be changed or removed without notice in
        /// any release. You should only use it directly in your code with extreme caution and knowing that
        /// doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogFoundDefaultSchema;

        /// <summary>
        /// This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        /// the same compatibility standards as public APIs. It may be changed or removed without notice in
        /// any release. You should only use it directly in your code with extreme caution and knowing that
        /// doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogFoundTypeAlias;

        /// <summary>
        /// This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        /// the same compatibility standards as public APIs. It may be changed or removed without notice in
        /// any release. You should only use it directly in your code with extreme caution and knowing that
        /// doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogFoundColumn;

        /// <summary>
        /// This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        /// the same compatibility standards as public APIs. It may be changed or removed without notice in
        /// any release. You should only use it directly in your code with extreme caution and knowing that
        /// doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogFoundForeignKey;

        /// <summary>
        /// This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        /// the same compatibility standards as public APIs. It may be changed or removed without notice in
        /// any release. You should only use it directly in your code with extreme caution and knowing that
        /// doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogPrincipalTableNotInSelectionSet;

        /// <summary>
        /// This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        /// the same compatibility standards as public APIs. It may be changed or removed without notice in
        /// any release. You should only use it directly in your code with extreme caution and knowing that
        /// doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogMissingSchema;

        /// <summary>
        /// This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        /// the same compatibility standards as public APIs. It may be changed or removed without notice in
        /// any release. You should only use it directly in your code with extreme caution and knowing that
        /// doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogMissingTable;

        /// <summary>
        /// This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        /// the same compatibility standards as public APIs. It may be changed or removed without notice in
        /// any release. You should only use it directly in your code with extreme caution and knowing that
        /// doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogFoundSequence;

        /// <summary>
        /// This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        /// the same compatibility standards as public APIs. It may be changed or removed without notice in
        /// any release. You should only use it directly in your code with extreme caution and knowing that
        /// doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogFoundTable;

        /// <summary>
        /// This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        /// the same compatibility standards as public APIs. It may be changed or removed without notice in
        /// any release. You should only use it directly in your code with extreme caution and knowing that
        /// doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogFoundIndex;

        /// <summary>
        /// This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        /// the same compatibility standards as public APIs. It may be changed or removed without notice in
        /// any release. You should only use it directly in your code with extreme caution and knowing that
        /// doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogFoundPrimaryKey;

        /// <summary>
        /// This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        /// the same compatibility standards as public APIs. It may be changed or removed without notice in
        /// any release. You should only use it directly in your code with extreme caution and knowing that
        /// doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogFoundUniqueConstraint;

        /// <summary>
        /// This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        /// the same compatibility standards as public APIs. It may be changed or removed without notice in
        /// any release. You should only use it directly in your code with extreme caution and knowing that
        /// doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogPrincipalColumnNotFound;

        /// <summary>
        /// This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        /// the same compatibility standards as public APIs. It may be changed or removed without notice in
        /// any release. You should only use it directly in your code with extreme caution and knowing that
        /// doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogReflexiveConstraintIgnored;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogPrincipalTableInformationNotFound;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogColumnWithoutType;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogDuplicateForeignKeyConstraintIgnored;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public EventDefinitionBase? LogMissingViewDefinitionRights;
    }
}
