using System;

namespace aPC.Client
{
  [Serializable]
  class UsageException : Exception
  {
    public UsageException(Exception xiException, string xiDescription)
    {
      mException = xiException;
      mUserDescription = xiDescription;
    }

    public UsageException(string xiDescription)
    {
      mUserDescription = xiDescription;
    }

    public void DisplayUsage()
    {
      Console.WriteLine("Error: " + mUserDescription);
      Console.WriteLine(Environment.NewLine + Environment.NewLine);

      if (mException != null)
      {
        //REVIEW: You should just write mException, not its component parts. This will omit any inner exceptions.
        Console.WriteLine("Underlying Exception: " + 
                          Environment.NewLine + mException.Message +
                          Environment.NewLine + mException.StackTrace);
      }
      Console.WriteLine(mUsage);
    }

    private readonly Exception mException;
    private readonly string mUserDescription;

    //REVIEW: It looks to me like relative paths are supported...
    private const string mUsage = 
@"Usage: Client [/I | /F] argument

One of either /I or /F MUST be given.

/I : Use an integrated amBXScene built into the application.  The exact
     name of the scene will need to be confirmed before running this 
     command and can currenly be found in the Source code.
     Here, the argument is the name of the integrated ambxScene you 
     would like to run.

/F : Run a custom xml ambxScene.  In this case, the argument is the full 
     absolute filepath of the xml file (not that relative paths are 
     currently unsupported).

Examples:

Client.exe /I CCNet_Green
Client.exe /F C:\path\to\scene\Scene.xml";
  }
}
