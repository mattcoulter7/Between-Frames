using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

static public class AnimationCurveExtension
{
    static public Keyframe GetLatest(this Keyframe[] self)
    {
        return self.Aggregate(self[0], (prev, curr) => curr.time > prev.time ? curr : prev);
    }
    static public Keyframe GetEarliest(this Keyframe[] self)
    {
        return self.Aggregate(self[0], (prev, curr) => curr.time < prev.time ? curr : prev);
    }
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

    static public Keyframe[] GetWithinRange(this Keyframe[] self,float? minTime = null, float? maxTime = null)
    {
        return self.Where((kf) =>   
            (minTime.HasValue ? kf.time >= minTime.Value : true) &&
            (maxTime.HasValue ? kf.time <= maxTime.Value : true)
        ).ToArray();
    }
    static public Keyframe[] GetScaled(this Keyframe[] self, float timeScale = 1f)
    {
        Keyframe[] scaledKeys = self;
        for (int i = 0; i < scaledKeys.Length; i++)
        {
            scaledKeys[i].time *= timeScale;
        }
        return scaledKeys;
    }
}
