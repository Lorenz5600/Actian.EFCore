namespace Actian.EFCore.Metadata.Internal
{
    // TODO: Which annotations are needed?
    public static class ActianAnnotationNames
    {
        public const string Prefix = "Actian:";
        //public const string Clustered = Prefix + "Clustered";
        //public const string Include = Prefix + "Include";
        //public const string CreatedOnline = Prefix + "Online";
        public const string ValueGenerationStrategy = Prefix + "ValueGenerationStrategy";
        public const string HiLoSequenceName = Prefix + "HiLoSequenceName";
        public const string HiLoSequenceSchema = Prefix + "HiLoSequenceSchema";
        //public const string MemoryOptimized = Prefix + "MemoryOptimized";
        public const string Identity = Prefix + "Identity";
        public const string IdentitySeed = Prefix + "IdentitySeed";
        public const string IdentityIncrement = Prefix + "IdentityIncrement";
        //public const string EditionOptions = Prefix + "EditionOptions";
        //public const string MaxDatabaseSize = Prefix + "DatabaseMaxSize";
        //public const string ServiceTierSql = Prefix + "ServiceTierSql";
        //public const string PerformanceLevelSql = Prefix + "PerformanceLevelSql";
        public const string Locations = Prefix + "Location";
        public const string Journaling = Prefix + "Journaling";
        public const string Duplicates = Prefix + "Duplicates";
        public const string Persistence = Prefix + "Persistence";

        public const string DbNameCase = Prefix + "DbNameCase";
        public const string DbDelimitedCase = Prefix + "DbDelimitedCase";
    }
}
