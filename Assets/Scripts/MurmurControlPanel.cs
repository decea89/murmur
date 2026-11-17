using UnityEngine;
using UnityEngine.InputSystem;

public sealed class MurmurControlPanel : MonoBehaviour
{
    public MurmurToroidalRing sculpture;
    public GameObject panel;
    public UnityEngine.UI.Slider radiusSlider;
    public UnityEngine.UI.Slider stiffnessSlider;
    public UnityEngine.UI.Slider roughnessSlider;
    public UnityEngine.UI.Text radiusLabel;
    public UnityEngine.UI.Text stiffnessLabel;
    public UnityEngine.UI.Text roughnessLabel;
    public UnityEngine.UI.Button resetButton;
    private float initialRadius, initialStiffness, initialRoughness;

    private void Awake()
    {
        initialRadius = sculpture.influenceRadius;
        initialStiffness = sculpture.returnStiffness;
        initialRoughness = sculpture.roughness;
        radiusSlider.onValueChanged.AddListener(SetRadius);
        stiffnessSlider.onValueChanged.AddListener(SetStiffness);
        roughnessSlider.onValueChanged.AddListener(SetRoughness);
        resetButton.onClick.AddListener(ResetValues);
        SyncControls();
    }

    public void SetRadius(float value) { sculpture.influenceRadius = value; RefreshLabels(); }
    public void SetStiffness(float value) { sculpture.returnStiffness = value; RefreshLabels(); }
    public void SetRoughness(float value) { sculpture.roughness = value; RefreshLabels(); }

    public void ResetValues()
    {
        sculpture.influenceRadius = initialRadius;
        sculpture.returnStiffness = initialStiffness;
        sculpture.roughness = initialRoughness;
        sculpture.Release();
        SyncControls();
    }

    private void SyncControls()
    {
        radiusSlider.SetValueWithoutNotify(sculpture.influenceRadius);
        stiffnessSlider.SetValueWithoutNotify(sculpture.returnStiffness);
        roughnessSlider.SetValueWithoutNotify(sculpture.roughness);
        RefreshLabels();
    }

    private void RefreshLabels()
    {
        radiusLabel.text = $"Radio del imán     {sculpture.influenceRadius:F2}";
        stiffnessLabel.text = $"Rigidez de retorno     {sculpture.returnStiffness:F0}";
        roughnessLabel.text = $"Rugosidad     {sculpture.roughness:F2}";
    }

    public void TogglePanel() { panel.SetActive(!panel.activeSelf); }
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.hKey.wasPressedThisFrame) TogglePanel();
    }

    private void OnDestroy()
    {
        radiusSlider.onValueChanged.RemoveListener(SetRadius);
        stiffnessSlider.onValueChanged.RemoveListener(SetStiffness);
        roughnessSlider.onValueChanged.RemoveListener(SetRoughness);
        resetButton.onClick.RemoveListener(ResetValues);
    }
}
