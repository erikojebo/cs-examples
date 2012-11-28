using System;
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
            var rules = new ValidationRuleCollection<double>();

            rules.Add(new PositiveValidationRule());
            rules.Add(new EvenValidationRule());

            Assert.IsTrue(rules.IsValid(2));
            Assert.IsTrue(rules.IsValid(4));
            Assert.IsFalse(rules.IsValid(-1));
            Assert.IsFalse(rules.IsValid(3));

            // How do we avoid copy-pasting the IntegerValidationRuleCollection class
            // if we need to validate doubles or floats or something else?
        }
    }

    public class ValidationRuleCollection<T>
    {
        private readonly List<IValidationRule<T>> _rules = new List<IValidationRule<T>>();

        public void Add(IValidationRule<T> rule)
        {
            _rules.Add(rule);
        }

        public bool IsValid(T value)
        {
            foreach (var rule in _rules)
            {
                if (!rule.IsValid(value))
                    return false;
            }

            return true;
        }
    }

    public interface IValidationRule<T>
    {
        bool IsValid(T value);
    }

    public class EvenValidationRule : IValidationRule<double>
    {
        public bool IsValid(double value)
        {
            return (int)value % 2 == 0;
        }
    }

    public class PositiveValidationRule : IValidationRule<double>
    {
        public bool IsValid(double value)
        {
            return value > 0;
        }
    }
}