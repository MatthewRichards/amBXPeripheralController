using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace aPC.Client
{
  class ArgumentReader
  {
    //REVIEW: There really ought to be some code you can use to handle the argument parsing.
    //REVIEW: Also, it actually feels like this class is doing two things - it's reading the arguments, and it's a data structure for storing them.
    //  I'd quite like to see ClientTask take an object which is its configuration, rather than taking the raw list of arguments, but without
    //  splitting up this class that feels a bit unnecessarily messy.
    //  In fact: Just like you've done in the Disco client :-)
    public ArgumentReader(List<string> xiArguments)
    {
      if (xiArguments.Count != 2)
      {
        throw new UsageException("Unexpected number of arguments");
      }

      if (xiArguments[0] == @"/F")
      {
        // File passed in
        SceneXml = RetrieveFile(xiArguments[1]);
      }
      else if (xiArguments[0] == @"/I")
      {
        // (Integrated) Scene passed in
        SceneName = xiArguments[1];
      }
      else
      {
        throw new UsageException("Unexpected first argument");
      }
    }

    //REVIEW: Wrong case in xi"f"ilePath
    private string RetrieveFile(string xifilePath)
    {
      string lInputFilePath;

      try
      {
        lInputFilePath = Path.GetFullPath(xifilePath);
      }
      catch
      {
        //REVIEW: What's the benefit in this comment?
        //REVIEW: If you wanted production-quality code, you should try to make your error more specific - "not valid" when the actual (hidden) error
        //  is "permission denied" is really annoying! If you handle specific expected exceptions, and then include the stack trace in unexpected 
        //  exceptions, the result is generally more usable.

        // File not there / error
        throw new UsageException("Input was not a valid path (a full path is required)");
      }

      //REVIEW: Simpler is:
      //  File.ReadAllLines(lInputFilePath);
      // And actually, do you even need the GetFullPath thing above? I'd expect relative paths to work as inputs to ReadAllLines or StreamReader
      // And you're missing error handling code here
      using (var lReader = new StreamReader(lInputFilePath))
      {
        return lReader.ReadToEnd();
      }
    }


    // The scene is integrated if a Scene name is specified
    // It isn't integrated if an Xml scene is given
    public bool IsIntegratedScene
    {
      get
      {
        if (!string.IsNullOrEmpty(SceneName))
        {
          return true;
        }

        if (!string.IsNullOrEmpty(SceneXml))
        {
          return false;
        }

        //REVIEW: Arguably this is redundant as your unit tests will ensure you never end up in this inconsistent state.
        // Speaking of unit tests... Where are they? :-)
        throw new InvalidOperationException("Attempted to access scene information before any data is available");
      }
    }

    public string SceneName { get; private set; }
    public string SceneXml { get; private set; }
  }
}
