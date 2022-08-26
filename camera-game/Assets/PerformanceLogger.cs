using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private float fps { get { return Time.frameCount / Time.time; } }

    private bool recording = true;
    private List<PerformanceCapture> performanceCaptures = new List<PerformanceCapture>();
    private static readonly HttpClient client = new HttpClient();

    public void Record()
    {
        PerformanceCapture pc = new PerformanceCapture(Time.time, fps);
        performanceCaptures.Add(pc);
    }

    private void Start()
    {
        StartCoroutine(FinishedRecording());
        StartCoroutine(RecordLoop());
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
