using System.ServiceModel;
using System.Threading.Tasks;

namespace DotNet462WcfAsyncFlowBug.Client
{
	[ServiceContract(ConfigurationName = "IServiceA")]
	public interface IServiceA
	{
		[OperationContract(Action = "http://tempuri.org/IServiceA/Foo", ReplyAction = "http://tempuri.org/IServiceA/FooResponse")]
		string Foo();

		[OperationContract(Action = "http://tempuri.org/IServiceA/Foo", ReplyAction = "http://tempuri.org/IServiceA/FooResponse")]
		Task<string> FooAsync();
	}

	public partial class ServiceAClient : ClientBase<IServiceA>, IServiceA
	{
		public ServiceAClient()
			: base(new WSHttpBinding(), new EndpointAddress(Addresses.ServiceAUri))
		{ }

		public string Foo() => Channel.Foo();
		public Task<string> FooAsync() => Channel.FooAsync();
	}
}
