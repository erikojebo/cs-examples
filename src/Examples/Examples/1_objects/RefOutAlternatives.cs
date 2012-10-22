using NUnit.Framework;

namespace Examples
{
    [TestFixture]
    public class RefOutAlternatives
    {
        [Test]
        public void Out_is_used_when_parsing_values_in_the_base_class_library_to_handle_multiple_return_values()
        {
            // The output of the function is both the returned bool and the potentially parsed value
            int a;
            var wasSuccessful = int.TryParse("123", out a);

            Assert.IsTrue(wasSuccessful);
            Assert.AreEqual(123, a);
        }

        [Test]
        public void Nullable_types_could_be_used_as_an_alternative_to_out_instead_of_a_bool_and_a_result()
        {
            var r1 = TryParse("a");
            var r2 = TryParse("123");

            Assert.IsFalse(r1.HasValue);
            Assert.IsTrue(r2.HasValue);
            Assert.AreEqual(123, r2.Value);
        }

        [Test]
        public void A_wrapper_class_can_be_created_to_contain_any_number_of_return_values()
        {
            var result = TryParseResultClass("123");

            Assert.IsTrue(result.WasSuccessful);
            Assert.AreEqual(123, result.Value);
            Assert.AreEqual("123", result.ParsedString);
        }

        public int? TryParse(string input)
        {
            int result;
            var wasSuccessful = int.TryParse(input, out result);

            if (wasSuccessful)
                return result;

            return new int?();
        }

        public ParseResult TryParseResultClass(string input)
        {
            int value;

            var wasSuccessful = int.TryParse(input, out value);

            return new ParseResult
                       {
                           WasSuccessful = wasSuccessful,
                           Value = value,
                           ParsedString = input
                       };
        }
    }

    public class ParseResult
    {
        public int Value { get; set; }
        public bool WasSuccessful { get; set; }
        public string ParsedString { get; set; }
    }
}