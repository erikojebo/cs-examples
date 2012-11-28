using System.Collections.Generic;
using NUnit.Framework;

namespace Examples.HashTable
{
    [TestFixture]
    public class HashTableTests
    {
        private SimplifiedHashTable<HashObject, string> _table;

        [SetUp]
        public void SetUp()
        {
            _table = new SimplifiedHashTable<HashObject, string>(9);
        }

        [Test]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Looking_up_value_not_in_table_throws_KeyNotFoundException()
        {
           var value = _table[new HashObject("abc")];
        }
        
        [Test]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Looking_up_value_with_other_value_with_same_hash_but_no_matching_key_throws_KeyNotFoundException()
        {
            _table[new HashObject("aaa")] = "aaa";

            var value = _table[new HashObject("abc")];
        }

        [Test]
        public void Looking_up_value_in_table_with_single_matching_value_returns_corresponding_value()
        {
            _table[new HashObject("abc")] = "abc";

            var actualValue =_table[new HashObject("abc")];

            Assert.AreEqual("abc", actualValue);
        }
        
        [Test]
        public void Looking_up_value_in_table_with_hash_conflict_returns_corresponding_value()
        {
            _table[new HashObject("aaa")] = "aaa";
            _table[new HashObject("abc")] = "abc";
            _table[new HashObject("acc")] = "acc";

            var actualValue =_table[new HashObject("abc")];

            Assert.AreEqual("abc", actualValue);
        }

        [Test]
        public void Looking_up_value_in_table_with_values_with_different_hashes_returns_corresponding_value()
        {
            _table[new HashObject("aaa")] = "aaa";
            _table[new HashObject("bbb")] = "bbb";
            _table[new HashObject("ccc")] = "ccc";

            var actualValue = _table[new HashObject("bbb")];

            Assert.AreEqual("bbb", actualValue);
        }

        [Test]
        public void Setting_value_for_key_that_already_exists_changes_the_value_for_that_key()
        {
            _table[new HashObject("abc")] = "aaa";
            _table[new HashObject("abc")] = "abc";

            var actualValue = _table[new HashObject("abc")];

            Assert.AreEqual("abc", actualValue);
        }

        private class HashObject
        {
            private readonly string _value;

            public HashObject(string value)
            {
                _value = value;
            }

            public override bool Equals(object obj)
            {
                var other = obj as HashObject;

                if (other == null)
                    return false;

                return _value == other._value;
            }

            // Use the first char as the hash code to easily create hash conflicts
            public override int GetHashCode()
            {
                return _value[0];
            }
        }
    }
}