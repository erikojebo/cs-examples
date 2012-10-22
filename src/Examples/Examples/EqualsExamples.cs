using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Examples
{
    [TestFixture]
    public class EqualsExamples
    {
        // Equals
        // GetHasCode
        // ReferenceEquals
        // object.Equals(a,b)
        // EqualityComparer / EqualityComparer<T>
        // GetType
        // ToString
        // MemberwiseClone

        [Test]
        public void An_object_equals_itself()
        {
            var expected = new object();
            Assert.AreEqual(expected, expected);
        }

        [Test]
        public void An_object_does_not_equal_another_object()
        {
            var o1 = new object();
            var o2 = new object();

            Assert.AreNotEqual(o1, o2);
        }

        [Test]
        public void Object_are_equal_if_equal_according_to_overriden_equals_definition()
        {
            var a = new CustomEqualsObject(1);
            var b = new CustomEqualsObject(1);
            
            Assert.AreEqual(a, b);
        }

        [Test]
        public void Object_are_not_equal_if_not_equal_according_to_overriden_equals_definition()
        {
            var a = new CustomEqualsObject(1);
            var b = new CustomEqualsObject(2);
            
            Assert.AreNotEqual(a, b);
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
            
        }

    }

    public class CustomEqualsObject
    {
        public int I { get; set; }

        public CustomEqualsObject(int i)
        {
            I = i;
        }

        public override bool Equals(object obj)
        {
            return I == ((CustomEqualsObject)obj).I;
        }

        public static bool operator==(CustomEqualsObject a, CustomEqualsObject b)
        {
            return a.Equals(b);
        }
        
        public static bool operator!=(CustomEqualsObject a, CustomEqualsObject b)
        {
            return !(a == b);
        }
    }
}
