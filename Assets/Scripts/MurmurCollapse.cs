using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect), typeof(MurmurToroidalRing))]
public sealed class MurmurCollapse : MonoBehaviour
{
    [Range(0f, .7f)] public float bounce = .22f;
    [Range(0f, 15f)] public float friction = 7f;
    [Range(1f, 5f)] public float rebuildSpeed = 2.4f;
    public Renderer pedestal;
    public Renderer floor;
    public bool IsCollapsed { get; private set; }
    VisualEffect effect;
    MurmurToroidalRing ring;
    float recovery = 100f;
    bool hasRebuilt;
    public float CompletionPulse { get { float t=recovery-(1.25f+8f/Mathf.Max(rebuildSpeed,.1f)); return hasRebuilt && !IsCollapsed && t>0 && t<.9f ? Mathf.Sin(t/.9f*Mathf.PI) : 0; } }
    void Awake() { effect=GetComponent<VisualEffect>(); ring=GetComponent<MurmurToroidalRing>(); }
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame && Application.isFocused) Toggle();
        if (!IsCollapsed) recovery += Mathf.Min(Time.deltaTime,.05f);
        ring.InteractionLocked = IsCollapsed || recovery < 1.25f+8f/Mathf.Max(rebuildSpeed,.1f)+1.15f;
        if (ring.InteractionLocked) ring.Release();
        effect.SetFloat("Collapsed",IsCollapsed?1:0);
        effect.SetFloat("RecoveryTime",recovery);
        effect.SetFloat("RebuildSpeed",rebuildSpeed);
        effect.SetFloat("Bounce",bounce);
        effect.SetFloat("FloorFriction",friction);
        effect.SetVector3("WorldUpLocal",transform.InverseTransformDirection(Vector3.up));
        effect.SetVector3("WorldRightLocal",transform.InverseTransformDirection(Vector3.right));
        effect.SetVector3("WorldForwardLocal",transform.InverseTransformDirection(Vector3.forward));
        effect.SetVector3("SculptureOrigin",transform.position);
        var bounds=pedestal?pedestal.bounds:new Bounds(Vector3.zero,Vector3.zero);
        effect.SetVector3("PlinthMin",bounds.min);
        effect.SetVector3("PlinthMax",bounds.max);
        effect.SetFloat("FloorHeight",floor?floor.bounds.max.y:0f);
    }
    public void Toggle() { IsCollapsed=!IsCollapsed; hasRebuilt=!IsCollapsed; recovery=0; ring.Release(); }
    void OnDisable() { if(ring)ring.InteractionLocked=false;if(effect){effect.SetFloat("Collapsed",0);effect.SetFloat("RecoveryTime",10);} }
}
