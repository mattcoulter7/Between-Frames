using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBars : MonoBehaviour
{
    public float scrollMultiplier = 1f;
    public Vector3 _pivotPoint = new Vector3(0,0,0);
    private Blackbar[] _blackBars = new Blackbar[]{};
    // Start is called before the first frame update
    void Start()
    {
        _blackBars = GetComponentsInChildren<Blackbar>();
        // pivot point defaults to avergae of all children
        Vector3 averagePos = new Vector3(0,0,0);
        foreach (Blackbar bar in _blackBars){
            averagePos += bar.transform.position;
        }
        averagePos /= _blackBars.Length;
        _pivotPoint = averagePos;
    }

    // Update is called once per frame
    void Update()
    {
        // Resize the objects based on scroll input
        /*float scrollAmount = Input.mouseScrollDelta.y;
        if (scrollAmount != 0f){
            foreach (Blackbar bar in _blackBars){
                Vector3 barToPivot = _pivotPoint - bar.GetAnchorWorldPos();
                float toPivotDist = barToPivot.magnitude; // multiplier 1
                Vector3 newLocalScale = bar.transform.localScale;
                newLocalScale.y += scrollAmount * toPivotDist * scrollMultiplier;
                if (newLocalScale.y > 0){
                    bar.transform.localScale = newLocalScale;
                }
            }
        }*/

        // Handle Updating Pivot Point
        
        // ensure anchor position
    }
}
