using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect),typeof(MurmurToroidalRing))]
public sealed class MurmurShapes : MonoBehaviour
{
    public Button[] buttons;
    [Range(.3f,4f)] public float transitionSpeed=1.8f;
    Vector4 weights=new Vector4(1,0,0,0);
    int selected;
    VisualEffect effect;
    MurmurToroidalRing ring;
    void Awake(){effect=GetComponent<VisualEffect>();ring=GetComponent<MurmurToroidalRing>();}
    void OnEnable(){if(buttons==null||buttons.Length!=4)return;buttons[0].onClick.AddListener(Ring);buttons[1].onClick.AddListener(Pentagon);buttons[2].onClick.AddListener(Star);buttons[3].onClick.AddListener(Hand);Refresh();}
    void OnDisable(){if(buttons==null||buttons.Length!=4)return;buttons[0].onClick.RemoveListener(Ring);buttons[1].onClick.RemoveListener(Pentagon);buttons[2].onClick.RemoveListener(Star);buttons[3].onClick.RemoveListener(Hand);}
    void Ring(){Select(0);} void Pentagon(){Select(1);} void Star(){Select(2);} void Hand(){Select(3);}
    public void Select(int shape){selected=Mathf.Clamp(shape,0,3);ring.Release();Refresh();}
    void Refresh(){if(buttons==null)return;for(int i=0;i<buttons.Length;i++)if(buttons[i]){var colors=buttons[i].colors;colors.normalColor=i==selected?new Color(.65f,.49f,.29f):new Color(.24f,.28f,.33f);colors.selectedColor=colors.normalColor;buttons[i].colors=colors;}}
    void Update(){Vector4 target=Vector4.zero;target[selected]=1;weights=Vector4.Lerp(weights,target,1-Mathf.Exp(-transitionSpeed*Mathf.Min(Time.deltaTime,.05f)));effect.SetVector4("ShapeWeights",weights);}
}
