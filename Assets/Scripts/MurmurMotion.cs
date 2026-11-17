using System;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

[RequireComponent(typeof(VisualEffect),typeof(MurmurToroidalRing))]
public sealed class MurmurMotion : MonoBehaviour
{
    [Range(0f,.5f)] public float amplitude=.22f;
    [Range(.1f,2f)] public float speed=.65f;
    [Range(0f,45f)] public float twist=22f;
    public bool motionEnabled=true;
    private VisualEffect effect;
    private MurmurToroidalRing ring;
    private float phase;
    private float blend=1;
    private float[] samples;
    private int sampleCount;
    private float sampleElapsed;
    private string sampleName;
    private void Awake(){effect=GetComponent<VisualEffect>();ring=GetComponent<MurmurToroidalRing>();}
    private void Update()
    {
        if(Keyboard.current!=null&&Keyboard.current.spaceKey.wasPressedThisFrame)motionEnabled=!motionEnabled;
        if(!ring.IsDragging)phase+=Mathf.Min(Time.deltaTime,.05f)*speed;
        blend=Mathf.MoveTowards(blend,motionEnabled?1:0,Time.deltaTime*2);
        effect.SetFloat("MotionTime",phase);
        effect.SetFloat("MotionAmplitude",amplitude*blend);
        effect.SetFloat("MotionTwist",twist*blend);
        if(samples!=null && Time.unscaledDeltaTime>0){if(sampleCount<samples.Length)samples[sampleCount++]=Time.unscaledDeltaTime*1000;sampleElapsed+=Time.unscaledDeltaTime;if(sampleElapsed>=12)FinishBenchmark();}
    }
    public void SetMotionEnabled(string value){motionEnabled=value!="0";}
    // Invoked on demand by browser verification; no per-frame logging.
    public void BeginBenchmark(string label){sampleName=label;samples=new float[8192];sampleCount=0;sampleElapsed=0;}
    private void FinishBenchmark()
    {
        var sorted=new float[sampleCount];Array.Copy(samples,sorted,sampleCount);Array.Sort(sorted);
        var urp=UniversalRenderPipeline.asset;
        Debug.Log("MURMUR_BENCH "+JsonUtility.ToJson(new Measurement{name=sampleName,frames=sampleCount,meanMs=sampleElapsed*1000/sampleCount,p95Ms=sorted[Mathf.Min(sampleCount-1,Mathf.FloorToInt(sampleCount*.95f))],width=Screen.width,height=Screen.height,renderScale=urp.renderScale,api=SystemInfo.graphicsDeviceType.ToString(),particles=effect.aliveParticleCount,motion=motionEnabled}));samples=null;
    }
    [Serializable] private struct Measurement{public string name;public int frames;public float meanMs,p95Ms;public int width,height;public float renderScale;public string api;public int particles;public bool motion;}
}
