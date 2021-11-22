using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query;

namespace Actian.EFCore.Query.Internal
{
    public class ActianMethodCallTranslatorProvider : RelationalMethodCallTranslatorProvider
    {
        public ActianMethodCallTranslatorProvider([NotNull] RelationalMethodCallTranslatorProviderDependencies dependencies)
            : base(dependencies)
        {
            var sqlExpressionFactory = dependencies.SqlExpressionFactory;

            AddTranslators(new IMethodCallTranslator[]
            {
                new ActianConvertTranslator(sqlExpressionFactory),
                //new ActianDateTimeMethodTranslator(sqlExpressionFactory),
                //new ActianDateDiffFunctionsTranslator(sqlExpressionFactory),
                //new ActianFullTextSearchFunctionsTranslator(sqlExpressionFactory),
                //new ActianIsDateFunctionTranslator(sqlExpressionFactory),
                //new ActianMathTranslator(sqlExpressionFactory),
                //new ActianNewGuidTranslator(sqlExpressionFactory),
                //new ActianObjectToStringTranslator(sqlExpressionFactory),
                new ActianStringMethodTranslator(sqlExpressionFactory)
            });
        }
    }
}
