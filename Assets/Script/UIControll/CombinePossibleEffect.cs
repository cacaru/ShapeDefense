using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinePossibleEffect : MonoBehaviour
{

    private readonly Color DEFAULT = new(111 / 255f, 190 / 255f, 78 / 255f, 0f);
    private readonly Color ON = new(111 / 255f, 190 / 255f, 78 / 255f, 1f);

    public void OnImageSetting(bool val) {
        // ¿ÃπÃ¡ˆ alpha on
        if (val) {
            gameObject.GetComponent<Image>().color = ON;
        }
        else {
            gameObject.GetComponent<Image>().color = DEFAULT;
        }
    }

}
