using ECI.Test.BL.Validation;
using System;
using System.Collections.Generic;

namespace ECI.Test.BL.Tests.TestUtilities
{
    /// <summary>
    /// Test utilities and helper methods for unit testing
    /// </summary>
    public static class TestHelper
    {
        /// <summary>
        /// Creates a ValidationResult with validation errors
        /// </summary>
        /// <param name="errors">List of error messages</param>
        /// <returns>ValidationResult with errors</returns>
        public static ValidationResult CreateValidationResult(params string[] errors)
        {
            var result = new ValidationResult();
            foreach (var error in errors)
            {
                result.AddError(error);
            }
            return result;
        }

        /// <summary>
        /// Creates a valid ValidationResult (no errors)
        /// </summary>
        /// <returns>Valid ValidationResult</returns>
        public static ValidationResult CreateValidValidationResult()
        {
            return new ValidationResult();
        }

        /// <summary>
        /// Generates test data for DateTime values
        /// </summary>
        public static class DateTimeHelper
        {
            public static DateTime Today => DateTime.Today;
            public static DateTime Yesterday => DateTime.Today.AddDays(-1);
            public static DateTime Tomorrow => DateTime.Today.AddDays(1);
            public static DateTime LastWeek => DateTime.Today.AddDays(-7);
            public static DateTime NextWeek => DateTime.Today.AddDays(7);
        }

        /// <summary>
        /// Generates test data for common test scenarios
        /// </summary>
        public static class TestData
        {
            public static readonly string ValidClientName = "Test Client";
            public static readonly string ValidPhone = "555-0123";
            public static readonly string TooLongClientName = new string('A', 150);
            public static readonly string TooLongPhone = new string('1', 25);

            public static readonly string ValidDogName = "Test Dog";
            public static readonly string ValidBreed = "Test Breed";
            public static readonly int ValidAge = 5;
            public static readonly string TooLongDogName = new string('D', 150);
            public static readonly string TooLongBreed = new string('B', 75);

            public static readonly string ValidUsername = "testuser";
            public static readonly string ValidPassword = "testpassword";
            public static readonly string TooLongUsername = new string('U', 75);
            public static readonly string TooLongPassword = new string('P', 300);

            public static readonly int ValidClientId = 1;
            public static readonly int ValidDogId = 1;
            public static readonly int ValidDuration = 30;
            public static readonly int InvalidDuration = 0;
        }
    }
}