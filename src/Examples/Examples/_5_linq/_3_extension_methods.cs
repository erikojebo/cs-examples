using System;
using NUnit.Framework;

namespace Examples._5_linq
{
    public class _3_extension_methods
    {
        [Test]
        public void Simple_helper_methods_often_end_up_in_a_static_helper_class_somewhere()
        {
            var contentWithoutWhitespace = StringHelper.StripWhitespace("\n\t content\r\n");

            Assert.AreEqual("content", contentWithoutWhitespace);
        }

        [Test]
        public void The_same_code_could_be_written_as_an_extension_method_to_simplify_the_syntax()
        {
            var contentWithoutWhitespace = "\n\t content\r\n".StripWhitespace();

            Assert.AreEqual("content", contentWithoutWhitespace);
        }
    }

    public class StringHelper
    {
         public static string StripWhitespace(string input)
         {
             return input
                 .Replace(" ", "")
                 .Replace("\t", "")
                 .Replace("\n", "")
                 .Replace("\r", "");
         }
    }

    public static class StringExtensions
    {
        public static string StripWhitespace(this string input)
        {
            return input
                 .Replace(" ", "")
                 .Replace("\t", "")
                 .Replace("\n", "")
                 .Replace("\r", "");
        }
    }
}