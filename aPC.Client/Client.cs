namespace aPC.Client
{
  internal class Client
  {
    //REVIEW: Private?
    private static void Main(string[] args)
    {
      var lClientTask = new ClientTask(args);
      lClientTask.Push();
    }
  }
}
