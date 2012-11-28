using NUnit.Framework;

namespace Examples
{
    [TestFixture]
    public class _2_ClassStructRefOutExamples
    {
        [Test]
        public void Structs_are_passed_by_value()
        {
            var s = new SomeStruct(1, 2);

            Modify(s);

            Assert.AreEqual(1, s.X);
            Assert.AreEqual(2, s.Y);
        }

        [Test]
        public void Classes_are_passed_by_reference()
        {
            var s = new SomeClass(1, 2);

            Modify(s);

            Assert.AreEqual(123, s.X);
            Assert.AreEqual(2, s.Y);
        }

        [Test]
        public void Out_can_be_used_to_pass_something_by_reference_instead_of_by_value()
        {
            SomeStruct s;

            Modify(out s);

            Assert.AreEqual(123, s.X);
            Assert.AreEqual(2, s.Y);
        }
        
        [Test]
        public void Ref_is_the_same_as_out_but_requires_that_the_object_has_been_initialized_and_does_not_have_to_assign_to_the_reference()
        {
            var s = new SomeStruct(1, 2);

            // This would not be allowed:
            // SomeStruct s;

            ModifyWithRefKeyword(ref s);

            Assert.AreEqual(123, s.X);
            Assert.AreEqual(2, s.Y);
        }

        [Test]
        public void Passing_a_reference_type_as_ref_allows_the_method_to_change_the_actual_reference_and_not_just_the_referenced_object()
        {
            var s1 = new SomeClass(1, 2);
            var s2 = s1;

            Assert.AreSame(s1, s2);

            // DON'T DO THIS!
            ReferenceAnotherObject(ref s1);

            Assert.AreNotSame(s1, s2);
        }

        private void ReferenceAnotherObject(ref SomeClass s)
        {
            s = new SomeClass(4, 5);
        }

        private void Modify(out SomeStruct s)
        {
            // If s is not assigned a new value here the compiler complains,
            // and no values can be read from s since it might not be initialized

            // This is not allowed:
            // var oldX = s.X

            s = new SomeStruct(123, 2);
        }

        // Cant be called Modify, since you can't have one overload with out and one with ref
        private void ModifyWithRefKeyword(ref SomeStruct s)
        {
            // These changes do actually affect the original value
            s.X = 123;
        }

        private void Modify(SomeStruct s)
        {
            // These changes do not affect the original value
            s.X = 123;
        }

        private void Modify(SomeClass s)
        {
            // These changes do actually affect the original value
            s.X = 123;
        }
    }
}