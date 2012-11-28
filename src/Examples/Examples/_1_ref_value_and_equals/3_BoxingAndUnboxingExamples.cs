using System;
using NUnit.Framework;

namespace Examples
{
    [TestFixture]
    public class _3_BoxingAndUnboxingExamples
    {
        [Test]
        public void Boxing_Creates_A_New_Object_But_Doesnt_referr_To_The_Old_Value_Type_By_Ref()
        {
            int a = 10;

            //Boxing
            object o = a;

            Assert.AreNotSame(a, o);
        }

        [Test]
        public void Boxing_Creates_A_New_Object_But_Doesnt_referr_To_The_Old_Value_Type_By_Ref_Part_2()
        {
            int a = 10;

            //Boxing
            object o = a;

            a = 12;

            Assert.AreEqual(12, a);
            Assert.AreEqual(10, o);
            Assert.AreNotSame(a, o);
        }

        [Test]
        [ExpectedException(typeof(InvalidCastException))]
        public void Invalid_Unboxing_Throws_Invalid_Cast_Exception()
        {
            long a = 12;

            object o = a;

            var i = (int) o;
        }
    }
}
