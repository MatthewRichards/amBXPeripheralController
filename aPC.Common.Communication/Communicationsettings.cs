namespace aPC.Common.Communication
{
    public static class CommunicationSettings
    {
      //REVIEW: It's not quite clear what the point of the HostnameHolder is. I imagine that in reality you'd want to configure
      // the full URL. Why not just an app.config file with the ServiceUrl configured in it?
      //EDIT: Oh, I see - it's because this is used by both client and server? Sort of clever, but still feels like in a real world
      // scenario you'd want more flexibility.

      // Template to use for the Url - [HOSTNAME] needs to be replaced with
      // the actual hostname to use.
      public const string HostnameHolder = @"[HOSTNAME]";
      public const string ServiceUrlTemplate = @"http://" + HostnameHolder + "/amBXNotification";
    }
}
