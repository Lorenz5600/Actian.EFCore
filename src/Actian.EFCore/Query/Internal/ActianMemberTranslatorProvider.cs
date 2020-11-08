using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query;

// TODO: ActianMemberTranslatorProvider
namespace Actian.EFCore.Query.Internal
{
    public class ActianMemberTranslatorProvider : RelationalMemberTranslatorProvider
    {
        public ActianMemberTranslatorProvider([NotNull] RelationalMemberTranslatorProviderDependencies dependencies)
            : base(dependencies)
        {
            var sqlExpressionFactory = dependencies.SqlExpressionFactory;

            AddTranslators(
                new IMemberTranslator[]
                {
                    //new ActianDateTimeMemberTranslator(sqlExpressionFactory),
                    //new ActianStringMemberTranslator(sqlExpressionFactory)
                });
        }
    }
}
