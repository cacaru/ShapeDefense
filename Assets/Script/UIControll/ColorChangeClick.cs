using UnityEngine;
using UnityEngine.EventSystems;

public class ColorChangeClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject Picker_Obj;
    public void OnPointerClick(PointerEventData eventData) {
        string name = eventData.pointerCurrentRaycast.gameObject.name;

        switch (name) {
            case "TopColor":
                Picker_Obj.GetComponent<ColorPicker>().Type = 1;
                break;
            case "BottomColor":
                Picker_Obj.GetComponent<ColorPicker>().Type = 2;
                break;
            case "IconBackColor":
                Picker_Obj.GetComponent<ColorPicker>().Type = 3;
                break;
            case "IconColor":
                Picker_Obj.GetComponent<ColorPicker>().Type = 4;
                break;
        }
        Picker_Obj.GetComponent<ColorPicker>().PickerOpen();
        Picker_Obj.SetActive(true);
    }

}
