using System.Text;
using NUnit.Framework;

namespace Examples._2_refandvalue
{
    [TestFixture]
    public class ReferenceAndValueTypesExamples
    {
        [Test]
        public void Ref_Type_Can_Be_Assigned_To_Other_Instance_Are_Same()
        {
            var sbOne = new StringBuilder();
            sbOne.Append("We're doing a Studiecirkel");

            var sbTwo = sbOne;

            Assert.AreSame(sbOne, sbTwo);
        }

        [Test]
        public void Changing_One_Reference_Type_Referring_To_Same_Ref_Changes_The_Value_In_Second()
        {
            var sbOne = new StringBuilder("Still doing the studiecirkel");

            var sbTwo = sbOne;

            sbOne.Append("Done soon?");

            Assert.AreSame(sbOne, sbTwo);
        }

        [Test]
        public void Re_Assigning_First_Does_Not_Affect_Second()
        {
            var sbOne = new StringBuilder("Still doing the studiecirkel");

            var sbTwo = sbOne;

            sbOne = new StringBuilder();

            Assert.AreNotSame(sbOne, sbTwo);
        }

        [Test] public void Value_Types_Only_Copy_Data_At_A_Specific_Moment()
        {
            int a = 6;

            int b = a;

            a = 8;

            Assert.AreEqual(6, b);
        }

        //What about Strings????
        [Test]
        public void Strings_Are_Reference_Types_But_Are_Immutable_And_Sometimes_Acts_Like_Value_Types_But_Not_When_Assigning()
        {
            string str = "Hello World";

            var str2 = str;

            Assert.AreSame(str, str2);
        }

        [Test]
        public void String_Is_Not_Modified_Since_It_Is_Immutable_And_Acts_Like_A_Value_type()
        {
            string str = "Hello World";

            var str2 = str;

            str += " Again";

            //str2 is still just "Hello World"
            Assert.AreNotSame(str, str2);
        }
    }
}
