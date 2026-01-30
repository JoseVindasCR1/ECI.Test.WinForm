using System.Collections.Generic;
using System.Linq;

namespace ECI.Test.BL.Validation
{
    public class ValidationResult
    {
        public bool IsValid => !Errors.Any();
        public List<string> Errors { get; set; } = new List<string>();

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public override string ToString()
        {
            return string.Join("; ", Errors);
        }
    }

    public interface IValidator<T>
    {
        ValidationResult Validate(T instance);
    }

    public abstract class AbstractValidator<T> : IValidator<T>
    {
        protected List<IValidationRule<T>> Rules = new List<IValidationRule<T>>();

        public ValidationResult Validate(T instance)
        {
            var result = new ValidationResult();
            
            foreach (var rule in Rules)
            {
                var ruleResult = rule.Validate(instance);
                if (!ruleResult.IsValid)
                {
                    result.Errors.AddRange(ruleResult.Errors);
                }
            }

            return result;
        }

        protected IRuleBuilder<T, TProperty> RuleFor<TProperty>(System.Func<T, TProperty> propertySelector)
        {
            return new RuleBuilder<T, TProperty>(propertySelector, Rules);
        }
    }

    public interface IValidationRule<T>
    {
        ValidationResult Validate(T instance);
    }

    public interface IRuleBuilder<T, TProperty>
    {
        IRuleBuilder<T, TProperty> NotEmpty();
        IRuleBuilder<T, TProperty> MaximumLength(int maxLength);
        IRuleBuilder<T, TProperty> GreaterThanOrEqualTo(TProperty value);
        IRuleBuilder<T, TProperty> WithMessage(string message);
        IRuleBuilder<T, TProperty> When(System.Func<T, bool> predicate);
    }

    public class RuleBuilder<T, TProperty> : IRuleBuilder<T, TProperty>
    {
        private readonly System.Func<T, TProperty> _propertySelector;
        private readonly List<IValidationRule<T>> _rules;
        private ValidationRule<T, TProperty> _currentRule;

        public RuleBuilder(System.Func<T, TProperty> propertySelector, List<IValidationRule<T>> rules)
        {
            _propertySelector = propertySelector;
            _rules = rules;
        }

        public IRuleBuilder<T, TProperty> NotEmpty()
        {
            _currentRule = new ValidationRule<T, TProperty>(_propertySelector, 
                value => !IsEmpty(value), "Field is required.");
            _rules.Add(_currentRule);
            return this;
        }

        public IRuleBuilder<T, TProperty> MaximumLength(int maxLength)
        {
            _currentRule = new ValidationRule<T, TProperty>(_propertySelector,
                value => value?.ToString()?.Length <= maxLength || value == null, 
                $"Field cannot exceed {maxLength} characters.");
            _rules.Add(_currentRule);
            return this;
        }

        public IRuleBuilder<T, TProperty> GreaterThanOrEqualTo(TProperty value)
        {
            _currentRule = new ValidationRule<T, TProperty>(_propertySelector,
                prop => Comparer<TProperty>.Default.Compare(prop, value) >= 0,
                $"Field must be greater than or equal to {value}.");
            _rules.Add(_currentRule);
            return this;
        }

        public IRuleBuilder<T, TProperty> WithMessage(string message)
        {
            if (_currentRule != null)
            {
                _currentRule.ErrorMessage = message;
            }
            return this;
        }

        public IRuleBuilder<T, TProperty> When(System.Func<T, bool> predicate)
        {
            if (_currentRule != null)
            {
                _currentRule.Condition = predicate;
            }
            return this;
        }

        private static bool IsEmpty(TProperty value)
        {
            if (value == null) return true;
            if (value is string str) return string.IsNullOrWhiteSpace(str);
            return false;
        }
    }

    public class ValidationRule<T, TProperty> : IValidationRule<T>
    {
        private readonly System.Func<T, TProperty> _propertySelector;
        private readonly System.Func<TProperty, bool> _validationFunc;
        
        public string ErrorMessage { get; set; }
        public System.Func<T, bool> Condition { get; set; }

        public ValidationRule(System.Func<T, TProperty> propertySelector, 
            System.Func<TProperty, bool> validationFunc, string errorMessage)
        {
            _propertySelector = propertySelector;
            _validationFunc = validationFunc;
            ErrorMessage = errorMessage;
        }

        public ValidationResult Validate(T instance)
        {
            var result = new ValidationResult();

            if (Condition != null && !Condition(instance))
            {
                return result;
            }

            var propertyValue = _propertySelector(instance);
            if (!_validationFunc(propertyValue))
            {
                result.AddError(ErrorMessage);
            }

            return result;
        }
    }
}