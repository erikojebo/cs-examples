using System.Collections.Generic;

namespace Examples
{
    public interface IValidationRule<T>
    {
        bool IsValid(T value);
    }

    public class IntValidationRuleCollection
    {
        private IList<IIntValidationRule> _rules = new List<IIntValidationRule>();

        public void Add(IIntValidationRule rule)
        {
            _rules.Add(rule);
        }

        public bool IsValid(int value)
        {
            foreach (var intValidationRule in _rules)
            {
                if (!intValidationRule.IsValid(value))
                    return false;
            }

            return true;
        }
    }

    public interface IIntValidationRule
    {
        bool IsValid(int value);
    }

    public class ValidationRuleCollection<T> where T : IValidationRule<int>
    {
         
    }

    public class EvenIntValidationRule : IValidationRule<int>
    {
        public bool IsValid(int value)
        {
            return value % 2 == 0;
        }
    }

    public class PositiveIntegerValidationRule : IValidationRule<int>
    {
        public bool IsValid(int value)
        {
            return value > 0;
        }
    }
}