using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit.Abstractions;

namespace FluentGherkinCSharp
{
    public class GherkinStep<T>
    {
    public static string WHEN_FUNCTION_FAILED = "When function failed.";
    public static string STEP_FUNCTION_FAILED = "Step function failed.";
    public static string FAIL = "- failed";
    public static string FAILED = "Then >> {0} (Actual Value is {1})   [FAIL]";
    public static string PASSED = "Then >> {0}  [PASS]";


        public static string GIVEN =  "Given >> {0}";
    public static string WHEN =   "When >> {0}";
    public static string THEN = "Then >> {0}";
    public static string AND = "  And >> {0}";

        public static string THEN_NOT_SATISFIED = "Then not satisfied.";
    public static string THEN_FUNCTION_FAILED = "Then function failed.";
    public static string FUTURE_FAILED = "Given future failed.";
        private T received;

        

        public GherkinStep(T received)
        {
            this.received = received;
        }


        protected void LogStep(String stepMessage)
        {
            OutputListener.GetListener().WriteLine(stepMessage);
        }

        public GherkinStep<TReturn> When<TReturn>(Func<T, TReturn> whenFunction)
        {
            String message = null;
            try
            {
                TReturn obj = whenFunction(received);
                LogStep(string.Format(WHEN, obj.ToString()));
                return new GherkinStep<TReturn>(obj);
            }
            catch (Exception ex)
            {
                throw new Exception(message == null ? WHEN_FUNCTION_FAILED : message + FAIL, ex);
            }

            
        }

        
        public GherkinStep<TReturn> When<TReturn>(String message, Func<T, TReturn>  whenFunction){
                LogStep(string.Format(WHEN, message));
                try
                {
                TReturn obj = whenFunction(received);
                    return new GherkinStep<TReturn>(obj);
                }
                catch (Exception ex)
                {
                    throw new Exception(message == null ? WHEN_FUNCTION_FAILED : message + FAIL, ex);
                }
            }


        public GherkinStep<TReturn> And<TReturn>(String message, Func<T, TReturn> whenFunction)
        {
            LogStep(string.Format(AND, message));
            try
            {
                TReturn obj = whenFunction(received);
                return new GherkinStep<TReturn>(obj);
            }
            catch (Exception ex)
            {
                throw new Exception(message == null ? WHEN_FUNCTION_FAILED : message + FAIL, ex);
            }
        }


        public GherkinStep<TReturn> And<TReturn>( Func<T, TReturn> whenFunction)
        {
            
            try
            {
                TReturn obj = whenFunction(received);
                LogStep(string.Format(AND, obj.ToString()));
                return new GherkinStep<TReturn>(obj);
            }
            catch (Exception ex)
            {
                throw new Exception(WHEN_FUNCTION_FAILED, ex);
            }
        }


        public void Then(Action<T> thenFunction)
            {
                Then( null, thenFunction);
            }

            public void Then(String message, Predicate<T> thenFunction)
            {
                bool testResult = thenFunction(received);
                if (message == null)
                    message = testResult.ToString();
                if (!testResult)
                {
                LogStep(string.Format(FAILED, message, received.ToString())); 
                    throw new Exception(message == null ? THEN_NOT_SATISFIED : string.Format(FAILED, message, received.ToString()));
                }else {
                    LogStep(string.Format (PASSED, message));
                }
            }

            public void Then(Predicate<T> thenFunction)
            {
                Then(received.ToString(), thenFunction);
            }

        public void Then(String message, Action<T> thenFunction)
            {
                LogStep(message);
                try
                {
                    thenFunction(received);
                    LogStep(string.Format(PASSED, message));
                }
                catch (Exception ex)
                {
                   LogStep(string.Format(FAILED, message));
                    throw new Exception(message == null ? THEN_FUNCTION_FAILED : 
                        string.Format(FAILED, message, received.ToString()), ex);
                }
            }

            public GherkinStep<T> Given(T receivedObj){
                return Given(receivedObj.ToString(), receivedObj);
            }
      
            public GherkinStep<T> Given(String message, T receivedObj){
            GherkinStep<T> Gherkin1 = new GherkinStep<T> (receivedObj);
            Gherkin1.LogStep(string.Format(GIVEN, message));
            return Gherkin1;
            }

            public GherkinStep<T> Given(String message, Func<T> givenAction)
            {
            GherkinStep<T> Gherkin1 = new GherkinStep<T>(givenAction());
                Gherkin1.LogStep(string.Format(GIVEN, message));
                return Gherkin1;
            }
      
      
    }
    }
