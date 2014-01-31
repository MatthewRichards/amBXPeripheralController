using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Client.Disco
{
  class ArgumentReader
  {
    public ArgumentReader(List<string> xiArgs)
    {
      mArgs = xiArgs;
    }

    public Settings ParseArguments()
    {
      var lArguments = new Settings();

      foreach (string lArg in mArgs)
      {
        var lDeconstructedArgument = lArg.Split(':');

        switch (lDeconstructedArgument[0].ToLower())
        {
          case "BPM":
            lArguments.BPM = int.Parse(lDeconstructedArgument[1]);
            break;
          case "red":
            lArguments.RedGenerator = GetColourWidth(lDeconstructedArgument[1]);
            break;
          case "blue":
            lArguments.BlueGenerator = GetColourWidth(lDeconstructedArgument[1]);
            break;
          case "green":
            lArguments.GreenGenerator = GetColourWidth(lDeconstructedArgument[1]);
            break;
          case "fan":
            lArguments.FanSpeedGenerator = GetColourWidth(lDeconstructedArgument[1]);
            break;
          default:
            throw new UsageException("Unknown argument: " + lDeconstructedArgument);
        }

      }

      return lArguments;
    }

    private CustomScaleRandomNumberGenerator GetColourWidth(string xiWidth)
    {
      var lDeconstructedWidth = xiWidth.Split(',');
      if (lDeconstructedWidth.Count() != 2)
      {
        throw new UsageException("Invalid # of args in colour width");
      }

      return new CustomScaleRandomNumberGenerator(float.Parse(lDeconstructedWidth[0]),
                             float.Parse(lDeconstructedWidth[1]));
    }

    private readonly List<string> mArgs;

  }
}
