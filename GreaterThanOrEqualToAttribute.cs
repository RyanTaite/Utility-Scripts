using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BusinessApp.Model.CustomDataAnnotations
{
    /// <summary>
    /// Check if this value is greater than or equal to another property
    /// </summary>
    /// <example>
    /// "This property should be greater than or equal to (another property). If it is not, return error message.
    /// <code>
    /// public int AnotherProperty { get; set; }
    ///
    /// [GreaterThanOrEqualTo[nameof(AnotherProperty), ErrorMessage = "This value should be greater than or equal to AnotherProperty"]
    /// public int TargetProperty { get; set; }
    /// </code>
    /// </example>
    public class GreaterThanOrEqualToAttribute : ValidationAttribute
    {
        private readonly string[] _propertyNamesToCompareAgainst;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="propertyNameToCompareAgainst">The name of the property we want to compare against</param>
        public GreaterThanOrEqualToAttribute(string propertyNameToCompareAgainst)
        {
            _propertyNamesToCompareAgainst = new string[] { propertyNameToCompareAgainst };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="propertyNamesToCompareAgainst">The names of the property we want to compare against</param>
        public GreaterThanOrEqualToAttribute(string[] propertyNamesToCompareAgainst)
        {
            _propertyNamesToCompareAgainst = propertyNamesToCompareAgainst;
        }

        /// <summary>
        /// Checks if we are valid
        /// </summary>
        /// <param name="value">The property value that has the data annotation</param>
        /// <param name="validationContext">The validation context being used</param>
        /// <returns>A <see cref="ValidationResult"/> indicating if it is a success or not. If not, contains error message.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // The total amount we want to be greater than or equal too
            var total = 0;

            foreach(var propertyNameToCompareAgainst in _propertyNamesToCompareAgainst)
            {
                // Get the property info with it's name.
                var propertyInfo = validationContext.ObjectType.GetProperty(propertyNameToCompareAgainst);

                // If we can't find that property return an error saying so
                if (propertyInfo == null)
                {
                    return new ValidationResult(string.Format(CultureInfo.CurrentCulture, $"Unknown property {propertyNameToCompareAgainst}"));
                }

                // With the property info, get it's value and cast it to an int.
                var propertyValueToCompareAgainst = propertyInfo.GetValue(validationContext.ObjectInstance, null) as int?;

                // If the value is null return an error saying so.
                if (propertyValueToCompareAgainst == null)
                {
                    return new ValidationResult(string.Format(CultureInfo.CurrentCulture, $"Value of property {propertyNameToCompareAgainst} is null"));
                }

                total += propertyValueToCompareAgainst ?? 0;
            }

            // Check if the value is greater than or equal to the property value we are comparing against
            // If value is greater than or equal to propertyValueToCompareAgainst, it's a success.
            // Otherwise, return an error.
            if ((int)value >= total)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName, (int)value));
            }
        }
    }
}
