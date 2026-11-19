using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Lives on the sculpture, so hiding UI cannot disable the shortcut itself.
public sealed class MurmurPresentation : MonoBehaviour
{
    Canvas[] canvases;
    GraphicRaycaster[] raycasters;
    bool[] canvasStates, raycasterStates;
    bool hidden;
    void Update()
    {
        if(Application.isFocused && Keyboard.current!=null && Keyboard.current.pKey.wasPressedThisFrame)Toggle();
    }
    public void Toggle()
    {
        if(hidden){Restore();return;}
        canvases=FindObjectsByType<Canvas>(FindObjectsInactive.Include,FindObjectsSortMode.None);
        raycasters=FindObjectsByType<GraphicRaycaster>(FindObjectsInactive.Include,FindObjectsSortMode.None);
        canvasStates=new bool[canvases.Length];raycasterStates=new bool[raycasters.Length];
        for(int i=0;i<canvases.Length;i++){canvasStates[i]=canvases[i].enabled;canvases[i].enabled=false;}
        for(int i=0;i<raycasters.Length;i++){raycasterStates[i]=raycasters[i].enabled;raycasters[i].enabled=false;}
        hidden=true;
    }
    void Restore()
    {
        if(!hidden)return;
        for(int i=0;i<canvases.Length;i++)if(canvases[i])canvases[i].enabled=canvasStates[i];
        for(int i=0;i<raycasters.Length;i++)if(raycasters[i])raycasters[i].enabled=raycasterStates[i];
        hidden=false;
    }
    void OnDisable(){Restore();}
}
