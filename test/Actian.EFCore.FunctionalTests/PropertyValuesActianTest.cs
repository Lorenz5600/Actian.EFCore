using System;
using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;
using Xunit.Abstractions;

#pragma warning disable xUnit1024 // Test methods cannot have overloads
namespace Actian.EFCore
{
    public class PropertyValuesActianTest : PropertyValuesTestBase<PropertyValuesActianTest.PropertyValuesActianFixture>, IDisposable
    {
        public PropertyValuesActianTest(PropertyValuesActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void Dispose() => Helpers.LogSql();

        [ActianTodo]
        public override Task Scalar_current_values_can_be_accessed_as_a_property_dictionary()
        {
            return base.Scalar_current_values_can_be_accessed_as_a_property_dictionary();
        }

        [ActianTodo]
        public override Task Scalar_original_values_can_be_accessed_as_a_property_dictionary()
        {
            return base.Scalar_original_values_can_be_accessed_as_a_property_dictionary();
        }

        [ActianTodo]
        public override Task Scalar_store_values_can_be_accessed_as_a_property_dictionary()
        {
            return base.Scalar_store_values_can_be_accessed_as_a_property_dictionary();
        }

        [ActianTodo]
        public override Task Scalar_store_values_can_be_accessed_asynchronously_as_a_property_dictionary()
        {
            return base.Scalar_store_values_can_be_accessed_asynchronously_as_a_property_dictionary();
        }

        [ActianTodo]
        public override Task Scalar_current_values_can_be_accessed_as_a_property_dictionary_using_IProperty()
        {
            return base.Scalar_current_values_can_be_accessed_as_a_property_dictionary_using_IProperty();
        }

        [ActianTodo]
        public override Task Scalar_original_values_can_be_accessed_as_a_property_dictionary_using_IProperty()
        {
            return base.Scalar_original_values_can_be_accessed_as_a_property_dictionary_using_IProperty();
        }

        [ActianTodo]
        public override Task Scalar_store_values_can_be_accessed_as_a_property_dictionary_using_IProperty()
        {
            return base.Scalar_store_values_can_be_accessed_as_a_property_dictionary_using_IProperty();
        }

        [ActianTodo]
        public override Task Scalar_store_values_can_be_accessed_asynchronously_as_a_property_dictionary_using_IProperty()
        {
            return base.Scalar_store_values_can_be_accessed_asynchronously_as_a_property_dictionary_using_IProperty();
        }

        [ActianTodo]
        public override Task Scalar_current_values_of_a_derived_object_can_be_accessed_as_a_property_dictionary()
        {
            return base.Scalar_current_values_of_a_derived_object_can_be_accessed_as_a_property_dictionary();
        }

        [ActianTodo]
        public override Task Scalar_original_values_of_a_derived_object_can_be_accessed_as_a_property_dictionary()
        {
            return base.Scalar_original_values_of_a_derived_object_can_be_accessed_as_a_property_dictionary();
        }

        [ActianTodo]
        public override Task Scalar_store_values_of_a_derived_object_can_be_accessed_as_a_property_dictionary()
        {
            return base.Scalar_store_values_of_a_derived_object_can_be_accessed_as_a_property_dictionary();
        }

        [ActianTodo]
        public override Task Scalar_store_values_of_a_derived_object_can_be_accessed_asynchronously_as_a_property_dictionary()
        {
            return base.Scalar_store_values_of_a_derived_object_can_be_accessed_asynchronously_as_a_property_dictionary();
        }

        [ActianTodo]
        public override Task Scalar_current_values_can_be_accessed_as_a_non_generic_property_dictionary()
        {
            return base.Scalar_current_values_can_be_accessed_as_a_non_generic_property_dictionary();
        }

        [ActianTodo]
        public override Task Scalar_original_values_can_be_accessed_as_a_non_generic_property_dictionary()
        {
            return base.Scalar_original_values_can_be_accessed_as_a_non_generic_property_dictionary();
        }

        [ActianTodo]
        public override Task Scalar_store_values_can_be_accessed_as_a_non_generic_property_dictionary()
        {
            return base.Scalar_store_values_can_be_accessed_as_a_non_generic_property_dictionary();
        }

        [ActianTodo]
        public override Task Scalar_store_values_can_be_accessed_asynchronously_as_a_non_generic_property_dictionary()
        {
            return base.Scalar_store_values_can_be_accessed_asynchronously_as_a_non_generic_property_dictionary();
        }

        [ActianTodo]
        public override Task Scalar_current_values_can_be_accessed_as_a_non_generic_property_dictionary_using_IProperty()
        {
            return base.Scalar_current_values_can_be_accessed_as_a_non_generic_property_dictionary_using_IProperty();
        }

        [ActianTodo]
        public override Task Scalar_original_values_can_be_accessed_as_a_non_generic_property_dictionary_using_IProperty()
        {
            return base.Scalar_original_values_can_be_accessed_as_a_non_generic_property_dictionary_using_IProperty();
        }

        [ActianTodo]
        public override Task Scalar_store_values_can_be_accessed_as_a_non_generic_property_dictionary_using_IProperty()
        {
            return base.Scalar_store_values_can_be_accessed_as_a_non_generic_property_dictionary_using_IProperty();
        }

        [ActianTodo]
        public override Task Scalar_store_values_can_be_accessed_asynchronously_as_a_non_generic_property_dictionary_using_IProperty()
        {
            return base.Scalar_store_values_can_be_accessed_asynchronously_as_a_non_generic_property_dictionary_using_IProperty();
        }

        [ActianTodo]
        public override Task Scalar_current_values_of_a_derived_object_can_be_accessed_as_a_non_generic_property_dictionary()
        {
            return base.Scalar_current_values_of_a_derived_object_can_be_accessed_as_a_non_generic_property_dictionary();
        }

        [ActianTodo]
        public override Task Scalar_original_values_of_a_derived_object_can_be_accessed_as_a_non_generic_property_dictionary()
        {
            return base.Scalar_original_values_of_a_derived_object_can_be_accessed_as_a_non_generic_property_dictionary();
        }

        [ActianTodo]
        public override Task Scalar_store_values_of_a_derived_object_can_be_accessed_as_a_non_generic_property_dictionary()
        {
            return base.Scalar_store_values_of_a_derived_object_can_be_accessed_as_a_non_generic_property_dictionary();
        }

        [ActianTodo]
        public override Task Scalar_store_values_of_a_derived_object_can_be_accessed_asynchronously_as_a_non_generic_property_dictionary()
        {
            return base.Scalar_store_values_of_a_derived_object_can_be_accessed_asynchronously_as_a_non_generic_property_dictionary();
        }

        [ActianTodo]
        public override void Scalar_current_values_can_be_set_using_a_property_dictionary()
        {
            base.Scalar_current_values_can_be_set_using_a_property_dictionary();
        }

        [ActianTodo]
        public override void Scalar_original_values_can_be_set_using_a_property_dictionary()
        {
            base.Scalar_original_values_can_be_set_using_a_property_dictionary();
        }

        [ActianTodo]
        public override void Scalar_current_values_can_be_set_using_a_property_dictionary_with_IProperty()
        {
            base.Scalar_current_values_can_be_set_using_a_property_dictionary_with_IProperty();
        }

        [ActianTodo]
        public override void Scalar_original_values_can_be_set_using_a_property_dictionary_with_IProperty()
        {
            base.Scalar_original_values_can_be_set_using_a_property_dictionary_with_IProperty();
        }

        [ActianTodo]
        public override void Scalar_current_values_can_be_set_using_a_non_generic_property_dictionary()
        {
            base.Scalar_current_values_can_be_set_using_a_non_generic_property_dictionary();
        }

        [ActianTodo]
        public override void Scalar_original_values_can_be_set_using_a_non_generic_property_dictionary()
        {
            base.Scalar_original_values_can_be_set_using_a_non_generic_property_dictionary();
        }

        [ActianTodo]
        public override Task Current_values_can_be_copied_into_an_object()
        {
            return base.Current_values_can_be_copied_into_an_object();
        }

        [ActianTodo]
        public override Task Original_values_can_be_copied_into_an_object()
        {
            return base.Original_values_can_be_copied_into_an_object();
        }

        [ActianTodo]
        public override Task Store_values_can_be_copied_into_an_object()
        {
            return base.Store_values_can_be_copied_into_an_object();
        }

        [ActianTodo]
        public override Task Store_values_can_be_copied_into_an_object_asynchronously()
        {
            return base.Store_values_can_be_copied_into_an_object_asynchronously();
        }

        [ActianTodo]
        public override Task Current_values_for_derived_object_can_be_copied_into_an_object()
        {
            return base.Current_values_for_derived_object_can_be_copied_into_an_object();
        }

        [ActianTodo]
        public override Task Original_values_for_derived_object_can_be_copied_into_an_object()
        {
            return base.Original_values_for_derived_object_can_be_copied_into_an_object();
        }

        [ActianTodo]
        public override Task Store_values_for_derived_object_can_be_copied_into_an_object()
        {
            return base.Store_values_for_derived_object_can_be_copied_into_an_object();
        }

        [ActianTodo]
        public override Task Store_values_for_derived_object_can_be_copied_into_an_object_asynchronously()
        {
            return base.Store_values_for_derived_object_can_be_copied_into_an_object_asynchronously();
        }

        [ActianTodo]
        public override Task Current_values_can_be_copied_from_a_non_generic_property_dictionary_into_an_object()
        {
            return base.Current_values_can_be_copied_from_a_non_generic_property_dictionary_into_an_object();
        }

        [ActianTodo]
        public override Task Original_values_can_be_copied_non_generic_property_dictionary_into_an_object()
        {
            return base.Original_values_can_be_copied_non_generic_property_dictionary_into_an_object();
        }

        [ActianTodo]
        public override Task Store_values_can_be_copied_non_generic_property_dictionary_into_an_object()
        {
            return base.Store_values_can_be_copied_non_generic_property_dictionary_into_an_object();
        }

        [ActianTodo]
        public override Task Store_values_can_be_copied_asynchronously_non_generic_property_dictionary_into_an_object()
        {
            return base.Store_values_can_be_copied_asynchronously_non_generic_property_dictionary_into_an_object();
        }

        [ActianTodo]
        public override Task Current_values_can_be_copied_into_a_cloned_dictionary()
        {
            return base.Current_values_can_be_copied_into_a_cloned_dictionary();
        }

        [ActianTodo]
        public override Task Original_values_can_be_copied_into_a_cloned_dictionary()
        {
            return base.Original_values_can_be_copied_into_a_cloned_dictionary();
        }

        [ActianTodo]
        public override Task Store_values_can_be_copied_into_a_cloned_dictionary()
        {
            return base.Store_values_can_be_copied_into_a_cloned_dictionary();
        }

        [ActianTodo]
        public override Task Store_values_can_be_copied_into_a_cloned_dictionary_asynchronously()
        {
            return base.Store_values_can_be_copied_into_a_cloned_dictionary_asynchronously();
        }

        [ActianTodo]
        public override void Values_in_cloned_dictionary_can_be_set_with_IProperty()
        {
            base.Values_in_cloned_dictionary_can_be_set_with_IProperty();
        }

        [ActianTodo]
        public override void Using_bad_property_names_throws()
        {
            base.Using_bad_property_names_throws();
        }

        [ActianTodo]
        public override void Using_bad_IProperty_instances_throws()
        {
            base.Using_bad_IProperty_instances_throws();
        }

        [ActianTodo]
        public override void Using_bad_property_names_throws_derived()
        {
            base.Using_bad_property_names_throws_derived();
        }

        [ActianTodo]
        public override void Using_bad_IProperty_instances_throws_derived()
        {
            base.Using_bad_IProperty_instances_throws_derived();
        }

        [ActianTodo]
        public override Task Current_values_can_be_copied_into_a_non_generic_cloned_dictionary()
        {
            return base.Current_values_can_be_copied_into_a_non_generic_cloned_dictionary();
        }

        [ActianTodo]
        public override Task Original_values_can_be_copied_into_a_non_generic_cloned_dictionary()
        {
            return base.Original_values_can_be_copied_into_a_non_generic_cloned_dictionary();
        }

        [ActianTodo]
        public override Task Store_values_can_be_copied_into_a_non_generic_cloned_dictionary()
        {
            return base.Store_values_can_be_copied_into_a_non_generic_cloned_dictionary();
        }

        [ActianTodo]
        public override Task Store_values_can_be_copied_asynchronously_into_a_non_generic_cloned_dictionary()
        {
            return base.Store_values_can_be_copied_asynchronously_into_a_non_generic_cloned_dictionary();
        }

        [ActianTodo]
        public override Task Current_values_can_be_read_or_set_for_an_object_in_the_Deleted_state()
        {
            return base.Current_values_can_be_read_or_set_for_an_object_in_the_Deleted_state();
        }

        [ActianTodo]
        public override Task Original_values_can_be_read_and_set_for_an_object_in_the_Deleted_state()
        {
            return base.Original_values_can_be_read_and_set_for_an_object_in_the_Deleted_state();
        }

        [ActianTodo]
        public override Task Store_values_can_be_read_and_set_for_an_object_in_the_Deleted_state()
        {
            return base.Store_values_can_be_read_and_set_for_an_object_in_the_Deleted_state();
        }

        [ActianTodo]
        public override Task Store_values_can_be_read_and_set_for_an_object_in_the_Deleted_state_asynchronously()
        {
            return base.Store_values_can_be_read_and_set_for_an_object_in_the_Deleted_state_asynchronously();
        }

        [ActianTodo]
        public override Task Current_values_can_be_read_and_set_for_an_object_in_the_Unchanged_state()
        {
            return base.Current_values_can_be_read_and_set_for_an_object_in_the_Unchanged_state();
        }

        [ActianTodo]
        public override Task Original_values_can_be_read_and_set_for_an_object_in_the_Unchanged_state()
        {
            return base.Original_values_can_be_read_and_set_for_an_object_in_the_Unchanged_state();
        }

        [ActianTodo]
        public override Task Store_values_can_be_read_and_set_for_an_object_in_the_Unchanged_state()
        {
            return base.Store_values_can_be_read_and_set_for_an_object_in_the_Unchanged_state();
        }

        [ActianTodo]
        public override Task Store_values_can_be_read_and_set_for_an_object_in_the_Unchanged_state_asynchronously()
        {
            return base.Store_values_can_be_read_and_set_for_an_object_in_the_Unchanged_state_asynchronously();
        }

        [ActianTodo]
        public override Task Current_values_can_be_read_and_set_for_an_object_in_the_Modified_state()
        {
            return base.Current_values_can_be_read_and_set_for_an_object_in_the_Modified_state();
        }

        [ActianTodo]
        public override Task Original_values_can_be_read_and_set_for_an_object_in_the_Modified_state()
        {
            return base.Original_values_can_be_read_and_set_for_an_object_in_the_Modified_state();
        }

        [ActianTodo]
        public override Task Store_values_can_be_read_and_set_for_an_object_in_the_Modified_state()
        {
            return base.Store_values_can_be_read_and_set_for_an_object_in_the_Modified_state();
        }

        [ActianTodo]
        public override Task Store_values_can_be_read_and_set_for_an_object_in_the_Modified_state_asynchronously()
        {
            return base.Store_values_can_be_read_and_set_for_an_object_in_the_Modified_state_asynchronously();
        }

        [ActianTodo]
        public override Task Current_values_can_be_read_and_set_for_an_object_in_the_Added_state()
        {
            return base.Current_values_can_be_read_and_set_for_an_object_in_the_Added_state();
        }

        [ActianTodo]
        public override Task Original_values_can_be_read_or_set_for_an_object_in_the_Added_state()
        {
            return base.Original_values_can_be_read_or_set_for_an_object_in_the_Added_state();
        }

        [ActianTodo]
        public override Task Store_values_can_be_read_or_set_for_an_object_in_the_Added_state()
        {
            return base.Store_values_can_be_read_or_set_for_an_object_in_the_Added_state();
        }

        [ActianTodo]
        public override Task Store_values_can_be_read_or_set_for_an_object_in_the_Added_state_asynchronously()
        {
            return base.Store_values_can_be_read_or_set_for_an_object_in_the_Added_state_asynchronously();
        }

        [ActianTodo]
        public override Task Current_values_can_be_read_or_set_for_a_Detached_object()
        {
            return base.Current_values_can_be_read_or_set_for_a_Detached_object();
        }

        [ActianTodo]
        public override Task Original_values_can_be_read_or_set_for_a_Detached_object()
        {
            return base.Original_values_can_be_read_or_set_for_a_Detached_object();
        }

        [ActianTodo]
        public override Task Store_values_can_be_read_or_set_for_a_Detached_object()
        {
            return base.Store_values_can_be_read_or_set_for_a_Detached_object();
        }

        [ActianTodo]
        public override Task Store_values_can_be_read_or_set_for_a_Detached_object_asynchronously()
        {
            return base.Store_values_can_be_read_or_set_for_a_Detached_object_asynchronously();
        }

        [ActianTodo]
        public override void Current_values_can_be_set_from_an_object_using_generic_dictionary()
        {
            base.Current_values_can_be_set_from_an_object_using_generic_dictionary();
        }

        [ActianTodo]
        public override void Original_values_can_be_set_from_an_object_using_generic_dictionary()
        {
            base.Original_values_can_be_set_from_an_object_using_generic_dictionary();
        }

        [ActianTodo]
        public override void Current_values_can_be_set_from_an_object_using_non_generic_dictionary()
        {
            base.Current_values_can_be_set_from_an_object_using_non_generic_dictionary();
        }

        [ActianTodo]
        public override void Original_values_can_be_set_from_an_object_using_non_generic_dictionary()
        {
            base.Original_values_can_be_set_from_an_object_using_non_generic_dictionary();
        }

        [ActianTodo]
        public override void Current_values_can_be_set_from_DTO_object_using_non_generic_dictionary()
        {
            base.Current_values_can_be_set_from_DTO_object_using_non_generic_dictionary();
        }

        [ActianTodo]
        public override void Original_values_can_be_set_from_DTO_object_using_non_generic_dictionary()
        {
            base.Original_values_can_be_set_from_DTO_object_using_non_generic_dictionary();
        }

        [ActianTodo]
        public override void Current_values_can_be_set_from_DTO_object_missing_key_using_non_generic_dictionary()
        {
            base.Current_values_can_be_set_from_DTO_object_missing_key_using_non_generic_dictionary();
        }

        [ActianTodo]
        public override void Original_values_can_be_set_from_DTO_object_missing_key_using_non_generic_dictionary()
        {
            base.Original_values_can_be_set_from_DTO_object_missing_key_using_non_generic_dictionary();
        }

        [ActianTodo]
        public override void Current_values_can_be_set_from_dictionary()
        {
            base.Current_values_can_be_set_from_dictionary();
        }

        [ActianTodo]
        public override void Original_values_can_be_set_from_dictionary()
        {
            base.Original_values_can_be_set_from_dictionary();
        }

        [ActianTodo]
        public override void Current_values_can_be_set_from_dictionary_some_missing()
        {
            base.Current_values_can_be_set_from_dictionary_some_missing();
        }

        [ActianTodo]
        public override void Original_values_can_be_set_from_dictionary_some_missing()
        {
            base.Original_values_can_be_set_from_dictionary_some_missing();
        }

        [ActianTodo]
        public override void Current_values_can_be_set_from_one_generic_dictionary_to_another_generic_dictionary()
        {
            base.Current_values_can_be_set_from_one_generic_dictionary_to_another_generic_dictionary();
        }

        [ActianTodo]
        public override void Original_values_can_be_set_from_one_generic_dictionary_to_another_generic_dictionary()
        {
            base.Original_values_can_be_set_from_one_generic_dictionary_to_another_generic_dictionary();
        }

        [ActianTodo]
        public override void Current_values_can_be_set_from_one_non_generic_dictionary_to_another_generic_dictionary()
        {
            base.Current_values_can_be_set_from_one_non_generic_dictionary_to_another_generic_dictionary();
        }

        [ActianTodo]
        public override void Original_values_can_be_set_from_one_non_generic_dictionary_to_another_generic_dictionary()
        {
            base.Original_values_can_be_set_from_one_non_generic_dictionary_to_another_generic_dictionary();
        }

        [ActianTodo]
        public override void Primary_key_in_current_values_cannot_be_changed_in_property_dictionary()
        {
            base.Primary_key_in_current_values_cannot_be_changed_in_property_dictionary();
        }

        [ActianTodo]
        public override void Primary_key_in_original_values_cannot_be_changed_in_property_dictionary()
        {
            base.Primary_key_in_original_values_cannot_be_changed_in_property_dictionary();
        }

        [ActianTodo]
        public override void Non_nullable_property_in_current_values_results_in_conceptual_null(CascadeTiming deleteOrphansTiming)
        {
            base.Non_nullable_property_in_current_values_results_in_conceptual_null(deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Non_nullable_shadow_property_in_current_values_results_in_conceptual_null(CascadeTiming deleteOrphansTiming)
        {
            base.Non_nullable_shadow_property_in_current_values_results_in_conceptual_null(deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Non_nullable_property_in_original_values_cannot_be_set_to_null_in_property_dictionary()
        {
            base.Non_nullable_property_in_original_values_cannot_be_set_to_null_in_property_dictionary();
        }

        [ActianTodo]
        public override void Non_nullable_shadow_property_in_original_values_cannot_be_set_to_null_in_property_dictionary()
        {
            base.Non_nullable_shadow_property_in_original_values_cannot_be_set_to_null_in_property_dictionary();
        }

        [ActianTodo]
        public override void Non_nullable_property_in_cloned_dictionary_cannot_be_set_to_null()
        {
            base.Non_nullable_property_in_cloned_dictionary_cannot_be_set_to_null();
        }

        [ActianTodo]
        public override void Property_in_current_values_cannot_be_set_to_instance_of_wrong_type()
        {
            base.Property_in_current_values_cannot_be_set_to_instance_of_wrong_type();
        }

        [ActianTodo]
        public override void Property_in_original_values_cannot_be_set_to_instance_of_wrong_type()
        {
            base.Property_in_original_values_cannot_be_set_to_instance_of_wrong_type();
        }

        [ActianTodo]
        public override void Shadow_property_in_current_values_cannot_be_set_to_instance_of_wrong_type()
        {
            base.Shadow_property_in_current_values_cannot_be_set_to_instance_of_wrong_type();
        }

        [ActianTodo]
        public override void Shadow_property_in_original_values_cannot_be_set_to_instance_of_wrong_type()
        {
            base.Shadow_property_in_original_values_cannot_be_set_to_instance_of_wrong_type();
        }

        [ActianTodo]
        public override void Property_in_cloned_dictionary_cannot_be_set_to_instance_of_wrong_type()
        {
            base.Property_in_cloned_dictionary_cannot_be_set_to_instance_of_wrong_type();
        }

        [ActianTodo]
        public override void Primary_key_in_current_values_cannot_be_changed_by_setting_values_from_object()
        {
            base.Primary_key_in_current_values_cannot_be_changed_by_setting_values_from_object();
        }

        [ActianTodo]
        public override void Primary_key_in_original_values_cannot_be_changed_by_setting_values_from_object()
        {
            base.Primary_key_in_original_values_cannot_be_changed_by_setting_values_from_object();
        }

        [ActianTodo]
        public override void Primary_key_in_current_values_cannot_be_changed_by_setting_values_from_another_dictionary()
        {
            base.Primary_key_in_current_values_cannot_be_changed_by_setting_values_from_another_dictionary();
        }

        [ActianTodo]
        public override void Primary_key_in_original_values_cannot_be_changed_by_setting_values_from_another_dictionary()
        {
            base.Primary_key_in_original_values_cannot_be_changed_by_setting_values_from_another_dictionary();
        }

        [ActianTodo]
        public override Task Properties_for_current_values_returns_properties()
        {
            return base.Properties_for_current_values_returns_properties();
        }

        [ActianTodo]
        public override Task Properties_for_original_values_returns_properties()
        {
            return base.Properties_for_original_values_returns_properties();
        }

        [ActianTodo]
        public override Task Properties_for_store_values_returns_properties()
        {
            return base.Properties_for_store_values_returns_properties();
        }

        [ActianTodo]
        public override Task Properties_for_store_values_returns_properties_asynchronously()
        {
            return base.Properties_for_store_values_returns_properties_asynchronously();
        }

        [ActianTodo]
        public override Task Properties_for_cloned_dictionary_returns_properties()
        {
            return base.Properties_for_cloned_dictionary_returns_properties();
        }

        [ActianTodo]
        public override Task GetDatabaseValues_for_entity_not_in_the_store_returns_null()
        {
            return base.GetDatabaseValues_for_entity_not_in_the_store_returns_null();
        }

        [ActianTodo]
        public override Task GetDatabaseValuesAsync_for_entity_not_in_the_store_returns_null()
        {
            return base.GetDatabaseValuesAsync_for_entity_not_in_the_store_returns_null();
        }

        [ActianTodo]
        public override Task NonGeneric_GetDatabaseValues_for_entity_not_in_the_store_returns_null()
        {
            return base.NonGeneric_GetDatabaseValues_for_entity_not_in_the_store_returns_null();
        }

        [ActianTodo]
        public override Task NonGeneric_GetDatabaseValuesAsync_for_entity_not_in_the_store_returns_null()
        {
            return base.NonGeneric_GetDatabaseValuesAsync_for_entity_not_in_the_store_returns_null();
        }

        [ActianTodo]
        public override Task GetDatabaseValues_for_derived_entity_not_in_the_store_returns_null()
        {
            return base.GetDatabaseValues_for_derived_entity_not_in_the_store_returns_null();
        }

        [ActianTodo]
        public override Task GetDatabaseValuesAsync_for_derived_entity_not_in_the_store_returns_null()
        {
            return base.GetDatabaseValuesAsync_for_derived_entity_not_in_the_store_returns_null();
        }

        [ActianTodo]
        public override Task NonGeneric_GetDatabaseValues_for_derived_entity_not_in_the_store_returns_null()
        {
            return base.NonGeneric_GetDatabaseValues_for_derived_entity_not_in_the_store_returns_null();
        }

        [ActianTodo]
        public override Task NonGeneric_GetDatabaseValuesAsync_for_derived_entity_not_in_the_store_returns_null()
        {
            return base.NonGeneric_GetDatabaseValuesAsync_for_derived_entity_not_in_the_store_returns_null();
        }

        [ActianTodo]
        public override Task GetDatabaseValues_for_the_wrong_type_in_the_store_returns_null()
        {
            return base.GetDatabaseValues_for_the_wrong_type_in_the_store_returns_null();
        }

        [ActianTodo]
        public override Task GetDatabaseValuesAsync_for_the_wrong_type_in_the_store_returns_null()
        {
            return base.GetDatabaseValuesAsync_for_the_wrong_type_in_the_store_returns_null();
        }

        [ActianTodo]
        public override Task NonGeneric_GetDatabaseValues_for_the_wrong_type_in_the_store_throws()
        {
            return base.NonGeneric_GetDatabaseValues_for_the_wrong_type_in_the_store_throws();
        }

        [ActianTodo]
        public override Task NonGeneric_GetDatabaseValuesAsync_for_the_wrong_type_in_the_store_throws()
        {
            return base.NonGeneric_GetDatabaseValuesAsync_for_the_wrong_type_in_the_store_throws();
        }

        [ActianTodo]
        public override void Setting_store_values_does_not_change_current_or_original_values()
        {
            base.Setting_store_values_does_not_change_current_or_original_values();
        }

        [ActianTodo]
        [ConditionalTheory]
        [InlineData(EntityState.Unchanged, true)]
        [InlineData(EntityState.Unchanged, false)]
        [InlineData(EntityState.Modified, true)]
        [InlineData(EntityState.Modified, false)]
        [InlineData(EntityState.Added, true)]
        [InlineData(EntityState.Added, false)]
        [InlineData(EntityState.Deleted, true)]
        [InlineData(EntityState.Deleted, false)]
        [InlineData(EntityState.Detached, true)]
        [InlineData(EntityState.Detached, false)]
        public new Task Reload_when_entity_deleted_in_store_can_happen_for_any_state(EntityState state, bool async)
        {
            return base.Reload_when_entity_deleted_in_store_can_happen_for_any_state(state, async);
        }

        [ActianTodo]
        [ConditionalFact]
        public new Task Store_values_really_are_store_values_not_current_or_original_values()
        {
            return base.Store_values_really_are_store_values_not_current_or_original_values();
        }

        [ActianTodo]
        [ConditionalFact]
        public new Task Store_values_really_are_store_values_not_current_or_original_values_async()
        {
            return base.Store_values_really_are_store_values_not_current_or_original_values_async();
        }

        [ActianTodo]
        [ConditionalTheory]
        [InlineData(EntityState.Unchanged, true)]
        [InlineData(EntityState.Unchanged, false)]
        [InlineData(EntityState.Modified, true)]
        [InlineData(EntityState.Modified, false)]
        [InlineData(EntityState.Added, true)]
        [InlineData(EntityState.Added, false)]
        [InlineData(EntityState.Deleted, true)]
        [InlineData(EntityState.Deleted, false)]
        [InlineData(EntityState.Detached, true)]
        [InlineData(EntityState.Detached, false)]
        public new Task Values_can_be_reloaded_from_database_for_entity_in_any_state(EntityState state, bool async)
        {
            return base.Values_can_be_reloaded_from_database_for_entity_in_any_state(state, async);
        }


        public class PropertyValuesActianFixture : PropertyValuesFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => (ITestStoreFactory)ActianTestStoreFactory.Instance;

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                base.OnModelCreating(modelBuilder, context);

                modelBuilder.Entity<Building>()
                    .Property(b => b.Value).HasColumnType("decimal(18,2)");

                modelBuilder.Entity<Office>()
                    .Property(b => b.Number).HasMaxLength(20);

                modelBuilder.Entity<CurrentEmployee>()
                    .Property(ce => ce.LeaveBalance).HasColumnType("decimal(18,2)");
            }
        }
    }
}
