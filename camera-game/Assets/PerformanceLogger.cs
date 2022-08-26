using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

/*
fetch('https://script.google.com/macros/s/AKfycbydgqil1iZ_xQzxC7SI_dejPYgVoTz8UVue0agNg7tY5BvYoxKRPTQmXImqSvp6TI8q/exec',{
    method: "POST",
    headers: {
        'Content-Type': 'application/json',
    },
    body: JSON.stringify({
        output:[{
            'FPS':'100',
            'Time':'0'
        }
        ]
    })
})
*/

public class PerformanceLogger : MonoBehaviour
{
    public struct PerformanceCapture
    {
        public float time;
        public float fps;
        public PerformanceCapture(float time,float fps)
        {
            this.time = time;
            this.fps = fps;
        }
        public string toJSONString()
        {
            return "{" + 
                "\"Time\": " + time.ToString() + ", " + 
                "\"FPS\": " + fps.ToString() +
            "}";
        }
    }

    /// <summary>
    /// Duration (in seconds) of performance monitoring
    /// </summary>
    public float recordDuration = 60f;
    public float recordInterval = 0.25f;

    private int accumFrames = 0;
    private float accumTime = 0f;

    private float fps { get { return Time.frameCount / Time.time; } }

    private bool recording = true;
    private List<PerformanceCapture> performanceCaptures = new List<PerformanceCapture>();
    private static readonly HttpClient client = new HttpClient();

    public void Record()
    {
        PerformanceCapture pc = new PerformanceCapture(Time.time, accumFrames / accumTime);
        accumFrames = 0;
        accumTime = 0f;

        performanceCaptures.Add(pc);
    }

    private void Start()
    {
        StartCoroutine(FinishedRecording());
        StartCoroutine(RecordLoop());
    }
    private void Update()
    {
        accumFrames += 1;
        accumTime += Time.deltaTime;
    }

    private IEnumerator FinishedRecording()
    {
        yield return new WaitForSeconds(recordDuration);
        recording = false;
        PostData();
    }
    private IEnumerator RecordLoop()
    {
        while (recording)
        {
            yield return new WaitForSeconds(recordInterval);
            Record();
        }
    }
    
    /*private void SaveProfilerData()
    {

        var firstFrameIndex = ProfilerDriver.firstFrameIndex;
        var lastFrameIndex = ProfilerDriver.lastFrameIndex;
        var profilerSortColumn = ProfilerColumn.TotalTime;
        var viewType = ProfilerViewType.Hierarchy;

        var profilerData = new ProfilerData();
        for (int frameIndex = firstFrameIndex; frameIndex <= lastFrameIndex; ++frameIndex)
        {
            var property = new ProfilerProperty();
            property.SetRoot(frameIndex, profilerSortColumn, viewType);
            property.onlyShowGPUSamples = false;
            bool enterChildren = true;

            while (property.Next(enterChildren))
            {
                // get all the desired ProfilerColumn
                var name = property.GetColumn(ProfilerColumn.FunctionName);
                var totalTime = property.GetColumn(ProfilerColumn.TotalTime);
                // store values somewhere
            }

            property.Cleanup();
        }
    }
    */

    private async Task PostData()
    {
        string content = "[" + string.Join(",\n", performanceCaptures.Select(pc => pc.toJSONString())) + "]";

        FormUrlEncodedContent data = new FormUrlEncodedContent(
            new Dictionary<string, string> {
                { "output",content}
            }
        );

        var endpoint = "https://script.google.com/macros/s/AKfycbw154TEOX1M7i3zROsQlrh53TYsMFnZLHk06p2Tz26x9pAvaBWlhCapcX2xUlHlYvM/exec";
        var response = await client.PostAsync(endpoint, data);

        var responseString = await response.Content.ReadAsStringAsync();
        Debug.Log(responseString);
    }
}
