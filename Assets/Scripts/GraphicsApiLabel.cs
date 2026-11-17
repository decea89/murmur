using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Text))]
public sealed class GraphicsApiLabel : MonoBehaviour
{
    private UnityEngine.UI.Text label;
    private string api;
    private double accumulatedTime;
    private int frames;
    public float MeanFrameMilliseconds { get; private set; }

    private void Awake()
    {
        label = GetComponent<UnityEngine.UI.Text>();
        api = SystemInfo.graphicsDeviceType.ToString();
        label.color = api == "WebGPU" ? new Color(0.65f, 0.95f, 0.75f) : new Color(1f, 0.75f, 0.5f);
        label.text = $"API: {api}\nTiempo de fotograma · media 1 s\nMidiendo…";
    }

    private void Update()
    {
        accumulatedTime += Time.unscaledDeltaTime;
        frames++;
        if (accumulatedTime < 1.0) return;
        MeanFrameMilliseconds = (float)(1000.0 * accumulatedTime / frames);
        label.text = $"API: {api}\nTiempo de fotograma · media 1 s\n{MeanFrameMilliseconds:F1} ms";
        accumulatedTime = 0;
        frames = 0;
    }

    private void OnApplicationFocus(bool focused)
    {
        accumulatedTime = 0;
        frames = 0;
    }
}
