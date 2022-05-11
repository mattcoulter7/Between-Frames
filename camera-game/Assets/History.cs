using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class History : MonoBehaviour
{
    public List<HistoryItem> history = new List<HistoryItem>();
    [System.Serializable]
    public abstract class HistoryItem
    {
        protected GameObject gameObject;
        public int frameCount;
        public HistoryItem(GameObject _gameObject)
        {
            gameObject = _gameObject;
            frameCount = Time.frameCount;
        }
        public abstract void Load();
    }
    [System.Serializable]
    public class TransformHistoryItem : HistoryItem
    {
        public Vector3 position;
        public Vector3 scale;
        public Quaternion rotation;
        public TransformHistoryItem(GameObject _gameObject, Transform _transform) : base(_gameObject)
        {
            position = new Vector3(_transform.position.x, _transform.position.y, _transform.position.z);
            scale = new Vector3(_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
            rotation = new Quaternion(_transform.rotation.x, _transform.rotation.y, _transform.rotation.z, _transform.rotation.w);
        }

        public override void Load()
        {
            gameObject.transform.position = position;
            gameObject.transform.localScale = scale;
            gameObject.transform.rotation = rotation;
        }
    }
    public bool transformHistory = true; // record the transform changes
    public int frameRecordInterval = 10; // record every 10 frames
    void Update()
    {
        if (Time.frameCount % 10 == 0){
            if (transformHistory){
                history.Add(new TransformHistoryItem(gameObject,transform));
            }
        }
    }
}
