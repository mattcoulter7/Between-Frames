using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// This extenstion class provides easy manipulation of Unity Keyframes
/// </summary>
static public class AnimationCurveExtension
{
    /// <summary>
    /// Extracts the latest KeyFrame from an array of Keyframes (greatest time value)
    /// can be invoked by keyframes.GetLatest()
    /// </summary>
    /// <param name="self">The object which this method is being called from</param>
    /// <returns>A KeyFrame</returns>
    static public Keyframe GetLatest(this Keyframe[] self)
    {
        return self.Aggregate(self[0], (prev, curr) => curr.time > prev.time ? curr : prev);
    }

    /// <summary>
    /// Extracts the earlist KeyFrame from an array of Keyframes (smallest time value)
    /// can be invoked by keyframes.GetEarliest()
    /// </summary>
    /// <param name="self">The object which this method is being called from</param>
    /// <returns>A KeyFrame</returns>
    static public Keyframe GetEarliest(this Keyframe[] self)
    {
        return self.Aggregate(self[0], (prev, curr) => curr.time < prev.time ? curr : prev);
    }

    /// <summary>
    /// Order an array of key frames in reverse chronilogical order
    /// Can be invoked by reversed = keyframes.GetReversed()
    /// </summary>
    /// <param name="self">The object which this method is being called from</param>
    /// <returns>An array of KeyFrames</returns>
    static public Keyframe[] GetReversed(this Keyframe[] self)
    {
        Keyframe[] reversedKeys = self;
        if (reversedKeys.Length == 0) return reversedKeys;

        //1. calculate min and max
        Keyframe earlistKey = reversedKeys.GetEarliest();
        float earlistTime = earlistKey.time;

        Keyframe latestKey = reversedKeys.GetLatest();
        float latestTime = latestKey.time;

        for (int i = 0; i < reversedKeys.Length; i++)
        {
            //2.subtract max from each
            reversedKeys[i].time -= latestTime;

            //3.make positive
            reversedKeys[i].time = Mathf.Abs(reversedKeys[i].time);

            //4. re add the min
            reversedKeys[i].time += earlistTime;
        }
        return reversedKeys;
    }

    /// <summary>
    /// Offset the time of all keyframes in an array by an amount of time
    /// The time unit is the same as what is used in Unity's Keyframe
    /// </summary>
    /// <param name="self">The object which this method is being called from</param>
    /// <param name="time">the float time amount</param>
    /// <returns>An array of KeyFrames</returns>
    static public Keyframe[] GetOffset(this Keyframe[] self,float time)
    {
        Keyframe[] offsetKeys = self;
        if (offsetKeys.Length == 0) return offsetKeys;

        for (int i = 0; i < offsetKeys.Length; i++)
        {
            offsetKeys[i].time += time;
        }
        return offsetKeys;
    }

    /// <summary>
    /// Filters an array of keyframes where time values are in between a min and max time (inclusive)
    /// </summary>
    /// <param name="self">The object which this method is being called from</param>
    /// <param name="minTime">The min time value (inclusive)</param>
    /// <param name="maxTime">The max time value (inclusive)</param>
    /// <returns>An array of time keyframes</returns>
    static public Keyframe[] GetWithinRange(this Keyframe[] self,float? minTime = null, float? maxTime = null)
    {
        return self.Where((kf) =>   
            (minTime.HasValue ? kf.time >= minTime.Value : true) &&
            (maxTime.HasValue ? kf.time <= maxTime.Value : true)
        ).ToArray();
    }

    /// <summary>
    /// Scales the playback time of a set of keyframes by a time scale multiplier
    /// For example, a timeScale of 2 would make the keyframes play at half the speed
    /// A timeScale of 0.5 would make the animation twice as fast
    /// </summary>
    /// <param name="self">The object which this method is being called from</param>
    /// <param name="timeScale">The timeScale multiplier</param>
    /// <returns>An array of keyframes after scaling</returns>
    static public Keyframe[] GetScaled(this Keyframe[] self, float timeScale = 1f)
    {
        Keyframe[] scaledKeys = self;
        for (int i = 0; i < scaledKeys.Length; i++)
        {
            scaledKeys[i].time *= timeScale;
        }
        return scaledKeys;
    }

