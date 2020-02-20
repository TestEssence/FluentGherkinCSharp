using System;
using Xunit.Abstractions;

class OutputListener
    {
        static private ITestOutputHelper _testOutputHelper = null;
        public static void SetListener(ITestOutputHelper _outputHelper)
        {
            _testOutputHelper = _outputHelper;
        }
        public static ITestOutputHelper GetListener()
        {
            if (_testOutputHelper != null)
                return _testOutputHelper;
            else throw (new NullReferenceException("_testOutputHelper is null"));
        }

    }
