using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using UnityEngine.EventSystems;
using System.Collections.Generic;

// Only scalar/vector controls cross the CPU/GPU boundary; particles stay in VFX Graph.
[RequireComponent(typeof(VisualEffect))]
public sealed class MurmurToroidalRing : MonoBehaviour
{
    [Min(0.05f), Tooltip("Radius in local sculpture units, captured when pressing.")]
    public float influenceRadius = 1.65f;
    [Min(0.01f), Tooltip("Spring stiffness; critical damping prevents sustained oscillation.")]
    public float returnStiffness = 64f;
    [Range(0f, 1f)] public float roughness = 0.32f;
    public Camera interactionCamera;
    private VisualEffect effect;
    private Plane dragPlane;
    private Vector3 grabStart;
    private Vector3 grabDelta;
    private float capturedRadius;
    private readonly List<RaycastResult> uiHits = new List<RaycastResult>();
    public bool IsDragging { get; private set; }

    private void OnEnable()
    {
        effect = GetComponent<VisualEffect>();
        if (!interactionCamera) interactionCamera = Camera.main;
        Release();
    }

    public bool BeginGrab(Vector2 screenPosition)
    {
        if (!interactionCamera) return false;
        if (EventSystem.current != null)
        {
            uiHits.Clear();
            EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) { position = screenPosition }, uiHits);
            if (uiHits.Count > 0) return false;
        }
        dragPlane = new Plane(interactionCamera.transform.forward, transform.position);
        if (!Project(screenPosition, out grabStart)) return false;
        capturedRadius = Mathf.Max(0.05f, influenceRadius);
        grabDelta = Vector3.zero;
        IsDragging = true;
        SendControls();
        return true;
    }

    public void DragTo(Vector2 screenPosition)
    {
        if (IsDragging && Project(screenPosition, out var point))
            grabDelta = Vector3.ClampMagnitude(point - grabStart, 6f);
        SendControls();
    }

    private bool Project(Vector2 screenPosition, out Vector3 local)
    {
        var ray = interactionCamera.ScreenPointToRay(screenPosition);
        if (dragPlane.Raycast(ray, out float distance))
        {
            local = transform.InverseTransformPoint(ray.GetPoint(distance));
            return true;
        }
        local = default;
        return false;
    }

    public void Release()
    {
        IsDragging = false;
        grabDelta = Vector3.zero;
        SendControls();
    }

    private void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null || !Application.isFocused) Release();
        else
        {
            var point = mouse.position.ReadValue();
            if (mouse.leftButton.wasPressedThisFrame) BeginGrab(point);
            if (IsDragging)
            {
                if (!mouse.leftButton.isPressed) Release();
                else DragTo(point);
            }
        }
        SendControls();
    }

    private void SendControls()
    {
        if (!effect || !effect.visualEffectAsset) return;
        effect.SetVector3("GrabStart", grabStart);
        effect.SetVector3("GrabDelta", grabDelta);
        effect.SetFloat("GrabActive", IsDragging ? 1f : 0f);
        effect.SetFloat("InfluenceRadius", IsDragging ? capturedRadius : Mathf.Max(0.05f, influenceRadius));
        effect.SetFloat("ReturnStiffness", Mathf.Max(0.01f, returnStiffness));
        effect.SetFloat("StepTime", Mathf.Min(Time.deltaTime, 0.05f));
        effect.SetFloat("Smoothness", 1f - Mathf.Clamp01(roughness));
    }

    private void OnApplicationFocus(bool focus) { if (!focus) Release(); }
    private void OnApplicationPause(bool paused) { if (paused) Release(); }
    private void OnDisable() { Release(); }
    public void RestoreToRest() { Release(); if (effect) effect.Reinit(); }
}
