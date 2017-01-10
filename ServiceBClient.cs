using System.ServiceModel;
using System.Threading.Tasks;

namespace DotNet462WcfAsyncFlowBug.Client
{
	[ServiceContract(ConfigurationName = "IServiceB")]
	public interface IServiceB
	{
		[OperationContract(Action = "http://tempuri.org/IServiceB/Bar", ReplyAction = "http://tempuri.org/IServiceB/BarResponse")]
		string Bar();

		[OperationContract(Action = "http://tempuri.org/IServiceB/Bar", ReplyAction = "http://tempuri.org/IServiceB/BarResponse")]
		Task<string> BarAsync();
	}

	public partial class ServiceBClient : ClientBase<IServiceB>, IServiceB
	{
		public ServiceBClient()
			: base(new WSHttpBinding(), new EndpointAddress(Addresses.ServiceBUri))
		{ }

		public string Bar() => Channel.Bar();
		public Task<string> BarAsync() => Channel.BarAsync();
	}
}
