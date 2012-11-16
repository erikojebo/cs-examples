using System.Collections.Generic;
using NUnit.Framework;

namespace Examples._2_types_and_generics
{
    [TestFixture]
    public class _2_generic_validation_rules
    {
        [Test]
        public void Here_is_an_example_of_applying_validation_rules_to_a_bunch_of_validation_rules_for_integers()
        {
            var rules = new IntegerValidationRuleCollection();

            rules.Add(new PositiveIntegerValidationRule());
            rules.Add(new EvenIntegerValidationRule());

            Assert.IsTrue(rules.IsValid(2));
            Assert.IsTrue(rules.IsValid(4));
            Assert.IsFalse(rules.IsValid(-1));
            Assert.IsFalse(rules.IsValid(3));

            // How do we avoid copy-pasting the IntegerValidationRuleCollection class
            // if we need to validate doubles or floats or something else?
        }
    }

    public class IntegerValidationRuleCollection
    {
        private readonly IList<IIntegerValidationRule> _rules = new List<IIntegerValidationRule>();

        public void Add(IIntegerValidationRule rule)
        {
            _rules.Add(rule);
        }

        public bool IsValid(int value)
        {
            foreach (var rule in _rules)
            {
                if (!rule.IsValid(value))
                    return false;
            }

            return true;
        }
    }

    public interface IIntegerValidationRule
    {
        bool IsValid(int value);
    }

    public class EvenIntegerValidationRule : IIntegerValidationRule
    {
        public bool IsValid(int value)
        {
            return value % 2 == 0;
        }
    }

    public class PositiveIntegerValidationRule : IIntegerValidationRule
    {
        public bool IsValid(int value)
        {
            return value > 0;
        }
    }
}