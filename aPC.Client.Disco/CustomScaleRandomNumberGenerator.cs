using System;

namespace aPC.Client.Disco
{
  /// <summary>
  /// A random number generator whose width can be specified.
  /// </summary>
  class CustomScaleRandomNumberGenerator
  {
    public CustomScaleRandomNumberGenerator(float xiMinimum, float xiMaximum)
    {
      mGenerator = new Random();

      mMinimum = xiMinimum;
      mMaximum = xiMaximum;
    }

    public float GetNext
    {
      get
      {
        return ((float)mGenerator.NextDouble() * Width) + mMinimum;
      }
    }

    private float Width
    {
      get
      {
        return mMaximum - mMinimum;
      }
    }

    private readonly float mMinimum;
    private readonly float mMaximum;
    private readonly Random mGenerator;
  }
}
