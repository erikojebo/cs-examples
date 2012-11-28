using System;
using System.Collections.Generic;
using Examples.HashTable;
using NUnit.Framework;
using System.Linq;

namespace Examples
{
    [TestFixture]
    public class _5_EqualsExamples
    {
        // Equals
        // GetHashCode
        // Equals for class vs struct
        // ReferenceEquals
        // object.Equals(a,b)
        // EqualityComparer / EqualityComparer<T>
        // ToString

        [Test]
        public void An_object_equals_itself()
        {
            var o = new object();

            Assert.AreEqual(o, o);
            Assert.IsTrue(o.Equals(o));
        }

        [Test]
        public void An_object_does_not_equal_another_object_by_default()
        {
            var o1 = new object();
            var o2 = new object();

            Assert.AreNotEqual(o1, o2);
            Assert.IsFalse(o1.Equals(o2));
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void The_instance_Equals_method_cannot_be_used_on_a_null_object()
        {
            object o1 = null;
            object o2 = new object();

            // Null reference exception here
            o1.Equals(o2);
        }

        [Test]
        public void The_static_equals_method_is_better_suited_for_coping_with_null_values()
        {
            object o1 = null;
            var o2 = new object();

            Assert.IsTrue(Equals(o1, o1));
            Assert.IsFalse(Equals(o1, o2));
        }

        [Test]
        public void Structs_with_same_values_are_equal_by_default()
        {
            var a = new SomeStruct(1, 2);
            var b = new SomeStruct(1, 2);

            Assert.AreEqual(a, b);
        }

        [Test]
        public void Structs_with_different_values_are_not_equal_by_default()
        {
            var a = new SomeStruct(1, 2);
            var b = new SomeStruct(1, 3);

            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void Equals_can_be_overridden()
        {
            var a = new CustomEqualsObject(1);
            var b = new CustomEqualsObject(1);
            var c = new CustomEqualsObject(2);

            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
        }

        [Ignore]
        [Test]
        public void Things_get_funky_if_you_dont_override_GetHashCode_when_overriding_Equals()
        {
            var table = new SimplifiedHashTable<ObjectWithCustomEqualsButDefaultHashCode, string>(9);

            // These two objects are equal, but have different hash codes
            var a = new ObjectWithCustomEqualsButDefaultHashCode("abc");
            var b = new ObjectWithCustomEqualsButDefaultHashCode("abc");

            table[a] = "original value";
            table[b] = "modified value";

            Assert.AreEqual("modified value", table[a]);
        }

        [Test]
        public void Equals_can_be_overriden_for_structs_as_well()
        {
            var a = new StructWithWeirdEquals(1, 2);
            var b = new StructWithWeirdEquals(1, 3);

            Assert.AreEqual(a, b);
        }

        [Test]
        public void Structs_with_same_values_have_the_same_hash_code_by_default()
        {
            var a = new SomeStruct(1, 2);
            var b = new SomeStruct(1, 2);

            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void Equality_operator_still_works_the_same_when_overriding_equals()
        {
            var a = new CustomEqualsObject(1);
            var b = new CustomEqualsObject(1);

            Assert.IsFalse(a == b);
        }

        [Test]
        public void Equality_operator_can_be_overridden()
        {
            var a = new CustomEqualsOperatorObject(1);
            var b = new CustomEqualsOperatorObject(1);
            var c = new CustomEqualsOperatorObject(2);

            Assert.IsTrue(a == a);
            Assert.IsTrue(a == b);
            Assert.IsTrue(a != c);
            Assert.IsFalse(a == c);
            Assert.IsFalse(a != b);
        }

        [Test]
        public void ReferenceEqual_works_as_expected_even_though_both_Equal_and_equals_operator_are_overridden()
        {
            var a = new CustomEqualsOperatorObject(1);
            var b = new CustomEqualsOperatorObject(1);

            Assert.IsFalse(ReferenceEquals(a, b));
        }

        [Test]
        public void Reference_equals_for_value_types_is_very_strange_and_should_be_avoided()
        {
            int a = 1;
            int b = 1;

            // ReferenceEquals boxes a as the first parameter and a as the second parameter,
            // which makes the boxes different
            Assert.IsFalse(ReferenceEquals(a, a));
            Assert.IsFalse(ReferenceEquals(a, b));
        }

        [Test]
        public void Custom_equality_operator_is_not_called_through_reference_with_base_type()
        {
            object a = new CustomEqualsOperatorObject(1);
            object b = new CustomEqualsOperatorObject(1);

            Assert.IsFalse(a == b);
        }

        [Test]
        public void Equals_implementation_can_be_done_through_an_equality_comparer_instead_of_overriding_Equals()
        {
            var a = new ObjectWithoutEqualsOverride(1);
            var b = new ObjectWithoutEqualsOverride(1);
            var c = new ObjectWithoutEqualsOverride(2);

            var comparer = new ObjectWithoutEqualsOverrideComparer();

            Assert.IsTrue(comparer.Equals(a, a));
            Assert.IsTrue(comparer.Equals(a, b));
            Assert.IsFalse(comparer.Equals(a, c));
        }

        [Test]
        public void Equality_comparers_are_useful_when_using_certain_apis_such_as_linq()
        {
            var items = new[]
                            {
                                new ObjectWithoutEqualsOverride(1),
                                new ObjectWithoutEqualsOverride(1),
                                new ObjectWithoutEqualsOverride(2)
                            };

            var comparer = new ObjectWithoutEqualsOverrideComparer();

            var distinctItems = items.Distinct(comparer);

            Assert.AreEqual(2, distinctItems.Count());
        }

        [Test]
        public void ToString_can_be_overridden()
        {
            var o = new CustomToStringObject("abc");

            Assert.AreEqual("Value: abc", o.ToString());
        }

        [Test]
        public void Overriding_ToString_can_be_quite_useful_when_debugging()
        {
            var itemsWithDefaultToString = new List<CustomEqualsObject>
                                               {
                                                   new CustomEqualsObject(1),
                                                   new CustomEqualsObject(2),
                                                   new CustomEqualsObject(3),
                                                   new CustomEqualsObject(4),
                                                   new CustomEqualsObject(5)
                                               };

            var itemsWithNiceToString = new List<CustomToStringObject>
                                            {
                                                new CustomToStringObject("1"),
                                                new CustomToStringObject("2"),
                                                new CustomToStringObject("3"),
                                                new CustomToStringObject("4"),
                                                new CustomToStringObject("5")
                                            };
        }
    }
}