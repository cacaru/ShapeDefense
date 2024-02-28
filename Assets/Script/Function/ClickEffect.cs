using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField]
    private Color down;
    [SerializeField]
    private Color up;
    [SerializeField]
    private Color enter;

    private float alpha_value;
    private Color default_value;

    void Start() {
        default_value = gameObject.GetComponent<Image>().color;
        alpha_value = gameObject.GetComponent<Image>().color.a;
        down.a = alpha_value;
        up.a = alpha_value;
        enter.a = alpha_value;
    }

    public void OnPointerDown(PointerEventData eventData) {
        gameObject.GetComponent<Image>().color = down;
    }

    public void OnPointerUp(PointerEventData eventData) {
        gameObject.GetComponent<Image>().color = up;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        gameObject.GetComponent<Image>().color = enter;
    }

    public void OnPointerExit(PointerEventData eventData) {
        gameObject.GetComponent<Image>().color = default_value;
    }
}
