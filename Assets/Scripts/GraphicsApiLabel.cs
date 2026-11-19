using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Text))]
public sealed class GraphicsApiLabel : MonoBehaviour
{
    UnityEngine.UI.Text label;
    string api;
    double accumulatedTime;
    int frames;
    public float MeanFrameMilliseconds { get; private set; }
    void Awake()
    {
        label=GetComponent<UnityEngine.UI.Text>();
        api=SystemInfo.graphicsDeviceType.ToString();
        label.color=api=="WebGPU"?new Color(.65f,.95f,.75f):new Color(1f,.75f,.5f);
        label.text=$"API: {api}\nFPS · media 1 s\nMidiendo…";
    }
    void Update()
    {
        if(!Application.isFocused)return;
        accumulatedTime+=Time.unscaledDeltaTime;frames++;
        if(accumulatedTime<1)return;
        MeanFrameMilliseconds=(float)(1000*accumulatedTime/frames);
        label.text=$"API: {api}\n{frames/accumulatedTime:F0} FPS · media 1 s";
        accumulatedTime=0;frames=0;
    }
    void OnApplicationFocus(bool focused){accumulatedTime=0;frames=0;}
}
