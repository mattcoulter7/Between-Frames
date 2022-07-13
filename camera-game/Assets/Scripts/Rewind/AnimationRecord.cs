using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AnimationRecord
{
    public DynamicModifier valueGetter;
    public abstract object lastValue { get; }
    public abstract int frameCount { get; }

    public abstract void Apply(AnimationClip clip);
    public abstract void Record(bool optimise = true);
    public virtual void OnInitialise()
    {
        valueGetter.OnInitialise();
    }
}