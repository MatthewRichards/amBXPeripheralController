using System;
using System.ServiceModel;
using aPC.Common.Communication;

namespace aPC.Common.Server.Communication
{
  public class CommunicationManager : IDisposable
  {
    public CommunicationManager(NotificationServiceBase xiNotificationService)
    {
      //REVIEW: Why is Open not in SetupHost? And if it's not, it might be worth changing SetupHost to return the host, and
      // assign it in the constructor - that makes the dependency between these two lines more obvious.
      SetupHost(xiNotificationService);
      mHost.Open();
    }

    private void SetupHost(NotificationServiceBase xiNotificationService)
    {
      mHost = new ServiceHost(xiNotificationService.GetType());

      AddEndpoint(CommunicationSettings.ServiceUrlTemplate.
        Replace(CommunicationSettings.HostnameHolder, System.Net.Dns.GetHostName()));
    }

    private void AddEndpoint(string xiUrl)
    {
      mHost.AddServiceEndpoint(typeof(INotificationService),
                               new BasicHttpBinding(),
                               xiUrl);
    }

    public void Close()
    {
      mHost.Close();
    }

    public void Dispose()
    {
      //REVIEW: What happens if Close is called twice - is that ok?
      Close();
    }

    private ServiceHost mHost;
  }
}
