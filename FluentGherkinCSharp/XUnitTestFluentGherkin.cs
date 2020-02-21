using System;
using Xunit;
using Xunit.Abstractions;
using FluentGherkinCSharp;


    public class XUnitTestFluentGherkin
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public XUnitTestFluentGherkin(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            OutputListener.SetListener(_testOutputHelper);
        }


        [Fact]
        public void TestFluentGherkinWithMutationalFailure()
        {
            Random rnd = new Random();
            int deviation = rnd.Next(0,2);

            Gherkin
                .Given("We have 3", () => 9-6)
                .And(string.Format("inpuit value was mutated into a deviation of {0} is added", deviation), givenValue => givenValue + deviation)
                .When("we multiply it by 3", stepResult => stepResult * 3)
                .And("we multiply the result by 3", whenResult =>{ //multiline step implementation with local variables
                        int z = whenResult * 3;
                        return z;
                    })
                .Then("the result is 27", stepResult => stepResult == 27);
        }

        [Fact]
        public void TestFluentGherkinSimple()
        {
            Gherkin
                .Given("We have 3", 3)
                .When("we multiply it by 3", givenValue => givenValue * 3)
                .And("we multiply the result by 3", whenResult =>  whenResult * 3)
                .Then("the result is 27", stepResult => stepResult == 27);
        }

        [Theory]
        [InlineData(1, 9)]
        [InlineData(3, 27)]
        [InlineData(5, 25)] //expected to fail        
        public void TestFluentGherkinTheory(int inputValue, int expectedResult)
        {
            Gherkin
                .Given(string.Format("We have {0}", inputValue), inputValue)
                .When("we multiply it by 3", givenValue => givenValue * 3)
                .And("we multiply the result by 3", whenResult => whenResult * 3)
                .Then(string.Format("the result is {0}",expectedResult), stepResult => stepResult == expectedResult);
        }

        [Fact]
        public void TestFluentGherkinDouble()
        {
            Gherkin
                .Given("We have 3", 3) //input is Integer 
                .When("we multiply it by 3", givenValue => givenValue * 3)
                .And("we multiply the result by 3", whenResult =>  whenResult * 3.000)
                .Then("the result is 27", stepResult => stepResult == 27.000); //result is Double
        }

    [Fact]
    public void TestWithLambdaNoText()
    {
        Gherkin
            .Given(3)
            .When(givenValue => givenValue * 3)
            .And(whenResult => whenResult * 3.000)
            .Then(stepResult => stepResult == 27.000);
    }

//The magic of fluency in details
[Fact]
    public void TestBehindTheMagicOfFluency()
    {
        GherkinStep<int> givenStep = Gherkin.Given("We have 3", 3);
        GherkinStep<int> whenStep = givenStep.When("we multiply it by 3", value => value * 3);
        GherkinStep<double> andStep = whenStep.And("we multiply the result by 3", value => value * 3.000);
        andStep.Then("the result is 27", stepResult => stepResult == 27.000); //result is Double
    }
}