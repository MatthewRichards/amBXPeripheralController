using System.Threading;

namespace aPC.Client.Disco
{
  //REVIEW: It feels like this class should be immutable. But it's not, because you construct it piecemeal. This is an interesting
  // dilemma and I'm not quite sure how to resolve it, other than by merging the data structure with the parsing code as per the 
  // non-disco client which has other disadvantages. Maybe there's just no reason this shouldn't be mutable, but in general
  // immutable is better :-)
  class Settings
  {
    public Settings()
    {
      SetupDefaultValues();
    }

    private void SetupDefaultValues()
    {
      BPM = 150;
      RedGenerator = new CustomScaleRandomNumberGenerator(0, 1);
      BlueGenerator = new CustomScaleRandomNumberGenerator(0, 1);
      GreenGenerator = new CustomScaleRandomNumberGenerator(0, 1);
      FanSpeedGenerator = new CustomScaleRandomNumberGenerator(0, 0);
    }

    public int PushInterval
    {
      get
      {
        return (1000 * 60) / BPM;

      }
    }

    public int BPM { private get; set; }
    public CustomScaleRandomNumberGenerator RedGenerator;
    public CustomScaleRandomNumberGenerator BlueGenerator;
    public CustomScaleRandomNumberGenerator GreenGenerator;
    public CustomScaleRandomNumberGenerator FanSpeedGenerator;
  }
}
