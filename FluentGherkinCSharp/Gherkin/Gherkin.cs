using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit.Abstractions;

namespace FluentGherkinCSharp
{
    public class Gherkin
    {
            public static GherkinStep<T> Given<T> (T receivedObj){
                return new GherkinStep<T>(receivedObj).Given(receivedObj);
            }
            public static GherkinStep<T> Given<T>(String message, Func<T> givenFunction) {
                T givenValue = givenFunction();
                return new GherkinStep<T>(givenValue).Given(message, givenValue);
            }

            public static GherkinStep<T> Given<T>(String message, T receivedObj)
            {
            return new GherkinStep<T>(receivedObj).Given(message, receivedObj);
        }
      
      
    }
    }
