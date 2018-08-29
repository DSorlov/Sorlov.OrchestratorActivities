using System.ServiceModel;
using Sorlov.OrchestratorActivities.PSRunspaceManager.Interfaces;

namespace Sorlov.OrchestratorActivities.PowerShell
{
    public static class PSRunspaceManger
    {
        public static IRunspaceManager CreateClient()
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = 2147483647;
            basicHttpBinding.MaxBufferPoolSize = 2147483647;
            basicHttpBinding.MaxBufferSize = 2147483647;
            basicHttpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            basicHttpBinding.ReaderQuotas.MaxDepth = 32;
            basicHttpBinding.ReaderQuotas.MaxArrayLength = 2147483647;
            basicHttpBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            basicHttpBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;

            string baseAddress = "http://localhost:62500/RunspaceManager";
            ChannelFactory<IRunspaceManager> channelFactory = new ChannelFactory<IRunspaceManager>(basicHttpBinding, new EndpointAddress(baseAddress));
            return channelFactory.CreateChannel();
        }
    }
}
