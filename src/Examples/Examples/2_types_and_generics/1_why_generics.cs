using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using NUnit.Framework;

namespace Examples
{
    [TestFixture]
    public class why_generics
    {
        [Test]
        public void Dot_NET_1_collections_were_untyped()
        {
            ArrayList list = new ArrayList();

            list.Add(1);
            list.Add("string");

            int firstValue = (int)list[0];
            string secondValue = (string)list[1];

            Assert.AreEqual(1, firstValue);
            Assert.AreEqual("string", secondValue);
        }

        [Test]
        public void Because_of_this_there_were_specialized_collections()
        {
            // StringCollection is a strongly typed collection for strings,
            // but duplication would be needed to create one for int, double, float etc.

            StringCollection strings = new StringCollection();

            strings.Add("string 1");
            strings.Add("string 2");

            string firstValue = strings[0];
            string secondValue = strings[1];

            Assert.AreEqual("string 1", firstValue);
            Assert.AreEqual("string 2", secondValue);
        }

        [Test]
        public void Collections_are_much_nicer_with_generics()
        {
            List<string> strings = new List<string>();

            strings.Add("string 1");
            strings.Add("string 2");

            string firstValue = strings[0];
            string secondValue = strings[1];

            Assert.AreEqual("string 1", firstValue);
            Assert.AreEqual("string 2", secondValue);
        }
    }
}