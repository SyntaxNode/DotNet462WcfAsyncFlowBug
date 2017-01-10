# WCF Async Flow Bug

We receive an InvalidOperationException with the message 'The value of OperationContext.Current is not
the OperationContext value installed by this OperationContextScope.' when disposing a second 
OperationContextScope object within the async handler of a WCF method.

This works fine in .Net Framework 4.6.1 and older, but breaks in .Net Framework 4.6.2. We can workaround
the issue by calling the OperationContext's internal DisableAsyncFlow method before instantiating any
new OperationContextScope objects.

Place a breakpoint in the catch clause of ServiceA to observe the issue.