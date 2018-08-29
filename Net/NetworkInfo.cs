using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Net
{
    [Activity("Network Info")]
    public class NetworkInfo : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddOutput("ID");
            designer.AddOutput("Physical Address");
            designer.AddOutput("Description");
            designer.AddOutput("Name");
            designer.AddOutput("Operational Status");
            designer.AddOutput("Interface Type");
            designer.AddOutput("Speed").AsNumber();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
            {
                Dictionary<string, object> publishedData = new Dictionary<string, object>();

                publishedData.Add("ID", adapter.Id);
                publishedData.Add("Physical Address", adapter.GetPhysicalAddress());
                publishedData.Add("Description", adapter.Description);
                publishedData.Add("Name", adapter.Name);
                publishedData.Add("Operational Status", adapter.OperationalStatus);
                publishedData.Add("Interface Type", adapter.NetworkInterfaceType);
                publishedData.Add("Speed", adapter.Speed);

                response.Publish(publishedData);
            }
        }
    }

}
