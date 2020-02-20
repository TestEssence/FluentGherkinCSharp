#FluentGherkinCSharp

A c# implementation of the functional [approach](https://dzone.com/articles/a-new-approach-to-given-when-then) for test code propoed by [Gabriel Deliu](https://dzone.com/users/1031037/gabriel.deliu.html).

---
Test Code Example (xUnit):
 ```csharp
  [Fact]
        public void TestFluentGherkinSimple()
        {
            Gherkin
                .Given("We have 3", 3)
                .When("we multiply it by 3", givenValue => givenValue * 3)
                .And("we multiply the result by 3", whenResult =>  whenResult * 3)
                .Then("the result is 27", stepResult => stepResult == 27);
        }
 ```



