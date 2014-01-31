using System;
using System.Linq;
using System.Reflection;
using aPC.Common.Defaults;
using aPC.Common.Entities;

namespace aPC.Common.Accessors
{
  public class SceneAccessor
  {
    //REVIEW: I'm not keen on big switch-type statements. Here's a possible alternative.
    public amBXScene GetScene(string xiDescription)
    {
      var lSceneProperty = mDefaultScenes
        .GetType()
        .GetProperties()
        .SingleOrDefault(property => MatchesSceneName(property, xiDescription));

      if (lSceneProperty == null)
      {
        Console.WriteLine("Integrated scene with description {0} not found.", xiDescription);
        return null;
      }

      return lSceneProperty.GetValue(mDefaultScenes) as amBXScene;
    }

    private bool MatchesSceneName(PropertyInfo xiPropertyInfo, string xiDescription)
    {
      var lAttribute = xiPropertyInfo.GetCustomAttribute<SceneNameAttribute>();

      if (lAttribute == null) return false;

      return string.Equals(lAttribute.Name, xiDescription, StringComparison.InvariantCultureIgnoreCase);
    }

    private static readonly DefaultScenes mDefaultScenes = new DefaultScenes();
  }
}
