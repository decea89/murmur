using UnityEngine;

// Two moving spotlights; only the warm key renders a shadow map.
public sealed class MurmurLightChoreography : MonoBehaviour
{
    public Transform sculpture;
    public Light warmKey;
    public Light coolRim;
    [Range(0f, 1f)] public float speed = .22f;
    [Range(0f, 2f)] public float movement = 1f;
    [Range(0f, 150f)] public float keyIntensity = 70f;
    [Range(0f, 150f)] public float rimIntensity = 65f;
    public bool animate = true;
    float phase;
    MurmurCollapse rebuild;
    void Start(){if(sculpture)rebuild=sculpture.GetComponent<MurmurCollapse>();}

    void LateUpdate()
    {
        if (!sculpture) return;
        if (animate) phase += Mathf.Min(Time.deltaTime, .05f) * speed;
        Vector3 center = sculpture.position;
        if (warmKey)
        {
            warmKey.transform.position = center + new Vector3(-4f + Mathf.Sin(phase) * 2f * movement, 4f + Mathf.Sin(phase * .7f) * .6f * movement, -4f);
            warmKey.transform.LookAt(center + Vector3.down * .6f);
            warmKey.intensity = keyIntensity * (1f + .12f * Mathf.Sin(phase * 1.3f)) * (1f + (rebuild ? rebuild.CompletionPulse * .8f : 0));
        }
        if (coolRim)
        {
            coolRim.transform.position = center + new Vector3(4f, 1.5f + Mathf.Cos(phase * .85f) * 1.2f * movement, 2.5f + Mathf.Sin(phase * .6f) * movement);
            coolRim.transform.LookAt(center);
            coolRim.intensity = rimIntensity * (1f + .15f * Mathf.Sin(phase + 1.4f));
        }
    }
}