    /// <summary>
    /// Extracts the latest AnimationEvent from an array of AnimationEvents (greatest time value)
    /// can be invoked by events.GetLatest()
    /// </summary>
    /// <param name="self">The object which this method is being called from</param>
    /// <returns>A AnimationEvent</returns>
    static public AnimationEvent GetLatest(this AnimationEvent[] self)
    {
        return self.Aggregate(self[0], (prev, curr) => curr.time > prev.time ? curr : prev);
    }
    /// <summary>
    /// Extracts the earlist AnimationEvent from an array of AnimationEvents (smallest time value)
    /// can be invoked by events.GetEarliest()
    /// </summary>
    /// <param name="self">The object which this method is being called from</param>
    /// <returns>A AnimationEvent</returns>
    static public AnimationEvent GetEarliest(this AnimationEvent[] self)
    {
        return self.Aggregate(self[0], (prev, curr) => curr.time < prev.time ? curr : prev);
    }
    /// <summary>
    /// Order an array of key frames in reverse chronilogical order
    /// Can be invoked by reversed = events.GetReversed()
    /// </summary>
    /// <param name="self">The object which this method is being called from</param>
    /// <returns>An array of AnimationEvents</returns>
    static public AnimationEvent[] GetReversed(this AnimationEvent[] self)
    {
        AnimationEvent[] reversedKeys = self;
        if (reversedKeys.Length == 0) return reversedKeys;

        //1. calculate min and max
        AnimationEvent earlistKey = reversedKeys.GetEarliest();
        float earlistTime = earlistKey.time;

        AnimationEvent latestKey = reversedKeys.GetLatest();
        float latestTime = latestKey.time;

        for (int i = 0; i < reversedKeys.Length; i++)
        {
            //2.subtract max from each
            reversedKeys[i].time -= latestTime;

            //3.make positive
            reversedKeys[i].time = Mathf.Abs(reversedKeys[i].time);

            //4. re add the min
            reversedKeys[i].time += earlistTime;
        }
        return reversedKeys;
    }
    /// <summary>
    /// Offset the time of all AnimationEvents in an array by an amount of time
    /// The time unit is the same as what is used in Unity's AnimationEvent
    /// </summary>
    /// <param name="self">The object which this method is being called from</param>
    /// <param name="time">the float time amount</param>
    /// <returns>An array of AnimationEvents</returns>
    static public AnimationEvent[] GetOffset(this AnimationEvent[] self, float time)
    {
        AnimationEvent[] offsetKeys = self;
        if (offsetKeys.Length == 0) return offsetKeys;

        for (int i = 0; i < offsetKeys.Length; i++)
        {
            offsetKeys[i].time += time;
        }
        return offsetKeys;
    }

    /// <summary>
    /// Filters an array of AnimationEvents where time values are in between a min and max time (inclusive)
    /// </summary>
    /// <param name="self">The object which this method is being called from</param>
    /// <param name="minTime">The min time value (inclusive)</param>
    /// <param name="maxTime">The max time value (inclusive)</param>
    /// <returns>An array of time AnimationEvents</returns>
    static public AnimationEvent[] GetWithinRange(this AnimationEvent[] self, float? minTime = null, float? maxTime = null)
    {
        return self.Where((kf) =>
            (minTime.HasValue ? kf.time >= minTime.Value : true) &&
            (maxTime.HasValue ? kf.time <= maxTime.Value : true)
        ).ToArray();
    }

    /// <summary>
    /// Scales the playback time of a set of AnimationEvents by a time scale multiplier
    /// For example, a timeScale of 2 would make the AnimationEvents play at half the speed
    /// A timeScale of 0.5 would make the animation twice as fast
    /// </summary>
    /// <param name="self">The object which this method is being called from</param>
    /// <param name="timeScale">The timeScale multiplier</param>
    /// <returns>An array of AnimationEvents after scaling</returns>
    static public AnimationEvent[] GetScaled(this AnimationEvent[] self, float timeScale = 1f)
    {
        AnimationEvent[] scaledKeys = self;
        for (int i = 0; i < scaledKeys.Length; i++)
        {
            scaledKeys[i].time *= timeScale;
        }
        return scaledKeys;
    }
}
