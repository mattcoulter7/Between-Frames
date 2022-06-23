using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindInstance : MonoBehaviour
{
    public enum AnimationProperty
    {
        localPositionX,
        localPositionY,
        localPositionZ,
        localRotationX,
        localRotationY,
        localRotationZ,
        localScaleX,
        localScaleY,
        localScaleZ
    }

    [System.Serializable]
    public class AnimationPropertyRecord
    {
        public string _property;
        public GameObject _gameObject;
        public AnimationCurve _animationCurve;
        public AnimationClip _animationClip;
        public System.Type _type;
        public AnimationPropertyRecord(GameObject gameObject,AnimationClip animationClip,System.Type type,string property)
        {
            _gameObject = gameObject;
            _property = property;
            _animationCurve = new AnimationCurve();
            _animationClip = animationClip;
            _type = type;
        }

        public virtual float GetPropertyValue() { return 0f; }
        public void Record()
        {
            _animationCurve.AddKey(new Keyframe(Time.time, GetPropertyValue()));
        }

        public void OnFinish()
        {
            _animationClip.SetCurve("", _type, _property, _animationCurve);
        }
    }

    public class LocalPositionX : AnimationPropertyRecord {
        public LocalPositionX(GameObject gameObject,AnimationClip animationClip) : base(gameObject, animationClip,typeof(Transform),"localPosition.x")
        {
        }
        public override float GetPropertyValue()
        {
            return _gameObject.transform.localPosition.x;
        }
    }
    public class LocalPositionY : AnimationPropertyRecord
    {
        public LocalPositionY(GameObject gameObject, AnimationClip animationClip) : base(gameObject, animationClip, typeof(Transform), "localPosition.y") { 
        
        }
        public override float GetPropertyValue()
        {
            return _gameObject.transform.localPosition.y;
        }
    }
    public class LocalPositionZ : AnimationPropertyRecord
    {
        public LocalPositionZ(GameObject gameObject, AnimationClip animationClip) : base(gameObject, animationClip, typeof(Transform), "localPosition.z") { 
        
        }
        public override float GetPropertyValue()
        {
            return _gameObject.transform.localPosition.z;
        }
    }
    public class LocalRotationX : AnimationPropertyRecord
    {
        public LocalRotationX(GameObject gameObject, AnimationClip animationClip) : base(gameObject, animationClip, typeof(Transform), "localRotation.x") { 
        
        }
        public override float GetPropertyValue()
        {
            return _gameObject.transform.localRotation.x;
        }
    }
    public class LocalRotationY : AnimationPropertyRecord
    {
        public LocalRotationY(GameObject gameObject, AnimationClip animationClip) : base(gameObject, animationClip, typeof(Transform), "localRotation.y") { 
        
        }
        public override float GetPropertyValue()
        {
            return _gameObject.transform.localRotation.y;
        }
    }
    public class LocalRotationZ : AnimationPropertyRecord
    {
        public LocalRotationZ(GameObject gameObject, AnimationClip animationClip) : base(gameObject, animationClip, typeof(Transform), "localRotation.z") { 
        
        }
        public override float GetPropertyValue()
        {
            return _gameObject.transform.localRotation.z;
        }
    }
    public class LocalScaleX : AnimationPropertyRecord
    {
        public LocalScaleX(GameObject gameObject, AnimationClip animationClip) : base(gameObject, animationClip, typeof(Transform), "localScale.x") { 
        
        }
        public override float GetPropertyValue()
        {
            return _gameObject.transform.localScale.x;
        }
    }
    public class LocalScaleY : AnimationPropertyRecord
    {
        public LocalScaleY(GameObject gameObject, AnimationClip animationClip) : base(gameObject, animationClip, typeof(Transform), "localScale.y") { 
        
        }
        public override float GetPropertyValue()
        {
            return _gameObject.transform.localScale.y;
        }
    }
    public class LocalScaleZ : AnimationPropertyRecord
    {
        public LocalScaleZ(GameObject gameObject, AnimationClip animationClip) : base(gameObject, animationClip, typeof(Transform), "localScale.z") { 
        
        }
        public override float GetPropertyValue()
        {
            return _gameObject.transform.localScale.z;
        }
    }


    public AnimationProperty[] trackProperties;
    public AnimationPropertyRecord[] trackPropertyRecords;
    private Animation _animation;
    private AnimationClip _animationClip;

    // Start is called before the first frame update
    void Start()
    {
        _animation = gameObject.AddComponent<Animation>();
        
        _animationClip = new AnimationClip();
        _animationClip.legacy = true;
        _animationClip.name = "Live Rewind Animation (Instance)";

        trackPropertyRecords = new AnimationPropertyRecord[trackProperties.Length];
        for (int i = 0; i < trackPropertyRecords.Length; i++)
        {
            trackPropertyRecords[i] = CreateRecordFromProperty(trackProperties[i]);
        }

        // now animate the GameObject
        _animation.AddClip(_animationClip, _animationClip.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= 10f)
        {
            foreach (AnimationPropertyRecord record in trackPropertyRecords)
            {
                record.OnFinish();
            }
            _animation.Play(_animationClip.name);
        }
        else
        {
            foreach (AnimationPropertyRecord record in trackPropertyRecords)
            {
                record.Record();
            }
        }
    }
    private AnimationPropertyRecord CreateRecordFromProperty(AnimationProperty prop)
    {
        switch (prop)
        {
            case AnimationProperty.localPositionX:
                return new LocalPositionX(gameObject, _animationClip);
            case AnimationProperty.localPositionY:
                return new LocalPositionY(gameObject, _animationClip);
            case AnimationProperty.localPositionZ:
                return new LocalPositionZ(gameObject, _animationClip);
            case AnimationProperty.localRotationX:
                return new LocalRotationX(gameObject, _animationClip);
            case AnimationProperty.localRotationY:
                return new LocalRotationY(gameObject, _animationClip);
            case AnimationProperty.localRotationZ:
                return new LocalRotationZ(gameObject, _animationClip);
            case AnimationProperty.localScaleX:
                return new LocalScaleX(gameObject, _animationClip);
            case AnimationProperty.localScaleY:
                return new LocalScaleY(gameObject, _animationClip);
            case AnimationProperty.localScaleZ:
                return new LocalScaleZ(gameObject, _animationClip);
            default:
                return null;
        }

    }
}
