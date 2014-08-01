using System.Linq;
using UpdateControls.Fields;
using UpdateControls.Correspondence.BinaryHTTPClient;

namespace TrainingRooms.FakeDevice
{
    public class HTTPConfigurationProvider : IHTTPConfigurationProvider
    {
        public HTTPConfiguration Configuration
        {
            get
            {
                string address = "https://api.facetedworlds.com/correspondence_server_web/bin";
                string apiKey = "B6F6EFEADE474A038143B0ECB40CDCB6";
				int timeoutSeconds = 30;
                return new HTTPConfiguration(address, "TrainingRooms.FakeDevice", apiKey, timeoutSeconds);
            }
        }
    }
}
