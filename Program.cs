using System;

namespace DotNet462WcfAsyncFlowBug
{
	class Program
	{
		static void Main(string[] args)
		{
			using (new Host.ServiceAHost())
			using (new Host.ServiceBHost())
			{
				var client = new Client.ServiceAClient();
				var response = client.Foo();
				Console.WriteLine(response);
			}
		}
	}
}
