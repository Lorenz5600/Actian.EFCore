using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Xunit.Abstractions;
using static Actian.EFCore.TestUtilities.ActianSkipReasons;

namespace Actian.EFCore.Query
{
    public class ComplexNavigationsWeakQueryActianTest : ComplexNavigationsWeakQueryTestBase<ComplexNavigationsWeakQueryActianFixture>
    {
        public ComplexNavigationsWeakQueryActianTest(ComplexNavigationsWeakQueryActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        public override Task Entity_equality_empty(bool isAsync)
        {
            return base.Entity_equality_empty(isAsync);
        }

        public override Task Key_equality_when_sentinel_ef_property(bool isAsync)
        {
            return base.Key_equality_when_sentinel_ef_property(isAsync);
        }

        public override Task Key_equality_using_property_method_required(bool isAsync)
        {
            return base.Key_equality_using_property_method_required(isAsync);
        }

        public override Task Key_equality_using_property_method_required2(bool isAsync)
        {
            return base.Key_equality_using_property_method_required2(isAsync);
        }

        public override Task Key_equality_using_property_method_nested(bool isAsync)
        {
            return base.Key_equality_using_property_method_nested(isAsync);
        }

        public override Task Key_equality_using_property_method_nested2(bool isAsync)
        {
            return base.Key_equality_using_property_method_nested2(isAsync);
        }

        public override Task Key_equality_using_property_method_and_member_expression1(bool isAsync)
        {
            return base.Key_equality_using_property_method_and_member_expression1(isAsync);
        }

        public override Task Key_equality_using_property_method_and_member_expression2(bool isAsync)
        {
            return base.Key_equality_using_property_method_and_member_expression2(isAsync);
        }

        public override Task Key_equality_using_property_method_and_member_expression3(bool isAsync)
        {
            return base.Key_equality_using_property_method_and_member_expression3(isAsync);
        }

        public override Task Key_equality_navigation_converted_to_FK(bool isAsync)
        {
            return base.Key_equality_navigation_converted_to_FK(isAsync);
        }

        public override Task Key_equality_two_conditions_on_same_navigation(bool isAsync)
        {
            return base.Key_equality_two_conditions_on_same_navigation(isAsync);
        }

        public override Task Key_equality_two_conditions_on_same_navigation2(bool isAsync)
        {
            return base.Key_equality_two_conditions_on_same_navigation2(isAsync);
        }

        public override Task Multi_level_include_one_to_many_optional_and_one_to_many_optional_produces_valid_sql(bool isAsync)
        {
            return base.Multi_level_include_one_to_many_optional_and_one_to_many_optional_produces_valid_sql(isAsync);
        }

        public override Task Multi_level_include_correct_PK_is_chosen_as_the_join_predicate_for_queries_that_join_same_table_multiple_times(
            bool isAsync)
        {
            return base.Multi_level_include_correct_PK_is_chosen_as_the_join_predicate_for_queries_that_join_same_table_multiple_times(isAsync);
        }

        public override void Multi_level_include_with_short_circuiting()
        {
            base.Multi_level_include_with_short_circuiting();
        }

        public override Task Join_navigation_key_access_optional(bool isAsync)
        {
            return base.Join_navigation_key_access_optional(isAsync);
        }

        public override Task Join_navigation_key_access_required(bool isAsync)
        {
            return base.Join_navigation_key_access_required(isAsync);
        }

        public override Task Navigation_key_access_optional_comparison(bool isAsync)
        {
            return base.Navigation_key_access_optional_comparison(isAsync);
        }

        public override async Task Simple_level1_include(bool isAsync)
        {
            await base.Simple_level1_include(isAsync);
            AssertSql(@"
                SELECT ""l"".""Id"", ""l"".""Date"", ""l"".""Name"", ""t"".""Id"", ""t"".""OneToOne_Required_PK_Date"", ""t"".""Level1_Optional_Id"", ""t"".""Level1_Required_Id"", ""t"".""Level2_Name"", ""t"".""OneToMany_Optional_Inverse2Id"", ""t"".""OneToMany_Required_Inverse2Id"", ""t"".""OneToOne_Optional_PK_Inverse2Id""
                FROM ""Level1"" AS ""l""
                LEFT JOIN (
                    SELECT ""l0"".""Id"", ""l0"".""OneToOne_Required_PK_Date"", ""l0"".""Level1_Optional_Id"", ""l0"".""Level1_Required_Id"", ""l0"".""Level2_Name"", ""l0"".""OneToMany_Optional_Inverse2Id"", ""l0"".""OneToMany_Required_Inverse2Id"", ""l0"".""OneToOne_Optional_PK_Inverse2Id"", ""l1"".""Id"" AS ""Id0""
                    FROM ""Level1"" AS ""l0""
                    INNER JOIN ""Level1"" AS ""l1"" ON ""l0"".""Id"" = ""l1"".""Id""
                    WHERE ""l0"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l0"".""Level1_Required_Id"" IS NOT NULL AND ""l0"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                ) AS ""t"" ON ""l"".""Id"" = ""t"".""Id""
            ");
        }

        public override async Task Simple_level1(bool isAsync)
        {
            await base.Simple_level1(isAsync);
            AssertSql(@"
                SELECT ""l"".""Id"", ""l"".""Date"", ""l"".""Name""
                FROM ""Level1"" AS ""l""
            ");
        }

        public override async Task Simple_level1_level2_include(bool isAsync)
        {
            await base.Simple_level1_level2_include(isAsync);
            AssertSql(@"
                SELECT ""l"".""Id"", ""l"".""Date"", ""l"".""Name"", ""t"".""Id"", ""t"".""OneToOne_Required_PK_Date"", ""t"".""Level1_Optional_Id"", ""t"".""Level1_Required_Id"", ""t"".""Level2_Name"", ""t"".""OneToMany_Optional_Inverse2Id"", ""t"".""OneToMany_Required_Inverse2Id"", ""t"".""OneToOne_Optional_PK_Inverse2Id"", ""t1"".""Id"", ""t1"".""Level2_Optional_Id"", ""t1"".""Level2_Required_Id"", ""t1"".""Level3_Name"", ""t1"".""OneToMany_Optional_Inverse3Id"", ""t1"".""OneToMany_Required_Inverse3Id"", ""t1"".""OneToOne_Optional_PK_Inverse3Id""
                FROM ""Level1"" AS ""l""
                LEFT JOIN (
                    SELECT ""l0"".""Id"", ""l0"".""OneToOne_Required_PK_Date"", ""l0"".""Level1_Optional_Id"", ""l0"".""Level1_Required_Id"", ""l0"".""Level2_Name"", ""l0"".""OneToMany_Optional_Inverse2Id"", ""l0"".""OneToMany_Required_Inverse2Id"", ""l0"".""OneToOne_Optional_PK_Inverse2Id"", ""l1"".""Id"" AS ""Id0""
                    FROM ""Level1"" AS ""l0""
                    INNER JOIN ""Level1"" AS ""l1"" ON ""l0"".""Id"" = ""l1"".""Id""
                    WHERE ""l0"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l0"".""Level1_Required_Id"" IS NOT NULL AND ""l0"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                ) AS ""t"" ON ""l"".""Id"" = ""t"".""Id""
                LEFT JOIN (
                    SELECT ""l2"".""Id"", ""l2"".""Level2_Optional_Id"", ""l2"".""Level2_Required_Id"", ""l2"".""Level3_Name"", ""l2"".""OneToMany_Optional_Inverse3Id"", ""l2"".""OneToMany_Required_Inverse3Id"", ""l2"".""OneToOne_Optional_PK_Inverse3Id"", ""t0"".""Id"" AS ""Id0"", ""t0"".""Id0"" AS ""Id00""
                    FROM ""Level1"" AS ""l2""
                    INNER JOIN (
                        SELECT ""l3"".""Id"", ""l3"".""OneToOne_Required_PK_Date"", ""l3"".""Level1_Optional_Id"", ""l3"".""Level1_Required_Id"", ""l3"".""Level2_Name"", ""l3"".""OneToMany_Optional_Inverse2Id"", ""l3"".""OneToMany_Required_Inverse2Id"", ""l3"".""OneToOne_Optional_PK_Inverse2Id"", ""l4"".""Id"" AS ""Id0""
                        FROM ""Level1"" AS ""l3""
                        INNER JOIN ""Level1"" AS ""l4"" ON ""l3"".""Id"" = ""l4"".""Id""
                        WHERE ""l3"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l3"".""Level1_Required_Id"" IS NOT NULL AND ""l3"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                    ) AS ""t0"" ON ""l2"".""Id"" = ""t0"".""Id""
                    WHERE ""l2"".""OneToMany_Required_Inverse3Id"" IS NOT NULL AND ""l2"".""Level2_Required_Id"" IS NOT NULL
                ) AS ""t1"" ON ""t"".""Id"" = ""t1"".""Id""
            ");
        }

        public override async Task Simple_level1_level2_GroupBy_Count(bool isAsync)
        {
            await base.Simple_level1_level2_GroupBy_Count(isAsync);
            AssertSql(@"
                SELECT COUNT(*)
                FROM ""Level1"" AS ""l""
                LEFT JOIN (
                    SELECT ""l0"".""Id"", ""l0"".""OneToOne_Required_PK_Date"", ""l0"".""Level1_Optional_Id"", ""l0"".""Level1_Required_Id"", ""l0"".""Level2_Name"", ""l0"".""OneToMany_Optional_Inverse2Id"", ""l0"".""OneToMany_Required_Inverse2Id"", ""l0"".""OneToOne_Optional_PK_Inverse2Id"", ""l1"".""Id"" AS ""Id0""
                    FROM ""Level1"" AS ""l0""
                    INNER JOIN ""Level1"" AS ""l1"" ON ""l0"".""Id"" = ""l1"".""Id""
                    WHERE ""l0"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l0"".""Level1_Required_Id"" IS NOT NULL AND ""l0"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                ) AS ""t"" ON ""l"".""Id"" = ""t"".""Id""
                LEFT JOIN (
                    SELECT ""l2"".""Id"", ""l2"".""Level2_Optional_Id"", ""l2"".""Level2_Required_Id"", ""l2"".""Level3_Name"", ""l2"".""OneToMany_Optional_Inverse3Id"", ""l2"".""OneToMany_Required_Inverse3Id"", ""l2"".""OneToOne_Optional_PK_Inverse3Id"", ""t0"".""Id"" AS ""Id0"", ""t0"".""Id0"" AS ""Id00""
                    FROM ""Level1"" AS ""l2""
                    INNER JOIN (
                        SELECT ""l3"".""Id"", ""l3"".""OneToOne_Required_PK_Date"", ""l3"".""Level1_Optional_Id"", ""l3"".""Level1_Required_Id"", ""l3"".""Level2_Name"", ""l3"".""OneToMany_Optional_Inverse2Id"", ""l3"".""OneToMany_Required_Inverse2Id"", ""l3"".""OneToOne_Optional_PK_Inverse2Id"", ""l4"".""Id"" AS ""Id0""
                        FROM ""Level1"" AS ""l3""
                        INNER JOIN ""Level1"" AS ""l4"" ON ""l3"".""Id"" = ""l4"".""Id""
                        WHERE ""l3"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l3"".""Level1_Required_Id"" IS NOT NULL AND ""l3"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                    ) AS ""t0"" ON ""l2"".""Id"" = ""t0"".""Id""
                    WHERE ""l2"".""OneToMany_Required_Inverse3Id"" IS NOT NULL AND ""l2"".""Level2_Required_Id"" IS NOT NULL
                ) AS ""t1"" ON ""t"".""Id"" = ""t1"".""Id""
                GROUP BY ""t1"".""Level3_Name""
            ");
        }

        public override async Task Simple_level1_level2_GroupBy_Having_Count(bool isAsync)
        {
            await base.Simple_level1_level2_GroupBy_Having_Count(isAsync);
            AssertSql(@"
                SELECT COUNT(*)
                FROM ""Level1"" AS ""l""
                LEFT JOIN (
                    SELECT ""l0"".""Id"", ""l0"".""OneToOne_Required_PK_Date"", ""l0"".""Level1_Optional_Id"", ""l0"".""Level1_Required_Id"", ""l0"".""Level2_Name"", ""l0"".""OneToMany_Optional_Inverse2Id"", ""l0"".""OneToMany_Required_Inverse2Id"", ""l0"".""OneToOne_Optional_PK_Inverse2Id"", ""l1"".""Id"" AS ""Id0""
                    FROM ""Level1"" AS ""l0""
                    INNER JOIN ""Level1"" AS ""l1"" ON ""l0"".""Id"" = ""l1"".""Id""
                    WHERE ""l0"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l0"".""Level1_Required_Id"" IS NOT NULL AND ""l0"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                ) AS ""t"" ON ""l"".""Id"" = ""t"".""Id""
                LEFT JOIN (
                    SELECT ""l2"".""Id"", ""l2"".""Level2_Optional_Id"", ""l2"".""Level2_Required_Id"", ""l2"".""Level3_Name"", ""l2"".""OneToMany_Optional_Inverse3Id"", ""l2"".""OneToMany_Required_Inverse3Id"", ""l2"".""OneToOne_Optional_PK_Inverse3Id"", ""t0"".""Id"" AS ""Id0"", ""t0"".""Id0"" AS ""Id00""
                    FROM ""Level1"" AS ""l2""
                    INNER JOIN (
                        SELECT ""l3"".""Id"", ""l3"".""OneToOne_Required_PK_Date"", ""l3"".""Level1_Optional_Id"", ""l3"".""Level1_Required_Id"", ""l3"".""Level2_Name"", ""l3"".""OneToMany_Optional_Inverse2Id"", ""l3"".""OneToMany_Required_Inverse2Id"", ""l3"".""OneToOne_Optional_PK_Inverse2Id"", ""l4"".""Id"" AS ""Id0""
                        FROM ""Level1"" AS ""l3""
                        INNER JOIN ""Level1"" AS ""l4"" ON ""l3"".""Id"" = ""l4"".""Id""
                        WHERE ""l3"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l3"".""Level1_Required_Id"" IS NOT NULL AND ""l3"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                    ) AS ""t0"" ON ""l2"".""Id"" = ""t0"".""Id""
                    WHERE ""l2"".""OneToMany_Required_Inverse3Id"" IS NOT NULL AND ""l2"".""Level2_Required_Id"" IS NOT NULL
                ) AS ""t1"" ON ""t"".""Id"" = ""t1"".""Id""
                GROUP BY ""t1"".""Level3_Name""
                HAVING MIN(COALESCE(""t"".""Id"", 0)) > 0
            ");
        }

        public override async Task Simple_level1_level2_level3_include(bool isAsync)
        {
            await base.Simple_level1_level2_level3_include(isAsync);
            AssertSql(@"
                SELECT ""l"".""Id"", ""l"".""Date"", ""l"".""Name"", ""t"".""Id"", ""t"".""OneToOne_Required_PK_Date"", ""t"".""Level1_Optional_Id"", ""t"".""Level1_Required_Id"", ""t"".""Level2_Name"", ""t"".""OneToMany_Optional_Inverse2Id"", ""t"".""OneToMany_Required_Inverse2Id"", ""t"".""OneToOne_Optional_PK_Inverse2Id"", ""t1"".""Id"", ""t1"".""Level2_Optional_Id"", ""t1"".""Level2_Required_Id"", ""t1"".""Level3_Name"", ""t1"".""OneToMany_Optional_Inverse3Id"", ""t1"".""OneToMany_Required_Inverse3Id"", ""t1"".""OneToOne_Optional_PK_Inverse3Id"", ""t4"".""Id"", ""t4"".""Level3_Optional_Id"", ""t4"".""Level3_Required_Id"", ""t4"".""Level4_Name"", ""t4"".""OneToMany_Optional_Inverse4Id"", ""t4"".""OneToMany_Required_Inverse4Id"", ""t4"".""OneToOne_Optional_PK_Inverse4Id""
                FROM ""Level1"" AS ""l""
                LEFT JOIN (
                    SELECT ""l0"".""Id"", ""l0"".""OneToOne_Required_PK_Date"", ""l0"".""Level1_Optional_Id"", ""l0"".""Level1_Required_Id"", ""l0"".""Level2_Name"", ""l0"".""OneToMany_Optional_Inverse2Id"", ""l0"".""OneToMany_Required_Inverse2Id"", ""l0"".""OneToOne_Optional_PK_Inverse2Id"", ""l1"".""Id"" AS ""Id0""
                    FROM ""Level1"" AS ""l0""
                    INNER JOIN ""Level1"" AS ""l1"" ON ""l0"".""Id"" = ""l1"".""Id""
                    WHERE ""l0"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l0"".""Level1_Required_Id"" IS NOT NULL AND ""l0"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                ) AS ""t"" ON ""l"".""Id"" = ""t"".""Id""
                LEFT JOIN (
                    SELECT ""l2"".""Id"", ""l2"".""Level2_Optional_Id"", ""l2"".""Level2_Required_Id"", ""l2"".""Level3_Name"", ""l2"".""OneToMany_Optional_Inverse3Id"", ""l2"".""OneToMany_Required_Inverse3Id"", ""l2"".""OneToOne_Optional_PK_Inverse3Id"", ""t0"".""Id"" AS ""Id0"", ""t0"".""Id0"" AS ""Id00""
                    FROM ""Level1"" AS ""l2""
                    INNER JOIN (
                        SELECT ""l3"".""Id"", ""l3"".""OneToOne_Required_PK_Date"", ""l3"".""Level1_Optional_Id"", ""l3"".""Level1_Required_Id"", ""l3"".""Level2_Name"", ""l3"".""OneToMany_Optional_Inverse2Id"", ""l3"".""OneToMany_Required_Inverse2Id"", ""l3"".""OneToOne_Optional_PK_Inverse2Id"", ""l4"".""Id"" AS ""Id0""
                        FROM ""Level1"" AS ""l3""
                        INNER JOIN ""Level1"" AS ""l4"" ON ""l3"".""Id"" = ""l4"".""Id""
                        WHERE ""l3"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l3"".""Level1_Required_Id"" IS NOT NULL AND ""l3"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                    ) AS ""t0"" ON ""l2"".""Id"" = ""t0"".""Id""
                    WHERE ""l2"".""OneToMany_Required_Inverse3Id"" IS NOT NULL AND ""l2"".""Level2_Required_Id"" IS NOT NULL
                ) AS ""t1"" ON ""t"".""Id"" = ""t1"".""Id""
                LEFT JOIN (
                    SELECT ""l5"".""Id"", ""l5"".""Level3_Optional_Id"", ""l5"".""Level3_Required_Id"", ""l5"".""Level4_Name"", ""l5"".""OneToMany_Optional_Inverse4Id"", ""l5"".""OneToMany_Required_Inverse4Id"", ""l5"".""OneToOne_Optional_PK_Inverse4Id"", ""t3"".""Id"" AS ""Id0"", ""t3"".""Id0"" AS ""Id00"", ""t3"".""Id00"" AS ""Id000""
                    FROM ""Level1"" AS ""l5""
                    INNER JOIN (
                        SELECT ""l6"".""Id"", ""l6"".""Level2_Optional_Id"", ""l6"".""Level2_Required_Id"", ""l6"".""Level3_Name"", ""l6"".""OneToMany_Optional_Inverse3Id"", ""l6"".""OneToMany_Required_Inverse3Id"", ""l6"".""OneToOne_Optional_PK_Inverse3Id"", ""t2"".""Id"" AS ""Id0"", ""t2"".""Id0"" AS ""Id00""
                        FROM ""Level1"" AS ""l6""
                        INNER JOIN (
                            SELECT ""l7"".""Id"", ""l7"".""OneToOne_Required_PK_Date"", ""l7"".""Level1_Optional_Id"", ""l7"".""Level1_Required_Id"", ""l7"".""Level2_Name"", ""l7"".""OneToMany_Optional_Inverse2Id"", ""l7"".""OneToMany_Required_Inverse2Id"", ""l7"".""OneToOne_Optional_PK_Inverse2Id"", ""l8"".""Id"" AS ""Id0""
                            FROM ""Level1"" AS ""l7""
                            INNER JOIN ""Level1"" AS ""l8"" ON ""l7"".""Id"" = ""l8"".""Id""
                            WHERE ""l7"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l7"".""Level1_Required_Id"" IS NOT NULL AND ""l7"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                        ) AS ""t2"" ON ""l6"".""Id"" = ""t2"".""Id""
                        WHERE ""l6"".""OneToMany_Required_Inverse3Id"" IS NOT NULL AND ""l6"".""Level2_Required_Id"" IS NOT NULL
                    ) AS ""t3"" ON ""l5"".""Id"" = ""t3"".""Id""
                    WHERE ""l5"".""OneToMany_Required_Inverse4Id"" IS NOT NULL AND ""l5"".""Level3_Required_Id"" IS NOT NULL
                ) AS ""t4"" ON ""t1"".""Id"" = ""t4"".""Id""
            ");
        }

        public override Task Navigation_key_access_required_comparison(bool isAsync)
        {
            return base.Navigation_key_access_required_comparison(isAsync);
        }

        public override Task Navigation_inside_method_call_translated_to_join(bool isAsync)
        {
            return base.Navigation_inside_method_call_translated_to_join(isAsync);
        }

        public override Task Navigation_inside_method_call_translated_to_join2(bool isAsync)
        {
            return base.Navigation_inside_method_call_translated_to_join2(isAsync);
        }

        public override Task Optional_navigation_inside_method_call_translated_to_join(bool isAsync)
        {
            return base.Optional_navigation_inside_method_call_translated_to_join(isAsync);
        }

        public override Task Optional_navigation_inside_property_method_translated_to_join(bool isAsync)
        {
            return base.Optional_navigation_inside_property_method_translated_to_join(isAsync);
        }

        public override Task Optional_navigation_inside_nested_method_call_translated_to_join(bool isAsync)
        {
            return base.Optional_navigation_inside_nested_method_call_translated_to_join(isAsync);
        }

        public override Task Method_call_on_optional_navigation_translates_to_null_conditional_properly_for_arguments(bool isAsync)
        {
            return base.Method_call_on_optional_navigation_translates_to_null_conditional_properly_for_arguments(isAsync);
        }

        [ActianTodo]
        public override Task Optional_navigation_inside_method_call_translated_to_join_keeps_original_nullability(bool isAsync)
        {
            return base.Optional_navigation_inside_method_call_translated_to_join_keeps_original_nullability(isAsync);
        }

        [ActianTodo]
        public override Task Optional_navigation_inside_nested_method_call_translated_to_join_keeps_original_nullability(bool isAsync)
        {
            return base.Optional_navigation_inside_nested_method_call_translated_to_join_keeps_original_nullability(isAsync);
        }

        [ActianTodo]
        public override Task Optional_navigation_inside_nested_method_call_translated_to_join_keeps_original_nullability_also_for_arguments(
            bool isAsync)
        {
            return base.Optional_navigation_inside_nested_method_call_translated_to_join_keeps_original_nullability_also_for_arguments(isAsync);
        }

        public override Task Join_navigation_in_outer_selector_translated_to_extra_join(bool isAsync)
        {
            return base.Join_navigation_in_outer_selector_translated_to_extra_join(isAsync);
        }

        public override Task Join_navigation_in_outer_selector_translated_to_extra_join_nested(bool isAsync)
        {
            return base.Join_navigation_in_outer_selector_translated_to_extra_join_nested(isAsync);
        }

        public override Task Join_navigation_in_outer_selector_translated_to_extra_join_nested2(bool isAsync)
        {
            return base.Join_navigation_in_outer_selector_translated_to_extra_join_nested2(isAsync);
        }

        public override Task Join_navigation_in_inner_selector(bool isAsync)
        {
            return base.Join_navigation_in_inner_selector(isAsync);
        }

        public override Task Join_navigations_in_inner_selector_translated_without_collision(bool isAsync)
        {
            return base.Join_navigations_in_inner_selector_translated_without_collision(isAsync);
        }

        public override Task Join_navigation_non_key_join(bool isAsync)
        {
            return base.Join_navigation_non_key_join(isAsync);
        }

        public override Task Join_with_orderby_on_inner_sequence_navigation_non_key_join(bool isAsync)
        {
            return base.Join_with_orderby_on_inner_sequence_navigation_non_key_join(isAsync);
        }

        public override Task Join_navigation_self_ref(bool isAsync)
        {
            return base.Join_navigation_self_ref(isAsync);
        }

        public override Task Join_navigation_nested(bool isAsync)
        {
            return base.Join_navigation_nested(isAsync);
        }

        public override Task Join_navigation_nested2(bool isAsync)
        {
            return base.Join_navigation_nested2(isAsync);
        }

        public override Task Join_navigation_deeply_nested_non_key_join(bool isAsync)
        {
            return base.Join_navigation_deeply_nested_non_key_join(isAsync);
        }

        public override Task Join_navigation_deeply_nested_required(bool isAsync)
        {
            return base.Join_navigation_deeply_nested_required(isAsync);
        }

        public override Task Multiple_complex_includes(bool isAsync)
        {
            return base.Multiple_complex_includes(isAsync);
        }

        public override Task Multiple_complex_includes_self_ref(bool isAsync)
        {
            return base.Multiple_complex_includes_self_ref(isAsync);
        }

        public override Task Multiple_complex_include_select(bool isAsync)
        {
            return base.Multiple_complex_include_select(isAsync);
        }

        public override Task Select_nav_prop_collection_one_to_many_required(bool isAsync)
        {
            return base.Select_nav_prop_collection_one_to_many_required(isAsync);
        }

        public override Task Select_nav_prop_reference_optional1(bool isAsync)
        {
            return base.Select_nav_prop_reference_optional1(isAsync);
        }

        public override Task Select_nav_prop_reference_optional1_via_DefaultIfEmpty(bool isAsync)
        {
            return base.Select_nav_prop_reference_optional1_via_DefaultIfEmpty(isAsync);
        }

        public override Task Select_nav_prop_reference_optional2(bool isAsync)
        {
            return base.Select_nav_prop_reference_optional2(isAsync);
        }

        public override Task Select_nav_prop_reference_optional2_via_DefaultIfEmpty(bool isAsync)
        {
            return base.Select_nav_prop_reference_optional2_via_DefaultIfEmpty(isAsync);
        }

        public override Task Select_nav_prop_reference_optional3(bool isAsync)
        {
            return base.Select_nav_prop_reference_optional3(isAsync);
        }

        public override Task Where_nav_prop_reference_optional1(bool isAsync)
        {
            return base.Where_nav_prop_reference_optional1(isAsync);
        }

        public override Task Where_nav_prop_reference_optional1_via_DefaultIfEmpty(bool isAsync)
        {
            return base.Where_nav_prop_reference_optional1_via_DefaultIfEmpty(isAsync);
        }

        public override Task Where_nav_prop_reference_optional2(bool isAsync)
        {
            return base.Where_nav_prop_reference_optional2(isAsync);
        }

        public override Task Where_nav_prop_reference_optional2_via_DefaultIfEmpty(bool isAsync)
        {
            return base.Where_nav_prop_reference_optional2_via_DefaultIfEmpty(isAsync);
        }

        public override Task Select_multiple_nav_prop_reference_optional(bool isAsync)
        {
            return base.Select_multiple_nav_prop_reference_optional(isAsync);
        }

        public override Task Where_multiple_nav_prop_reference_optional_member_compared_to_value(bool isAsync)
        {
            return base.Where_multiple_nav_prop_reference_optional_member_compared_to_value(isAsync);
        }

        public override Task Where_multiple_nav_prop_reference_optional_member_compared_to_null(bool isAsync)
        {
            return base.Where_multiple_nav_prop_reference_optional_member_compared_to_null(isAsync);
        }

        public override Task Where_multiple_nav_prop_reference_optional_compared_to_null1(bool isAsync)
        {
            return base.Where_multiple_nav_prop_reference_optional_compared_to_null1(isAsync);
        }

        public override Task Where_multiple_nav_prop_reference_optional_compared_to_null2(bool isAsync)
        {
            return base.Where_multiple_nav_prop_reference_optional_compared_to_null2(isAsync);
        }

        public override Task Where_multiple_nav_prop_reference_optional_compared_to_null3(bool isAsync)
        {
            return base.Where_multiple_nav_prop_reference_optional_compared_to_null3(isAsync);
        }

        public override Task Where_multiple_nav_prop_reference_optional_compared_to_null4(bool isAsync)
        {
            return base.Where_multiple_nav_prop_reference_optional_compared_to_null4(isAsync);
        }

        public override Task Where_multiple_nav_prop_reference_optional_compared_to_null5(bool isAsync)
        {
            return base.Where_multiple_nav_prop_reference_optional_compared_to_null5(isAsync);
        }

        public override Task Select_multiple_nav_prop_reference_required(bool isAsync)
        {
            return base.Select_multiple_nav_prop_reference_required(isAsync);
        }

        public override Task Select_multiple_nav_prop_reference_required2(bool isAsync)
        {
            return base.Select_multiple_nav_prop_reference_required2(isAsync);
        }

        public override Task Select_multiple_nav_prop_optional_required(bool isAsync)
        {
            return base.Select_multiple_nav_prop_optional_required(isAsync);
        }

        public override Task Where_multiple_nav_prop_optional_required(bool isAsync)
        {
            return base.Where_multiple_nav_prop_optional_required(isAsync);
        }

        public override Task SelectMany_navigation_comparison1(bool isAsync)
        {
            return base.SelectMany_navigation_comparison1(isAsync);
        }

        public override Task SelectMany_navigation_comparison2(bool isAsync)
        {
            return base.SelectMany_navigation_comparison2(isAsync);
        }

        public override Task SelectMany_navigation_comparison3(bool isAsync)
        {
            return base.SelectMany_navigation_comparison3(isAsync);
        }

        public override Task Where_complex_predicate_with_with_nav_prop_and_OrElse1(bool isAsync)
        {
            return base.Where_complex_predicate_with_with_nav_prop_and_OrElse1(isAsync);
        }

        public override Task Where_complex_predicate_with_with_nav_prop_and_OrElse2(bool isAsync)
        {
            return base.Where_complex_predicate_with_with_nav_prop_and_OrElse2(isAsync);
        }

        public override Task Where_complex_predicate_with_with_nav_prop_and_OrElse3(bool isAsync)
        {
            return base.Where_complex_predicate_with_with_nav_prop_and_OrElse3(isAsync);
        }

        public override Task Where_complex_predicate_with_with_nav_prop_and_OrElse4(bool isAsync)
        {
            return base.Where_complex_predicate_with_with_nav_prop_and_OrElse4(isAsync);
        }

        [ActianTodo]
        public override Task Complex_navigations_with_predicate_projected_into_anonymous_type(bool isAsync)
        {
            return base.Complex_navigations_with_predicate_projected_into_anonymous_type(isAsync);
        }

        public override Task Complex_navigations_with_predicate_projected_into_anonymous_type2(bool isAsync)
        {
            return base.Complex_navigations_with_predicate_projected_into_anonymous_type2(isAsync);
        }

        public override Task Optional_navigation_projected_into_DTO(bool isAsync)
        {
            return base.Optional_navigation_projected_into_DTO(isAsync);
        }

        [ActianTodo]
        public override Task OrderBy_nav_prop_reference_optional(bool isAsync)
        {
            return base.OrderBy_nav_prop_reference_optional(isAsync);
        }

        [ActianTodo]
        public override Task OrderBy_nav_prop_reference_optional_via_DefaultIfEmpty(bool isAsync)
        {
            return base.OrderBy_nav_prop_reference_optional_via_DefaultIfEmpty(isAsync);
        }

        public override Task Result_operator_nav_prop_reference_optional_Sum(bool isAsync)
        {
            return base.Result_operator_nav_prop_reference_optional_Sum(isAsync);
        }

        public override Task Result_operator_nav_prop_reference_optional_Min(bool isAsync)
        {
            return base.Result_operator_nav_prop_reference_optional_Min(isAsync);
        }

        public override Task Result_operator_nav_prop_reference_optional_Max(bool isAsync)
        {
            return base.Result_operator_nav_prop_reference_optional_Max(isAsync);
        }

        public override Task Result_operator_nav_prop_reference_optional_Average(bool isAsync)
        {
            return base.Result_operator_nav_prop_reference_optional_Average(isAsync);
        }

        public override Task Result_operator_nav_prop_reference_optional_Average_with_identity_selector(bool isAsync)
        {
            return base.Result_operator_nav_prop_reference_optional_Average_with_identity_selector(isAsync);
        }

        public override Task Result_operator_nav_prop_reference_optional_Average_without_selector(bool isAsync)
        {
            return base.Result_operator_nav_prop_reference_optional_Average_without_selector(isAsync);
        }

        public override async Task Result_operator_nav_prop_reference_optional_via_DefaultIfEmpty(bool isAsync)
        {
            await base.Result_operator_nav_prop_reference_optional_via_DefaultIfEmpty(isAsync);
            AssertSql(@"
                SELECT SUM(CASE
                    WHEN ""t1"".""Id"" IS NULL THEN 0
                    ELSE ""t1"".""Level1_Required_Id""
                END)
                FROM ""Level1"" AS ""l""
                LEFT JOIN (
                    SELECT ""l0"".""Id"", ""l0"".""Date"", ""l0"".""Name"", ""t"".""Id"" AS ""Id0"", ""t"".""OneToOne_Required_PK_Date"", ""t"".""Level1_Optional_Id"", ""t"".""Level1_Required_Id"", ""t"".""Level2_Name"", ""t"".""OneToMany_Optional_Inverse2Id"", ""t"".""OneToMany_Required_Inverse2Id"", ""t"".""OneToOne_Optional_PK_Inverse2Id""
                    FROM ""Level1"" AS ""l0""
                    LEFT JOIN (
                        SELECT ""l1"".""Id"", ""l1"".""OneToOne_Required_PK_Date"", ""l1"".""Level1_Optional_Id"", ""l1"".""Level1_Required_Id"", ""l1"".""Level2_Name"", ""l1"".""OneToMany_Optional_Inverse2Id"", ""l1"".""OneToMany_Required_Inverse2Id"", ""l1"".""OneToOne_Optional_PK_Inverse2Id"", ""l2"".""Id"" AS ""Id0""
                        FROM ""Level1"" AS ""l1""
                        INNER JOIN ""Level1"" AS ""l2"" ON ""l1"".""Id"" = ""l2"".""Id""
                        WHERE ""l1"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l1"".""Level1_Required_Id"" IS NOT NULL AND ""l1"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                    ) AS ""t"" ON ""l0"".""Id"" = ""t"".""Id""
                    WHERE ""t"".""Id"" IS NOT NULL
                ) AS ""t0"" ON ""l"".""Id"" = ""t0"".""Level1_Optional_Id""
                LEFT JOIN (
                    SELECT ""l3"".""Id"", ""l3"".""OneToOne_Required_PK_Date"", ""l3"".""Level1_Optional_Id"", ""l3"".""Level1_Required_Id"", ""l3"".""Level2_Name"", ""l3"".""OneToMany_Optional_Inverse2Id"", ""l3"".""OneToMany_Required_Inverse2Id"", ""l3"".""OneToOne_Optional_PK_Inverse2Id"", ""l4"".""Id"" AS ""Id0""
                    FROM ""Level1"" AS ""l3""
                    INNER JOIN ""Level1"" AS ""l4"" ON ""l3"".""Id"" = ""l4"".""Id""
                    WHERE ""l3"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l3"".""Level1_Required_Id"" IS NOT NULL AND ""l3"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                ) AS ""t1"" ON ""t0"".""Id"" = ""t1"".""Id""
            ");
        }

        public override Task Include_with_optional_navigation(bool isAsync)
        {
            return base.Include_with_optional_navigation(isAsync);
        }

        public override Task Include_nested_with_optional_navigation(bool isAsync)
        {
            return base.Include_nested_with_optional_navigation(isAsync);
        }

        public override Task Include_with_groupjoin_skip_and_take(bool isAsync)
        {
            return base.Include_with_groupjoin_skip_and_take(isAsync);
        }

        public override Task Join_flattening_bug_4539(bool isAsync)
        {
            return base.Join_flattening_bug_4539(isAsync);
        }

        [ActianTodo]
        public override Task Query_source_materialization_bug_4547(bool isAsync)
        {
            return base.Query_source_materialization_bug_4547(isAsync);
        }

        public override Task SelectMany_navigation_property(bool isAsync)
        {
            return base.SelectMany_navigation_property(isAsync);
        }

        public override Task SelectMany_navigation_property_and_projection(bool isAsync)
        {
            return base.SelectMany_navigation_property_and_projection(isAsync);
        }

        public override Task SelectMany_navigation_property_and_filter_before(bool isAsync)
        {
            return base.SelectMany_navigation_property_and_filter_before(isAsync);
        }

        public override Task SelectMany_navigation_property_and_filter_after(bool isAsync)
        {
            return base.SelectMany_navigation_property_and_filter_after(isAsync);
        }

        public override Task SelectMany_nested_navigation_property_required(bool isAsync)
        {
            return base.SelectMany_nested_navigation_property_required(isAsync);
        }

        public override Task SelectMany_nested_navigation_property_optional_and_projection(bool isAsync)
        {
            return base.SelectMany_nested_navigation_property_optional_and_projection(isAsync);
        }

        public override Task Multiple_SelectMany_calls(bool isAsync)
        {
            return base.Multiple_SelectMany_calls(isAsync);
        }

        public override Task SelectMany_navigation_property_with_another_navigation_in_subquery(bool isAsync)
        {
            return base.SelectMany_navigation_property_with_another_navigation_in_subquery(isAsync);
        }

        public override Task Where_navigation_property_to_collection(bool isAsync)
        {
            return base.Where_navigation_property_to_collection(isAsync);
        }

        public override Task Where_navigation_property_to_collection2(bool isAsync)
        {
            return base.Where_navigation_property_to_collection2(isAsync);
        }

        public override Task Where_navigation_property_to_collection_of_original_entity_type(bool isAsync)
        {
            return base.Where_navigation_property_to_collection_of_original_entity_type(isAsync);
        }

        [ActianTodo]
        public override Task Complex_multi_include_with_order_by_and_paging(bool isAsync)
        {
            return base.Complex_multi_include_with_order_by_and_paging(isAsync);
        }

        [ActianTodo]
        public override Task Complex_multi_include_with_order_by_and_paging_joins_on_correct_key(bool isAsync)
        {
            return base.Complex_multi_include_with_order_by_and_paging_joins_on_correct_key(isAsync);
        }

        [ActianTodo]
        public override Task Complex_multi_include_with_order_by_and_paging_joins_on_correct_key2(bool isAsync)
        {
            return base.Complex_multi_include_with_order_by_and_paging_joins_on_correct_key2(isAsync);
        }

        public override Task Multiple_include_with_multiple_optional_navigations(bool isAsync)
        {
            return base.Multiple_include_with_multiple_optional_navigations(isAsync);
        }

        public override Task Correlated_subquery_doesnt_project_unnecessary_columns_in_top_level(bool isAsync)
        {
            return base.Correlated_subquery_doesnt_project_unnecessary_columns_in_top_level(isAsync);
        }

        public override Task Correlated_subquery_doesnt_project_unnecessary_columns_in_top_level_join(bool isAsync)
        {
            return base.Correlated_subquery_doesnt_project_unnecessary_columns_in_top_level_join(isAsync);
        }

        public override Task Correlated_nested_subquery_doesnt_project_unnecessary_columns_in_top_level(bool isAsync)
        {
            return base.Correlated_nested_subquery_doesnt_project_unnecessary_columns_in_top_level(isAsync);
        }

        public override Task Correlated_nested_two_levels_up_subquery_doesnt_project_unnecessary_columns_in_top_level(bool isAsync)
        {
            return base.Correlated_nested_two_levels_up_subquery_doesnt_project_unnecessary_columns_in_top_level(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_on_subquery_and_set_operation_on_grouping_but_nothing_from_grouping_is_projected(bool isAsync)
        {
            return base.GroupJoin_on_subquery_and_set_operation_on_grouping_but_nothing_from_grouping_is_projected(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_on_complex_subquery_and_set_operation_on_grouping_but_nothing_from_grouping_is_projected(bool isAsync)
        {
            return base.GroupJoin_on_complex_subquery_and_set_operation_on_grouping_but_nothing_from_grouping_is_projected(isAsync);
        }

        [ActianTodo]
        public override Task Null_protection_logic_work_for_inner_key_access_of_manually_created_GroupJoin1(bool isAsync)
        {
            return base.Null_protection_logic_work_for_inner_key_access_of_manually_created_GroupJoin1(isAsync);
        }

        [ActianTodo]
        public override Task Null_protection_logic_work_for_inner_key_access_of_manually_created_GroupJoin2(bool isAsync)
        {
            return base.Null_protection_logic_work_for_inner_key_access_of_manually_created_GroupJoin2(isAsync);
        }

        [ActianTodo]
        public override Task Null_protection_logic_work_for_outer_key_access_of_manually_created_GroupJoin(bool isAsync)
        {
            return base.Null_protection_logic_work_for_outer_key_access_of_manually_created_GroupJoin(isAsync);
        }

        public override Task SelectMany_where_with_subquery(bool isAsync)
        {
            return base.SelectMany_where_with_subquery(isAsync);
        }

        public override Task Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access1(bool isAsync)
        {
            return base.Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access1(isAsync);
        }

        public override Task Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access2(bool isAsync)
        {
            return base.Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access2(isAsync);
        }

        public override Task Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access3(bool isAsync)
        {
            return base.Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access3(isAsync);
        }

        public override Task Order_by_key_of_navigation_similar_to_projected_gets_optimized_into_FK_access(bool isAsync)
        {
            return base.Order_by_key_of_navigation_similar_to_projected_gets_optimized_into_FK_access(isAsync);
        }

        [ActianTodo]
        public override Task Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access_subquery(bool isAsync)
        {
            return base.Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access_subquery(isAsync);
        }

        [ActianTodo]
        public override Task Order_by_key_of_anonymous_type_projected_navigation_doesnt_get_optimized_into_FK_access_subquery(bool isAsync)
        {
            return base.Order_by_key_of_anonymous_type_projected_navigation_doesnt_get_optimized_into_FK_access_subquery(isAsync);
        }

        [ActianTodo]
        public override Task Optional_navigation_take_optional_navigation(bool isAsync)
        {
            return base.Optional_navigation_take_optional_navigation(isAsync);
        }

        [ActianTodo]
        public override Task Projection_select_correct_table_from_subquery_when_materialization_is_not_required(bool isAsync)
        {
            return base.Projection_select_correct_table_from_subquery_when_materialization_is_not_required(isAsync);
        }

        [ActianTodo]
        public override Task Projection_select_correct_table_with_anonymous_projection_in_subquery(bool isAsync)
        {
            return base.Projection_select_correct_table_with_anonymous_projection_in_subquery(isAsync);
        }

        [ActianTodo]
        public override Task Projection_select_correct_table_in_subquery_when_materialization_is_not_required_in_multiple_joins(bool isAsync)
        {
            return base.Projection_select_correct_table_in_subquery_when_materialization_is_not_required_in_multiple_joins(isAsync);
        }

        [ActianTodo]
        public override Task Where_predicate_on_optional_reference_navigation(bool isAsync)
        {
            return base.Where_predicate_on_optional_reference_navigation(isAsync);
        }

        public override async Task SelectMany_with_Include1(bool isAsync)
        {
            await base.SelectMany_with_Include1(isAsync);
            AssertSql(@"
                SELECT ""t"".""Id"", ""t"".""OneToOne_Required_PK_Date"", ""t"".""Level1_Optional_Id"", ""t"".""Level1_Required_Id"", ""t"".""Level2_Name"", ""t"".""OneToMany_Optional_Inverse2Id"", ""t"".""OneToMany_Required_Inverse2Id"", ""t"".""OneToOne_Optional_PK_Inverse2Id"", ""l"".""Id"", ""t"".""Id0"", ""t1"".""Id"", ""t1"".""Level2_Optional_Id"", ""t1"".""Level2_Required_Id"", ""t1"".""Level3_Name"", ""t1"".""OneToMany_Optional_Inverse3Id"", ""t1"".""OneToMany_Required_Inverse3Id"", ""t1"".""OneToOne_Optional_PK_Inverse3Id"", ""t1"".""Id0"", ""t1"".""Id00""
                FROM ""Level1"" AS ""l""
                INNER JOIN (
                    SELECT ""l0"".""Id"", ""l0"".""OneToOne_Required_PK_Date"", ""l0"".""Level1_Optional_Id"", ""l0"".""Level1_Required_Id"", ""l0"".""Level2_Name"", ""l0"".""OneToMany_Optional_Inverse2Id"", ""l0"".""OneToMany_Required_Inverse2Id"", ""l0"".""OneToOne_Optional_PK_Inverse2Id"", ""l1"".""Id"" AS ""Id0""
                    FROM ""Level1"" AS ""l0""
                    INNER JOIN ""Level1"" AS ""l1"" ON ""l0"".""Id"" = ""l1"".""Id""
                    WHERE ""l0"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l0"".""Level1_Required_Id"" IS NOT NULL AND ""l0"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                ) AS ""t"" ON ""l"".""Id"" = ""t"".""OneToMany_Optional_Inverse2Id""
                LEFT JOIN (
                    SELECT ""l2"".""Id"", ""l2"".""Level2_Optional_Id"", ""l2"".""Level2_Required_Id"", ""l2"".""Level3_Name"", ""l2"".""OneToMany_Optional_Inverse3Id"", ""l2"".""OneToMany_Required_Inverse3Id"", ""l2"".""OneToOne_Optional_PK_Inverse3Id"", ""t0"".""Id"" AS ""Id0"", ""t0"".""Id0"" AS ""Id00""
                    FROM ""Level1"" AS ""l2""
                    INNER JOIN (
                        SELECT ""l3"".""Id"", ""l3"".""OneToOne_Required_PK_Date"", ""l3"".""Level1_Optional_Id"", ""l3"".""Level1_Required_Id"", ""l3"".""Level2_Name"", ""l3"".""OneToMany_Optional_Inverse2Id"", ""l3"".""OneToMany_Required_Inverse2Id"", ""l3"".""OneToOne_Optional_PK_Inverse2Id"", ""l4"".""Id"" AS ""Id0""
                        FROM ""Level1"" AS ""l3""
                        INNER JOIN ""Level1"" AS ""l4"" ON ""l3"".""Id"" = ""l4"".""Id""
                        WHERE ""l3"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l3"".""Level1_Required_Id"" IS NOT NULL AND ""l3"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                    ) AS ""t0"" ON ""l2"".""Id"" = ""t0"".""Id""
                    WHERE ""l2"".""OneToMany_Required_Inverse3Id"" IS NOT NULL AND ""l2"".""Level2_Required_Id"" IS NOT NULL
                ) AS ""t1"" ON ""t"".""Id"" = ""t1"".""OneToMany_Optional_Inverse3Id""
                ORDER BY ""l"".""Id"", ""t"".""Id"", ""t"".""Id0"", ""t1"".""Id"", ""t1"".""Id0"", ""t1"".""Id00""
            ");
        }

        public override Task Orderby_SelectMany_with_Include1(bool isAsync)
        {
            return base.Orderby_SelectMany_with_Include1(isAsync);
        }

        public override Task SelectMany_with_Include2(bool isAsync)
        {
            return base.SelectMany_with_Include2(isAsync);
        }

        public override Task SelectMany_with_Include_ThenInclude(bool isAsync)
        {
            return base.SelectMany_with_Include_ThenInclude(isAsync);
        }

        public override Task Multiple_SelectMany_with_Include(bool isAsync)
        {
            return base.Multiple_SelectMany_with_Include(isAsync);
        }

        public override Task SelectMany_with_string_based_Include1(bool isAsync)
        {
            return base.SelectMany_with_string_based_Include1(isAsync);
        }

        public override Task SelectMany_with_string_based_Include2(bool isAsync)
        {
            return base.SelectMany_with_string_based_Include2(isAsync);
        }

        public override Task Multiple_SelectMany_with_string_based_Include(bool isAsync)
        {
            return base.Multiple_SelectMany_with_string_based_Include(isAsync);
        }

        [ActianTodo]
        public override Task Required_navigation_with_Include(bool isAsync)
        {
            return base.Required_navigation_with_Include(isAsync);
        }

        [ActianTodo]
        public override Task Required_navigation_with_Include_ThenInclude(bool isAsync)
        {
            return base.Required_navigation_with_Include_ThenInclude(isAsync);
        }

        [ActianTodo]
        public override Task Multiple_required_navigations_with_Include(bool isAsync)
        {
            return base.Multiple_required_navigations_with_Include(isAsync);
        }

        [ActianTodo]
        public override Task Multiple_required_navigation_using_multiple_selects_with_Include(bool isAsync)
        {
            return base.Multiple_required_navigation_using_multiple_selects_with_Include(isAsync);
        }

        [ActianTodo]
        public override Task Multiple_required_navigation_with_string_based_Include(bool isAsync)
        {
            return base.Multiple_required_navigation_with_string_based_Include(isAsync);
        }

        [ActianTodo]
        public override Task Multiple_required_navigation_using_multiple_selects_with_string_based_Include(bool isAsync)
        {
            return base.Multiple_required_navigation_using_multiple_selects_with_string_based_Include(isAsync);
        }

        [ActianTodo]
        public override Task Optional_navigation_with_Include(bool isAsync)
        {
            return base.Optional_navigation_with_Include(isAsync);
        }

        [ActianTodo]
        public override Task Optional_navigation_with_Include_ThenInclude(bool isAsync)
        {
            return base.Optional_navigation_with_Include_ThenInclude(isAsync);
        }

        [ActianTodo]
        public override Task Multiple_optional_navigation_with_Include(bool isAsync)
        {
            return base.Multiple_optional_navigation_with_Include(isAsync);
        }

        [ActianTodo]
        public override Task Multiple_optional_navigation_with_string_based_Include(bool isAsync)
        {
            return base.Multiple_optional_navigation_with_string_based_Include(isAsync);
        }

        [ActianTodo]
        public override Task Optional_navigation_with_order_by_and_Include(bool isAsync)
        {
            return base.Optional_navigation_with_order_by_and_Include(isAsync);
        }

        [ActianTodo]
        public override Task Optional_navigation_with_Include_and_order(bool isAsync)
        {
            return base.Optional_navigation_with_Include_and_order(isAsync);
        }

        public override Task SelectMany_with_order_by_and_Include(bool isAsync)
        {
            return base.SelectMany_with_order_by_and_Include(isAsync);
        }

        public override Task SelectMany_with_Include_and_order_by(bool isAsync)
        {
            return base.SelectMany_with_Include_and_order_by(isAsync);
        }

        public override Task SelectMany_with_navigation_and_explicit_DefaultIfEmpty(bool isAsync)
        {
            return base.SelectMany_with_navigation_and_explicit_DefaultIfEmpty(isAsync);
        }

        public override Task SelectMany_with_navigation_and_Distinct(bool isAsync)
        {
            return base.SelectMany_with_navigation_and_Distinct(isAsync);
        }

        public override Task SelectMany_with_navigation_filter_and_explicit_DefaultIfEmpty(bool isAsync)
        {
            return base.SelectMany_with_navigation_filter_and_explicit_DefaultIfEmpty(isAsync);
        }

        public override Task SelectMany_with_nested_navigation_and_explicit_DefaultIfEmpty(bool isAsync)
        {
            return base.SelectMany_with_nested_navigation_and_explicit_DefaultIfEmpty(isAsync);
        }

        public override Task SelectMany_with_nested_navigation_filter_and_explicit_DefaultIfEmpty(bool isAsync)
        {
            return base.SelectMany_with_nested_navigation_filter_and_explicit_DefaultIfEmpty(isAsync);
        }

        public override Task SelectMany_with_nested_required_navigation_filter_and_explicit_DefaultIfEmpty(bool isAsync)
        {
            return base.SelectMany_with_nested_required_navigation_filter_and_explicit_DefaultIfEmpty(isAsync);
        }

        [ActianTodo]
        public override Task SelectMany_with_nested_navigations_and_additional_joins_outside_of_SelectMany(bool isAsync)
        {
            return base.SelectMany_with_nested_navigations_and_additional_joins_outside_of_SelectMany(isAsync);
        }

        [ActianTodo]
        public override Task SelectMany_with_nested_navigations_explicit_DefaultIfEmpty_and_additional_joins_outside_of_SelectMany(
            bool isAsync)
        {
            return base.SelectMany_with_nested_navigations_explicit_DefaultIfEmpty_and_additional_joins_outside_of_SelectMany(isAsync);
        }

        [ActianTodo]
        public override Task SelectMany_with_nested_navigations_explicit_DefaultIfEmpty_and_additional_joins_outside_of_SelectMany2(
            bool isAsync)
        {
            return base.SelectMany_with_nested_navigations_explicit_DefaultIfEmpty_and_additional_joins_outside_of_SelectMany2(isAsync);
        }

        public override Task SelectMany_with_nested_navigations_explicit_DefaultIfEmpty_and_additional_joins_outside_of_SelectMany3(
            bool isAsync)
        {
            return base.SelectMany_with_nested_navigations_explicit_DefaultIfEmpty_and_additional_joins_outside_of_SelectMany3(isAsync);
        }

        public override Task SelectMany_with_nested_navigations_explicit_DefaultIfEmpty_and_additional_joins_outside_of_SelectMany4(
            bool isAsync)
        {
            return base.SelectMany_with_nested_navigations_explicit_DefaultIfEmpty_and_additional_joins_outside_of_SelectMany4(isAsync);
        }

        [ActianTodo]
        public override Task Multiple_SelectMany_with_nested_navigations_and_explicit_DefaultIfEmpty_joined_together(bool isAsync)
        {
            return base.Multiple_SelectMany_with_nested_navigations_and_explicit_DefaultIfEmpty_joined_together(isAsync);
        }

        public override Task SelectMany_with_nested_navigations_and_explicit_DefaultIfEmpty_followed_by_Select_required_navigation_using_same_navs(
                bool isAsync)
        {
            return base.SelectMany_with_nested_navigations_and_explicit_DefaultIfEmpty_followed_by_Select_required_navigation_using_same_navs(isAsync);
        }

        public override Task SelectMany_with_nested_navigations_and_explicit_DefaultIfEmpty_followed_by_Select_required_navigation_using_different_navs(
                bool isAsync)
        {
            return base.SelectMany_with_nested_navigations_and_explicit_DefaultIfEmpty_followed_by_Select_required_navigation_using_different_navs(isAsync);
        }

        [ActianTodo]
        public override Task Complex_SelectMany_with_nested_navigations_and_explicit_DefaultIfEmpty_with_other_query_operators_composed_on_top(bool isAsync)
        {
            return base.Complex_SelectMany_with_nested_navigations_and_explicit_DefaultIfEmpty_with_other_query_operators_composed_on_top(isAsync);
        }

        public override Task Multiple_SelectMany_with_navigation_and_explicit_DefaultIfEmpty(bool isAsync)
        {
            return base.Multiple_SelectMany_with_navigation_and_explicit_DefaultIfEmpty(isAsync);
        }

        public override Task SelectMany_with_navigation_filter_paging_and_explicit_DefaultIfEmpty(bool isAsync)
        {
            return base.SelectMany_with_navigation_filter_paging_and_explicit_DefaultIfEmpty(isAsync);
        }

        public override Task Select_join_subquery_containing_filter_and_distinct(bool isAsync)
        {
            return base.Select_join_subquery_containing_filter_and_distinct(isAsync);
        }

        [ActianTodo]
        public override Task Select_join_with_key_selector_being_a_subquery(bool isAsync)
        {
            return base.Select_join_with_key_selector_being_a_subquery(isAsync);
        }

        [ActianTodo]
        public override Task Contains_with_subquery_optional_navigation_and_constant_item(bool isAsync)
        {
            return base.Contains_with_subquery_optional_navigation_and_constant_item(isAsync);
        }

        public override Task Complex_query_with_optional_navigations_and_client_side_evaluation(bool isAsync)
        {
            return base.Complex_query_with_optional_navigations_and_client_side_evaluation(isAsync);
        }

        [ActianTodo]
        public override Task Required_navigation_on_a_subquery_with_First_in_projection(bool isAsync)
        {
            return base.Required_navigation_on_a_subquery_with_First_in_projection(isAsync);
        }

        [ActianTodo]
        public override Task Required_navigation_on_a_subquery_with_complex_projection_and_First(bool isAsync)
        {
            return base.Required_navigation_on_a_subquery_with_complex_projection_and_First(isAsync);
        }

        [ActianTodo]
        public override Task Required_navigation_on_a_subquery_with_First_in_predicate(bool isAsync)
        {
            return base.Required_navigation_on_a_subquery_with_First_in_predicate(isAsync);
        }

        public override Task Manually_created_left_join_propagates_nullability_to_navigations(bool isAsync)
        {
            return base.Manually_created_left_join_propagates_nullability_to_navigations(isAsync);
        }

        public override Task Optional_navigation_propagates_nullability_to_manually_created_left_join1(bool isAsync)
        {
            return base.Optional_navigation_propagates_nullability_to_manually_created_left_join1(isAsync);
        }

        public override Task Optional_navigation_propagates_nullability_to_manually_created_left_join2(bool isAsync)
        {
            return base.Optional_navigation_propagates_nullability_to_manually_created_left_join2(isAsync);
        }

        public override Task Null_reference_protection_complex(bool isAsync)
        {
            return base.Null_reference_protection_complex(isAsync);
        }

        public override Task Null_reference_protection_complex_materialization(bool isAsync)
        {
            return base.Null_reference_protection_complex_materialization(isAsync);
        }

        public override Task Null_reference_protection_complex_client_eval(bool isAsync)
        {
            return base.Null_reference_protection_complex_client_eval(isAsync);
        }

        public override Task GroupJoin_with_complex_subquery_with_joins_does_not_get_flattened(bool isAsync)
        {
            return base.GroupJoin_with_complex_subquery_with_joins_does_not_get_flattened(isAsync);
        }

        public override Task GroupJoin_with_complex_subquery_with_joins_does_not_get_flattened2(bool isAsync)
        {
            return base.GroupJoin_with_complex_subquery_with_joins_does_not_get_flattened2(isAsync);
        }

        public override Task GroupJoin_with_complex_subquery_with_joins_does_not_get_flattened3(bool isAsync)
        {
            return base.GroupJoin_with_complex_subquery_with_joins_does_not_get_flattened3(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_with_complex_subquery_with_joins_with_reference_to_grouping1(bool isAsync)
        {
            return base.GroupJoin_with_complex_subquery_with_joins_with_reference_to_grouping1(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_with_complex_subquery_with_joins_with_reference_to_grouping2(bool isAsync)
        {
            return base.GroupJoin_with_complex_subquery_with_joins_with_reference_to_grouping2(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_on_a_subquery_containing_another_GroupJoin_projecting_outer(bool isAsync)
        {
            return base.GroupJoin_on_a_subquery_containing_another_GroupJoin_projecting_outer(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_on_a_subquery_containing_another_GroupJoin_projecting_outer_with_client_method(bool isAsync)
        {
            return base.GroupJoin_on_a_subquery_containing_another_GroupJoin_projecting_outer_with_client_method(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_on_a_subquery_containing_another_GroupJoin_projecting_inner(bool isAsync)
        {
            return base.GroupJoin_on_a_subquery_containing_another_GroupJoin_projecting_inner(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_on_a_subquery_containing_another_GroupJoin_with_orderby_on_inner_sequence_projecting_inner(
            bool isAsync)
        {
            return base.GroupJoin_on_a_subquery_containing_another_GroupJoin_with_orderby_on_inner_sequence_projecting_inner(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_on_left_side_being_a_subquery(bool isAsync)
        {
            return base.GroupJoin_on_left_side_being_a_subquery(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_on_right_side_being_a_subquery(bool isAsync)
        {
            return base.GroupJoin_on_right_side_being_a_subquery(isAsync);
        }

        public override Task GroupJoin_in_subquery_with_client_result_operator(bool isAsync)
        {
            return base.GroupJoin_in_subquery_with_client_result_operator(isAsync);
        }

        public override Task GroupJoin_in_subquery_with_client_projection(bool isAsync)
        {
            return base.GroupJoin_in_subquery_with_client_projection(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_in_subquery_with_client_projection_nested1(bool isAsync)
        {
            return base.GroupJoin_in_subquery_with_client_projection_nested1(isAsync);
        }

        public override Task GroupJoin_in_subquery_with_client_projection_nested2(bool isAsync)
        {
            return base.GroupJoin_in_subquery_with_client_projection_nested2(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_reference_to_group_in_OrderBy(bool isAsync)
        {
            return base.GroupJoin_reference_to_group_in_OrderBy(isAsync);
        }

        public override Task GroupJoin_client_method_on_outer(bool isAsync)
        {
            return base.GroupJoin_client_method_on_outer(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_client_method_in_OrderBy(bool isAsync)
        {
            return base.GroupJoin_client_method_in_OrderBy(isAsync);
        }

        public override Task GroupJoin_without_DefaultIfEmpty(bool isAsync)
        {
            return base.GroupJoin_without_DefaultIfEmpty(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_with_subquery_on_inner(bool isAsync)
        {
            return base.GroupJoin_with_subquery_on_inner(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_with_subquery_on_inner_and_no_DefaultIfEmpty(bool isAsync)
        {
            return base.GroupJoin_with_subquery_on_inner_and_no_DefaultIfEmpty(isAsync);
        }

        [ActianTodo]
        public override Task Optional_navigation_in_subquery_with_unrelated_projection(bool isAsync)
        {
            return base.Optional_navigation_in_subquery_with_unrelated_projection(isAsync);
        }

        [ActianTodo]
        public override Task Explicit_GroupJoin_in_subquery_with_unrelated_projection(bool isAsync)
        {
            return base.Explicit_GroupJoin_in_subquery_with_unrelated_projection(isAsync);
        }

        public override async Task Explicit_GroupJoin_in_subquery_with_unrelated_projection2(bool isAsync)
        {
            await base.Explicit_GroupJoin_in_subquery_with_unrelated_projection2(isAsync);
            AssertSql(@"
                SELECT ""t2"".""Id""
                FROM (
                    SELECT DISTINCT ""l"".""Id"", ""l"".""Date"", ""l"".""Name""
                    FROM ""Level1"" AS ""l""
                    LEFT JOIN (
                        SELECT ""l0"".""Id"", ""l0"".""Date"", ""l0"".""Name"", ""t"".""Id"" AS ""Id0"", ""t"".""OneToOne_Required_PK_Date"", ""t"".""Level1_Optional_Id"", ""t"".""Level1_Required_Id"", ""t"".""Level2_Name"", ""t"".""OneToMany_Optional_Inverse2Id"", ""t"".""OneToMany_Required_Inverse2Id"", ""t"".""OneToOne_Optional_PK_Inverse2Id""
                        FROM ""Level1"" AS ""l0""
                        LEFT JOIN (
                            SELECT ""l1"".""Id"", ""l1"".""OneToOne_Required_PK_Date"", ""l1"".""Level1_Optional_Id"", ""l1"".""Level1_Required_Id"", ""l1"".""Level2_Name"", ""l1"".""OneToMany_Optional_Inverse2Id"", ""l1"".""OneToMany_Required_Inverse2Id"", ""l1"".""OneToOne_Optional_PK_Inverse2Id"", ""l2"".""Id"" AS ""Id0""
                            FROM ""Level1"" AS ""l1""
                            INNER JOIN ""Level1"" AS ""l2"" ON ""l1"".""Id"" = ""l2"".""Id""
                            WHERE ""l1"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l1"".""Level1_Required_Id"" IS NOT NULL AND ""l1"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                        ) AS ""t"" ON ""l0"".""Id"" = ""t"".""Id""
                        WHERE ""t"".""Id"" IS NOT NULL
                    ) AS ""t0"" ON ""l"".""Id"" = ""t0"".""Level1_Optional_Id""
                    LEFT JOIN (
                        SELECT ""l3"".""Id"", ""l3"".""OneToOne_Required_PK_Date"", ""l3"".""Level1_Optional_Id"", ""l3"".""Level1_Required_Id"", ""l3"".""Level2_Name"", ""l3"".""OneToMany_Optional_Inverse2Id"", ""l3"".""OneToMany_Required_Inverse2Id"", ""l3"".""OneToOne_Optional_PK_Inverse2Id"", ""l4"".""Id"" AS ""Id0""
                        FROM ""Level1"" AS ""l3""
                        INNER JOIN ""Level1"" AS ""l4"" ON ""l3"".""Id"" = ""l4"".""Id""
                        WHERE ""l3"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l3"".""Level1_Required_Id"" IS NOT NULL AND ""l3"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                    ) AS ""t1"" ON ""t0"".""Id"" = ""t1"".""Id""
                    WHERE (""t1"".""Level2_Name"" <> N'Foo') OR ""t1"".""Level2_Name"" IS NULL
                ) AS ""t2""
            ");
        }

        public override Task Explicit_GroupJoin_in_subquery_with_unrelated_projection3(bool isAsync)
        {
            return base.Explicit_GroupJoin_in_subquery_with_unrelated_projection3(isAsync);
        }

        [ActianTodo]
        public override Task Explicit_GroupJoin_in_subquery_with_unrelated_projection4(bool isAsync)
        {
            return base.Explicit_GroupJoin_in_subquery_with_unrelated_projection4(isAsync);
        }

        public override Task Explicit_GroupJoin_in_subquery_with_scalar_result_operator(bool isAsync)
        {
            return base.Explicit_GroupJoin_in_subquery_with_scalar_result_operator(isAsync);
        }

        public override Task Explicit_GroupJoin_in_subquery_with_multiple_result_operator_distinct_count_materializes_main_clause(
            bool isAsync)
        {
            return base.Explicit_GroupJoin_in_subquery_with_multiple_result_operator_distinct_count_materializes_main_clause(isAsync);
        }

        [ActianTodo]
        public override Task Where_on_multilevel_reference_in_subquery_with_outer_projection(bool isAsync)
        {
            return base.Where_on_multilevel_reference_in_subquery_with_outer_projection(isAsync);
        }

        public override Task Join_condition_optimizations_applied_correctly_when_anonymous_type_with_single_property(bool isAsync)
        {
            return base.Join_condition_optimizations_applied_correctly_when_anonymous_type_with_single_property(isAsync);
        }

        public override Task Join_condition_optimizations_applied_correctly_when_anonymous_type_with_multiple_properties(bool isAsync)
        {
            return base.Join_condition_optimizations_applied_correctly_when_anonymous_type_with_multiple_properties(isAsync);
        }

        public override Task Navigation_filter_navigation_grouping_ordering_by_group_key(bool isAsync)
        {
            return base.Navigation_filter_navigation_grouping_ordering_by_group_key(isAsync);
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override async Task Nested_group_join_with_take(bool isAsync)
        {
            await base.Nested_group_join_with_take(isAsync);
            AssertSql(@"
                @__p_0='2'
                
                SELECT ""t5"".""Level2_Name""
                FROM (
                    SELECT FIRST @__p_0 ""t1"".""Id"", ""t1"".""OneToOne_Required_PK_Date"", ""t1"".""Level1_Optional_Id"", ""t1"".""Level1_Required_Id"", ""t1"".""Level2_Name"", ""t1"".""OneToMany_Optional_Inverse2Id"", ""t1"".""OneToMany_Required_Inverse2Id"", ""t1"".""OneToOne_Optional_PK_Inverse2Id"", ""l"".""Id"" AS ""Id0""
                    FROM ""Level1"" AS ""l""
                    LEFT JOIN (
                        SELECT ""l0"".""Id"", ""l0"".""Date"", ""l0"".""Name"", ""t"".""Id"" AS ""Id0"", ""t"".""OneToOne_Required_PK_Date"", ""t"".""Level1_Optional_Id"", ""t"".""Level1_Required_Id"", ""t"".""Level2_Name"", ""t"".""OneToMany_Optional_Inverse2Id"", ""t"".""OneToMany_Required_Inverse2Id"", ""t"".""OneToOne_Optional_PK_Inverse2Id""
                        FROM ""Level1"" AS ""l0""
                        LEFT JOIN (
                            SELECT ""l1"".""Id"", ""l1"".""OneToOne_Required_PK_Date"", ""l1"".""Level1_Optional_Id"", ""l1"".""Level1_Required_Id"", ""l1"".""Level2_Name"", ""l1"".""OneToMany_Optional_Inverse2Id"", ""l1"".""OneToMany_Required_Inverse2Id"", ""l1"".""OneToOne_Optional_PK_Inverse2Id"", ""l2"".""Id"" AS ""Id0""
                            FROM ""Level1"" AS ""l1""
                            INNER JOIN ""Level1"" AS ""l2"" ON ""l1"".""Id"" = ""l2"".""Id""
                            WHERE ""l1"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l1"".""Level1_Required_Id"" IS NOT NULL AND ""l1"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                        ) AS ""t"" ON ""l0"".""Id"" = ""t"".""Id""
                        WHERE ""t"".""Id"" IS NOT NULL
                    ) AS ""t0"" ON ""l"".""Id"" = ""t0"".""Level1_Optional_Id""
                    LEFT JOIN (
                        SELECT ""l3"".""Id"", ""l3"".""OneToOne_Required_PK_Date"", ""l3"".""Level1_Optional_Id"", ""l3"".""Level1_Required_Id"", ""l3"".""Level2_Name"", ""l3"".""OneToMany_Optional_Inverse2Id"", ""l3"".""OneToMany_Required_Inverse2Id"", ""l3"".""OneToOne_Optional_PK_Inverse2Id"", ""l4"".""Id"" AS ""Id0""
                        FROM ""Level1"" AS ""l3""
                        INNER JOIN ""Level1"" AS ""l4"" ON ""l3"".""Id"" = ""l4"".""Id""
                        WHERE ""l3"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l3"".""Level1_Required_Id"" IS NOT NULL AND ""l3"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                    ) AS ""t1"" ON ""t0"".""Id"" = ""t1"".""Id""
                    ORDER BY ""l"".""Id""
                ) AS ""t2""
                LEFT JOIN (
                    SELECT ""l5"".""Id"", ""l5"".""Date"", ""l5"".""Name"", ""t3"".""Id"" AS ""Id0"", ""t3"".""OneToOne_Required_PK_Date"", ""t3"".""Level1_Optional_Id"", ""t3"".""Level1_Required_Id"", ""t3"".""Level2_Name"", ""t3"".""OneToMany_Optional_Inverse2Id"", ""t3"".""OneToMany_Required_Inverse2Id"", ""t3"".""OneToOne_Optional_PK_Inverse2Id""
                    FROM ""Level1"" AS ""l5""
                    LEFT JOIN (
                        SELECT ""l6"".""Id"", ""l6"".""OneToOne_Required_PK_Date"", ""l6"".""Level1_Optional_Id"", ""l6"".""Level1_Required_Id"", ""l6"".""Level2_Name"", ""l6"".""OneToMany_Optional_Inverse2Id"", ""l6"".""OneToMany_Required_Inverse2Id"", ""l6"".""OneToOne_Optional_PK_Inverse2Id"", ""l7"".""Id"" AS ""Id0""
                        FROM ""Level1"" AS ""l6""
                        INNER JOIN ""Level1"" AS ""l7"" ON ""l6"".""Id"" = ""l7"".""Id""
                        WHERE ""l6"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l6"".""Level1_Required_Id"" IS NOT NULL AND ""l6"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                    ) AS ""t3"" ON ""l5"".""Id"" = ""t3"".""Id""
                    WHERE ""t3"".""Id"" IS NOT NULL
                ) AS ""t4"" ON ""t2"".""Id"" = ""t4"".""Level1_Optional_Id""
                LEFT JOIN (
                    SELECT ""l8"".""Id"", ""l8"".""OneToOne_Required_PK_Date"", ""l8"".""Level1_Optional_Id"", ""l8"".""Level1_Required_Id"", ""l8"".""Level2_Name"", ""l8"".""OneToMany_Optional_Inverse2Id"", ""l8"".""OneToMany_Required_Inverse2Id"", ""l8"".""OneToOne_Optional_PK_Inverse2Id"", ""l9"".""Id"" AS ""Id0""
                    FROM ""Level1"" AS ""l8""
                    INNER JOIN ""Level1"" AS ""l9"" ON ""l8"".""Id"" = ""l9"".""Id""
                    WHERE ""l8"".""OneToMany_Required_Inverse2Id"" IS NOT NULL AND (""l8"".""Level1_Required_Id"" IS NOT NULL AND ""l8"".""OneToOne_Required_PK_Date"" IS NOT NULL)
                ) AS ""t5"" ON ""t4"".""Id"" = ""t5"".""Id""
                ORDER BY ""t2"".""Id0""
            ");
        }

        public override Task Navigation_with_same_navigation_compared_to_null(bool isAsync)
        {
            return base.Navigation_with_same_navigation_compared_to_null(isAsync);
        }

        public override Task Multi_level_navigation_compared_to_null(bool isAsync)
        {
            return base.Multi_level_navigation_compared_to_null(isAsync);
        }

        public override Task Multi_level_navigation_with_same_navigation_compared_to_null(bool isAsync)
        {
            return base.Multi_level_navigation_with_same_navigation_compared_to_null(isAsync);
        }

        public override Task Navigations_compared_to_each_other1(bool isAsync)
        {
            return base.Navigations_compared_to_each_other1(isAsync);
        }

        public override Task Navigations_compared_to_each_other2(bool isAsync)
        {
            return base.Navigations_compared_to_each_other2(isAsync);
        }

        public override Task Navigations_compared_to_each_other3(bool isAsync)
        {
            return base.Navigations_compared_to_each_other3(isAsync);
        }

        public override Task Navigations_compared_to_each_other4(bool isAsync)
        {
            return base.Navigations_compared_to_each_other4(isAsync);
        }

        public override Task Navigations_compared_to_each_other5(bool isAsync)
        {
            return base.Navigations_compared_to_each_other5(isAsync);
        }

        public override Task Level4_Include(bool isAsync)
        {
            return base.Level4_Include(isAsync);
        }

        public override Task Comparing_collection_navigation_on_optional_reference_to_null(bool isAsync)
        {
            return base.Comparing_collection_navigation_on_optional_reference_to_null(isAsync);
        }

        [ActianTodo]
        public override Task Select_subquery_with_client_eval_and_navigation1(bool isAsync)
        {
            return base.Select_subquery_with_client_eval_and_navigation1(isAsync);
        }

        [ActianTodo]
        public override Task Select_subquery_with_client_eval_and_navigation2(bool isAsync)
        {
            return base.Select_subquery_with_client_eval_and_navigation2(isAsync);
        }

        [ActianTodo]
        public override Task Select_subquery_with_client_eval_and_multi_level_navigation(bool isAsync)
        {
            return base.Select_subquery_with_client_eval_and_multi_level_navigation(isAsync);
        }

        [ActianTodo]
        public override Task Member_doesnt_get_pushed_down_into_subquery_with_result_operator(bool isAsync)
        {
            return base.Member_doesnt_get_pushed_down_into_subquery_with_result_operator(isAsync);
        }

        [ActianTodo]
        public override Task Subquery_with_Distinct_Skip_FirstOrDefault_without_OrderBy(bool isAsync)
        {
            return base.Subquery_with_Distinct_Skip_FirstOrDefault_without_OrderBy(isAsync);
        }

        public override Task Project_collection_navigation(bool isAsync)
        {
            return base.Project_collection_navigation(isAsync);
        }

        public override Task Project_collection_navigation_nested(bool isAsync)
        {
            return base.Project_collection_navigation_nested(isAsync);
        }

        [ActianTodo]
        public override Task Project_collection_navigation_nested_with_take(bool isAsync)
        {
            return base.Project_collection_navigation_nested_with_take(isAsync);
        }

        public override Task Project_collection_navigation_using_ef_property(bool isAsync)
        {
            return base.Project_collection_navigation_using_ef_property(isAsync);
        }

        public override Task Project_collection_navigation_nested_anonymous(bool isAsync)
        {
            return base.Project_collection_navigation_nested_anonymous(isAsync);
        }

        public override Task Project_collection_navigation_count(bool isAsync)
        {
            return base.Project_collection_navigation_count(isAsync);
        }

        public override Task Project_collection_navigation_composed(bool isAsync)
        {
            return base.Project_collection_navigation_composed(isAsync);
        }

        public override Task Project_collection_and_root_entity(bool isAsync)
        {
            return base.Project_collection_and_root_entity(isAsync);
        }

        public override Task Project_collection_and_include(bool isAsync)
        {
            return base.Project_collection_and_include(isAsync);
        }

        public override Task Project_navigation_and_collection(bool isAsync)
        {
            return base.Project_navigation_and_collection(isAsync);
        }

        [ActianTodo]
        public override Task Include_inside_subquery(bool isAsync)
        {
            return base.Include_inside_subquery(isAsync);
        }

        public override Task Select_optional_navigation_property_string_concat(bool isAsync)
        {
            return base.Select_optional_navigation_property_string_concat(isAsync);
        }

        [ActianTodo]
        public override Task Include_collection_with_multiple_orderbys_member(bool isAsync)
        {
            return base.Include_collection_with_multiple_orderbys_member(isAsync);
        }

        [ActianTodo]
        public override Task Include_collection_with_multiple_orderbys_property(bool isAsync)
        {
            return base.Include_collection_with_multiple_orderbys_property(isAsync);
        }

        [ActianTodo]
        public override Task Include_collection_with_multiple_orderbys_methodcall(bool isAsync)
        {
            return base.Include_collection_with_multiple_orderbys_methodcall(isAsync);
        }

        [ActianTodo]
        public override Task Include_collection_with_multiple_orderbys_complex(bool isAsync)
        {
            return base.Include_collection_with_multiple_orderbys_complex(isAsync);
        }

        [ActianTodo]
        public override Task Include_collection_with_multiple_orderbys_complex_repeated(bool isAsync)
        {
            return base.Include_collection_with_multiple_orderbys_complex_repeated(isAsync);
        }

        public override void Entries_for_detached_entities_are_removed()
        {
            base.Entries_for_detached_entities_are_removed();
        }

        [ActianTodo]
        public override Task Include_reference_with_groupby_in_subquery(bool isAsync)
        {
            return base.Include_reference_with_groupby_in_subquery(isAsync);
        }

        [ActianTodo]
        public override Task Include_collection_with_groupby_in_subquery(bool isAsync)
        {
            return base.Include_collection_with_groupby_in_subquery(isAsync);
        }

        [ActianTodo]
        public override Task Multi_include_with_groupby_in_subquery(bool isAsync)
        {
            return base.Multi_include_with_groupby_in_subquery(isAsync);
        }

        [ActianTodo]
        public override Task Include_collection_with_groupby_in_subquery_and_filter_before_groupby(bool isAsync)
        {
            return base.Include_collection_with_groupby_in_subquery_and_filter_before_groupby(isAsync);
        }

        [ActianTodo]
        public override Task Include_collection_with_groupby_in_subquery_and_filter_after_groupby(bool isAsync)
        {
            return base.Include_collection_with_groupby_in_subquery_and_filter_after_groupby(isAsync);
        }

        public override Task String_include_multiple_derived_navigation_with_same_name_and_same_type(bool isAsync)
        {
            return base.String_include_multiple_derived_navigation_with_same_name_and_same_type(isAsync);
        }

        public override Task String_include_multiple_derived_navigation_with_same_name_and_different_type(bool isAsync)
        {
            return base.String_include_multiple_derived_navigation_with_same_name_and_different_type(isAsync);
        }

        public override Task String_include_multiple_derived_navigation_with_same_name_and_different_type_nested_also_includes_partially_matching_navigation_chains(
                bool isAsync)
        {
            return base.String_include_multiple_derived_navigation_with_same_name_and_different_type_nested_also_includes_partially_matching_navigation_chains(isAsync);
        }

        public override Task String_include_multiple_derived_collection_navigation_with_same_name_and_same_type(bool isAsync)
        {
            return base.String_include_multiple_derived_collection_navigation_with_same_name_and_same_type(isAsync);
        }

        public override Task String_include_multiple_derived_collection_navigation_with_same_name_and_different_type(bool isAsync)
        {
            return base.String_include_multiple_derived_collection_navigation_with_same_name_and_different_type(isAsync);
        }

        public override Task String_include_multiple_derived_collection_navigation_with_same_name_and_different_type_nested_also_includes_partially_matching_navigation_chains(
                bool isAsync)
        {
            return base.String_include_multiple_derived_collection_navigation_with_same_name_and_different_type_nested_also_includes_partially_matching_navigation_chains(isAsync);
        }

        public override Task String_include_multiple_derived_navigations_complex(bool isAsync)
        {
            return base.String_include_multiple_derived_navigations_complex(isAsync);
        }

        public override Task Include_reference_collection_order_by_reference_navigation(bool isAsync)
        {
            return base.Include_reference_collection_order_by_reference_navigation(isAsync);
        }

        public override Task Nav_rewrite_doesnt_apply_null_protection_for_function_arguments(bool isAsync)
        {
            return base.Nav_rewrite_doesnt_apply_null_protection_for_function_arguments(isAsync);
        }

        [ActianTodo]
        public override Task Accessing_optional_property_inside_result_operator_subquery(bool isAsync)
        {
            return base.Accessing_optional_property_inside_result_operator_subquery(isAsync);
        }

        [ActianTodo]
        public override Task Include_after_SelectMany_and_reference_navigation(bool isAsync)
        {
            return base.Include_after_SelectMany_and_reference_navigation(isAsync);
        }

        public override Task Include_after_multiple_SelectMany_and_reference_navigation(bool isAsync)
        {
            return base.Include_after_multiple_SelectMany_and_reference_navigation(isAsync);
        }

        public override Task Include_after_SelectMany_and_multiple_reference_navigations(bool isAsync)
        {
            return base.Include_after_SelectMany_and_multiple_reference_navigations(isAsync);
        }

        [ActianTodo]
        public override Task Include_after_SelectMany_and_reference_navigation_with_another_SelectMany_with_Distinct(bool isAsync)
        {
            return base.Include_after_SelectMany_and_reference_navigation_with_another_SelectMany_with_Distinct(isAsync);
        }

        [ActianTodo]
        public override Task SelectMany_subquery_with_custom_projection(bool isAsync)
        {
            return base.SelectMany_subquery_with_custom_projection(isAsync);
        }

        public override Task Null_check_in_anonymous_type_projection_should_not_be_removed(bool isAsync)
        {
            return base.Null_check_in_anonymous_type_projection_should_not_be_removed(isAsync);
        }

        public override Task Null_check_in_Dto_projection_should_not_be_removed(bool isAsync)
        {
            return base.Null_check_in_Dto_projection_should_not_be_removed(isAsync);
        }

        public override Task SelectMany_navigation_property_followed_by_select_collection_navigation(bool isAsync)
        {
            return base.SelectMany_navigation_property_followed_by_select_collection_navigation(isAsync);
        }

        public override Task Multiple_SelectMany_navigation_property_followed_by_select_collection_navigation(bool isAsync)
        {
            return base.Multiple_SelectMany_navigation_property_followed_by_select_collection_navigation(isAsync);
        }

        public override Task SelectMany_navigation_property_with_include_and_followed_by_select_collection_navigation(bool isAsync)
        {
            return base.SelectMany_navigation_property_with_include_and_followed_by_select_collection_navigation(isAsync);
        }

        public override Task Include1(bool isAsync)
        {
            return base.Include1(isAsync);
        }

        public override Task Include2(bool isAsync)
        {
            return base.Include2(isAsync);
        }

        public override Task Include3(bool isAsync)
        {
            return base.Include3(isAsync);
        }

        public override Task Include4(bool isAsync)
        {
            return base.Include4(isAsync);
        }

        public override Task Include5(bool isAsync)
        {
            return base.Include5(isAsync);
        }

        public override Task Include6(bool isAsync)
        {
            return base.Include6(isAsync);
        }

        public override Task Include7(bool isAsync)
        {
            return base.Include7(isAsync);
        }

        [ActianTodo]
        public override Task Include8(bool isAsync)
        {
            return base.Include8(isAsync);
        }

        [ActianTodo]
        public override Task Include9(bool isAsync)
        {
            return base.Include9(isAsync);
        }

        public override Task Include10(bool isAsync)
        {
            return base.Include10(isAsync);
        }

        public override Task Include11(bool isAsync)
        {
            return base.Include11(isAsync);
        }

        public override Task Include12(bool isAsync)
        {
            return base.Include12(isAsync);
        }

        public override Task Include13(bool isAsync)
        {
            return base.Include13(isAsync);
        }

        public override Task Include14(bool isAsync)
        {
            return base.Include14(isAsync);
        }

        [ActianTodo]
        public override void Include15()
        {
            base.Include15();
        }

        [ActianTodo]
        public override void Include16()
        {
            base.Include16();
        }

        [ActianTodo]
        public override void Include17()
        {
            base.Include17();
        }

        public override Task Include18_1(bool isAsync)
        {
            return base.Include18_1(isAsync);
        }

        [ActianTodo]
        public override Task Include18_1_1(bool isAsync)
        {
            return base.Include18_1_1(isAsync);
        }

        public override Task Include18_2(bool isAsync)
        {
            return base.Include18_2(isAsync);
        }

        [ActianTodo]
        public override void Include18_3()
        {
            base.Include18_3();
        }

        [ActianTodo]
        public override void Include18_3_1()
        {
            base.Include18_3_1();
        }

        [ActianTodo]
        public override void Include18_3_2()
        {
            base.Include18_3_2();
        }

        public override Task Include18_3_3(bool isAsync)
        {
            return base.Include18_3_3(isAsync);
        }

        public override void Include18_4()
        {
            base.Include18_4();
        }

        [ActianTodo]
        public override void Include18()
        {
            base.Include18();
        }

        public override void Include19()
        {
            base.Include19();
        }

        public override void IncludeCollection1()
        {
            base.IncludeCollection1();
        }

        public override void IncludeCollection2()
        {
            base.IncludeCollection2();
        }

        public override void IncludeCollection3()
        {
            base.IncludeCollection3();
        }

        public override void IncludeCollection4()
        {
            base.IncludeCollection4();
        }

        public override void IncludeCollection5()
        {
            base.IncludeCollection5();
        }

        public override void IncludeCollection6()
        {
            base.IncludeCollection6();
        }

        public override void IncludeCollection6_1()
        {
            base.IncludeCollection6_1();
        }

        public override void IncludeCollection6_2()
        {
            base.IncludeCollection6_2();
        }

        public override void IncludeCollection6_3()
        {
            base.IncludeCollection6_3();
        }

        public override void IncludeCollection6_4()
        {
            base.IncludeCollection6_4();
        }

        public override void IncludeCollection7()
        {
            base.IncludeCollection7();
        }

        public override Task IncludeCollection8(bool isAsync)
        {
            return base.IncludeCollection8(isAsync);
        }

        public override Task Include_with_all_method_include_gets_ignored(bool isAsync)
        {
            return base.Include_with_all_method_include_gets_ignored(isAsync);
        }

        public override Task Join_with_navigations_in_the_result_selector1(bool isAsync)
        {
            return base.Join_with_navigations_in_the_result_selector1(isAsync);
        }

        public override void Join_with_navigations_in_the_result_selector2()
        {
            base.Join_with_navigations_in_the_result_selector2();
        }

        [ActianTodo]
        public override void GroupJoin_with_navigations_in_the_result_selector()
        {
            base.GroupJoin_with_navigations_in_the_result_selector();
        }

        public override void Member_pushdown_chain_3_levels_deep()
        {
            base.Member_pushdown_chain_3_levels_deep();
        }

        public override void Member_pushdown_chain_3_levels_deep_entity()
        {
            base.Member_pushdown_chain_3_levels_deep_entity();
        }

        public override void Member_pushdown_with_collection_navigation_in_the_middle()
        {
            base.Member_pushdown_with_collection_navigation_in_the_middle();
        }

        [ActianTodo]
        public override Task Member_pushdown_with_multiple_collections(bool isAsync)
        {
            return base.Member_pushdown_with_multiple_collections(isAsync);
        }

        public override Task Include_multiple_collections_on_same_level(bool isAsync)
        {
            return base.Include_multiple_collections_on_same_level(isAsync);
        }

        public override Task Null_check_removal_applied_recursively(bool isAsync)
        {
            return base.Null_check_removal_applied_recursively(isAsync);
        }

        public override Task Null_check_different_structure_does_not_remove_null_checks(bool isAsync)
        {
            return base.Null_check_different_structure_does_not_remove_null_checks(isAsync);
        }

        public override Task Union_over_entities_with_different_nullability(bool isAsync)
        {
            return base.Union_over_entities_with_different_nullability(isAsync);
        }

        [ActianTodo]
        public override Task Lift_projection_mapping_when_pushing_down_subquery(bool isAsync)
        {
            return base.Lift_projection_mapping_when_pushing_down_subquery(isAsync);
        }

        public override Task Including_reference_navigation_and_projecting_collection_navigation(bool isAsync)
        {
            return base.Including_reference_navigation_and_projecting_collection_navigation(isAsync);
        }

        public override Task Including_reference_navigation_and_projecting_collection_navigation_2(bool isAsync)
        {
            return base.Including_reference_navigation_and_projecting_collection_navigation_2(isAsync);
        }

        [ActianTodo]
        public override Task OrderBy_collection_count_ThenBy_reference_navigation(bool async)
        {
            return base.OrderBy_collection_count_ThenBy_reference_navigation(async);
        }

        [ActianTodo]
        public override Task SelectMany_with_outside_reference_to_joined_table_correctly_translated_to_apply(bool async)
        {
            return base.SelectMany_with_outside_reference_to_joined_table_correctly_translated_to_apply(async);
        }
    }
}
