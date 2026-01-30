using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ECI.Test.BL.Tests
{
    /// <summary>
    /// Test configuration and setup for the Business Logic Test project
    /// </summary>
    [TestClass]
    public class TestConfiguration
    {
        /// <summary>
        /// Assembly initialize method - runs once before all tests in the assembly
        /// </summary>
        /// <param name="context">Test context</param>
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            // Initialize any assembly-level test resources
            Console.WriteLine("Initializing ECI.Test.BL.Tests assembly...");
            
            // You can add any assembly-level initialization here
            // For example: Initialize test databases, load configuration, etc.
        }

        /// <summary>
        /// Assembly cleanup method - runs once after all tests in the assembly
        /// </summary>
        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            // Clean up any assembly-level test resources
            Console.WriteLine("Cleaning up ECI.Test.BL.Tests assembly...");
            
            // You can add any assembly-level cleanup here
            // For example: Dispose test databases, clean up files, etc.
        }
    }

    /// <summary>
    /// Base class for all test classes with common setup and teardown
    /// </summary>
    [TestClass]
    public abstract class BaseTestClass
    {
        /// <summary>
        /// Test context property
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Class initialize method - runs once before all tests in the class
        /// </summary>
        /// <param name="testContext">Test context</param>
        [ClassInitialize]
        public static void BaseClassInitialize(TestContext testContext)
        {
            Console.WriteLine($"Initializing test class: {testContext.FullyQualifiedTestClassName}");
        }

        /// <summary>
        /// Class cleanup method - runs once after all tests in the class
        /// </summary>
        [ClassCleanup]
        public static void BaseClassCleanup()
        {
            Console.WriteLine("Cleaning up test class...");
        }

        /// <summary>
        /// Test initialize method - runs before each test method
        /// </summary>
        [TestInitialize]
        public virtual void TestInitialize()
        {
            Console.WriteLine($"Starting test: {TestContext.TestName}");
        }

        /// <summary>
        /// Test cleanup method - runs after each test method
        /// </summary>
        [TestCleanup]
        public virtual void TestCleanup()
        {
            Console.WriteLine($"Finished test: {TestContext.TestName} - Result: {TestContext.CurrentTestOutcome}");
        }
    }
}