using System;
using System.Collections;
using System.Collections.Generic;
using Examples._2_types_and_generics;
using NUnit.Framework;
using System.Linq;

namespace Examples._5_linq
{
    public class _1_linq
    {
        [Test]
        [ExpectedException]
        public void First_throw_an_exception_if_the_collection_is_empty()
        {
            var items = new List<int>();
            var firstItem = items.First();
        }
        
        [Test]
        public void First_or_default_returns_the_default_value_of_the_type_if_the_collection_is_empty()
        {
            var strings = new List<string>();
            var firstString = strings.FirstOrDefault();

            var ints = new List<int>();
            var firstInt = ints.FirstOrDefault();

            Assert.AreEqual(0, firstInt);
            Assert.AreEqual(default(int), firstInt);

            Assert.IsNull(firstString);
            Assert.AreEqual(default(string), firstString);
        }

        [Test]
        [ExpectedException]
        public void Single_is_like_first_but_throws_an_exception_if_there_are_more_than_one_item_in_the_collection()
        {
            var items = new List<int> { 1, 2 };
            var firstItem = items.Single();
        }

        [Test]
        public void Count_queries_can_be_used_to_get_the_number_of_items_in_any_IEnumerable_collection()
        {
            var intArray = new[] { 1, 2, 1, 3, 1 };
            var intList = new List<int> { 1, 2, 1, 3 };

            // Equivalent to intArray.Length, since this is an array.
            var arrayItemCount = intArray.Count();

            // Equivalent to intList.Count, since this is a List (which implements the interface ICollection).
            var listItemCount = intList.Count();

            Assert.AreEqual(5, arrayItemCount);
            Assert.AreEqual(4, listItemCount);
        }

        [Test]
        public void Count_queries_can_be_used_to_count_the_number_of_items_matching_a_predicate()
        {
            var ints = new[] { 1, 2, 1, 3, 1 };

            var onesCount = CountOnes(ints);

            Assert.AreEqual(3, onesCount);
        }

        private int CountOnes(int[] ints)
        {
            var count = 0;

            foreach (var value in ints)
            {
                if (value == 1)
                    count++;
            }

            return count;
        }

        [Test]
        public void Where_queries_are_used_to_filter_a_collection_according_to_a_predicate()
        {
            var people = new[]
                {
                    new Person { Id = 1, Name = "Kalle" },
                    new Person { Id = 2, Name = "Pelle" },
                    new Person { Id = 3, Name = "Stina" },
                    new Person { Id = 4, Name = "Lisa" },
                    new Person { Id = 5, Name = "Kalle" }
                };

            var kalles = GetPeopleCalledKalle(people);

            Assert.AreEqual(2, kalles.Count());
        }

        private static IEnumerable<Person> GetPeopleCalledKalle(IEnumerable<Person> people)
        {
            var result = new List<Person>();

            foreach (var person in people)
            {
                if (person.Name == "Kalle")
                    result.Add(person);
            }

            return result;
        }

        [Test]
        public void Methods_can_be_used_just_as_well_as_lambda_expressions_in_linq_queries_as_long_as_the_signature_matches()
        {
            var people = new[]
                {
                    new Person { Id = 1, Name = "Kalle" },
                    new Person { Id = 2, Name = "Pelle" },
                    new Person { Id = 3, Name = "Stina" },
                    new Person { Id = 4, Name = "Lisa" },
                    new Person { Id = 5, Name = "Kalle" }
                };

            var kalles = people.Where(IsCalledKalle);

            Assert.AreEqual(2, kalles.Count());
        }

        private bool IsCalledKalle(Person person)
        {
            return person.Name == "Kalle";
        }

        [Test]
        public void Select_is_used_to_transform_a_collection()
        {
            var people = new[]
                {
                    new Person { Id = 1, Name = "Kalle" },
                    new Person { Id = 2, Name = "Pelle" },
                    new Person { Id = 3, Name = "Stina" },
                    new Person { Id = 4, Name = "Lisa" },
                    new Person { Id = 5, Name = "Kalle" }
                };

            var names = people.Select(x => x.Name.ToUpper());

            Assert.AreEqual(5, names.Count());
            Assert.AreEqual("KALLE", names.First());
        }

        [Test]
        public void Linq_has_orderby_queries_to_sort_the_result()
        {
            var people = new[]
                {
                    new Person { Id = 1, Name = "Kalle" },
                    new Person { Id = 2, Name = "Pelle" },
                    new Person { Id = 3, Name = "Stina" },
                    new Person { Id = 4, Name = "Lisa" },
                    new Person { Id = 5, Name = "Kalle" }
                };

            var ascendingPeople = people.OrderBy(x => x.Name);
            var descendingPeople = people.OrderByDescending(x => x.Name);

            Assert.AreEqual("Kalle", ascendingPeople.First().Name);
            Assert.AreEqual("Stina", descendingPeople.First().Name);

            Assert.AreEqual("Stina", ascendingPeople.Last().Name);
            Assert.AreEqual("Kalle", descendingPeople.Last().Name);
        }

        [Test]
        public void Multiple_different_linq_methods_can_be_chained_together_to_create_a_more_complex_query()
        {
            var people = new[]
                {
                    new Person { Id = 1, Name = "Kalle" },
                    new Person { Id = 2, Name = "Pelle" },
                    new Person { Id = 3, Name = "Stina" },
                    new Person { Id = 4, Name = "Lisa" },
                    new Person { Id = 5, Name = "Kalle" }
                };

            var lastNameInUpperCase = people
                .OrderBy(x => x.Name)
                .Select(x => x.Name.ToUpper())
                .Last();

            Assert.AreEqual("STINA", lastNameInUpperCase);
        }
    }

    
}