using UnityEngine;

// https://garry.tv/2018/01/16/timesince/
namespace GameJamStarterKit
{
  /// <summary>
  /// Measures time since this struct was initialized. Implicitly converts to a float. 
  /// <para>Example: TimeSince timeSinceStart = 0f will measure the time since the moment timeSinceStart was initialized</para>
  /// <para>Usage: if ( timeSinceStart > 10f ) { // it's been more than 10 seconds! };</para>
  /// </summary>
  public struct TimeSince
  {
    private float _time;

    public static implicit operator float(TimeSince ts)
    {
      return Time.time - ts._time;
    }

    public static implicit operator TimeSince(float ts)
    {
      return new TimeSince {_time = Time.time - ts};
    }
  }

  /// <summary>
  /// Measures unscaled time since this struct was initialized. Implicitly converts to a float. 
  /// <para>Example: UnscaledTimeSince timeSinceStart = 0f will measure the unscaled time since the moment timeSinceStart was initialized</para>
  /// <para>Usage: ( timeSinceStart > 10f ) { // it's been more than 10 unscaled seconds! };</para>
  /// </summary>
  public struct UnscaledTimeSince
  {
    private float _time;

    public static implicit operator float(UnscaledTimeSince ts)
    {
      return Time.unscaledTime - ts._time;
    }

    public static implicit operator UnscaledTimeSince(float ts)
    {
      return new UnscaledTimeSince {_time = Time.unscaledTime - ts};
    }
  }
}