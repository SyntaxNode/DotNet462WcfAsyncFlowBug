using System;
using System.ServiceModel;

namespace DotNet462WcfAsyncFlowBug.Host
{
	[ServiceContract]
	interface IServiceB
	{
		[OperationContract]
		string Bar();
	}

	class ServiceB : IServiceB
	{
		public string Bar()
		{
			return $"Greetings {GetAuthHeaderValue()}";
		}

		private string GetAuthHeaderValue()
		{
			var headers = OperationContext.Current.RequestContext.RequestMessage.Headers;
			var headerIndex = headers.FindHeader("Authorization", String.Empty);
			return headers.GetHeader<string>(headerIndex);
		}
	}

	class ServiceBHost : IDisposable
	{
		private ServiceHost host;
		private bool disposed;

		public ServiceBHost()
		{
			InitializeServiceHost();
			SetEndpoint();
			host.Open();
		}

		private void InitializeServiceHost()
		{
			var serviceType = typeof(ServiceB);
			host = new ServiceHost(serviceType, Addresses.ServiceBUri);
		}

		private void SetEndpoint()
		{
			host.AddServiceEndpoint(typeof(IServiceB), new WSHttpBinding(), String.Empty);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
				return;

			if (disposing)
			{
				host.Close();
				((IDisposable)host).Dispose();
			}

			disposed = true;
		}
	}
}
