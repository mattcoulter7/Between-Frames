using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewinder : MonoBehaviour
{
    static public Rewinder Instance;
    public List<History> historyComponents;
    public float rewindSpeed = 1;
    void Awake(){
        Instance = this;
    }
    List<History.HistoryItem> historyItems = new List<History.HistoryItem>();
    // Update is called once per frame
    List<History.HistoryItem> GetHistoryItems(){
        historyItems.Clear();
        // flat map of all history component history arrays
        foreach (var historyComponent in historyComponents){
            historyItems.AddRange(historyComponent.history);
        }
        historyItems.Sort((History.HistoryItem a,History.HistoryItem b) => a.frameCount < b.frameCount ? 1 : -1);
        return historyItems;
    }
    IEnumerator Rewind(){
        int startFrame = Time.frameCount;
        int highestFrameCount = historyItems[0].frameCount;
        while (historyItems.Count > 0){
            int currentFrame = Time.frameCount;
            int frameProgress = currentFrame - startFrame;
            History.HistoryItem currentHistoryItem = historyItems[0];
            float frameOffset = (highestFrameCount - currentHistoryItem.frameCount) / rewindSpeed;
            if (frameProgress >= frameOffset){
                currentHistoryItem.Load();
                historyItems.RemoveAt(0);
            } else {
                yield return null;
            }
        }
        EventDispatcher.Instance.Dispatch("OnRewindFinish",this);
    }
    public void BeginRewind()
    {
        GetHistoryItems();
        StartCoroutine(Rewind());
    }
}
