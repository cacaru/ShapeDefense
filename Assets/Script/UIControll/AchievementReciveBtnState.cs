using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ShapeDefenseSpace.PublicData;
public class AchievementReciveBtnState : MonoBehaviour
{
    //private readonly Color DISABLE_STATE = new(163 / 255f, 159 / 255f, 137 / 255f, 1f);
    //private readonly Color DISABLE_STATE = new(108 / 255f, 75 / 255f, 133 / 255f, 1f);
    //private readonly Color DISABLE_STATE = new(142 / 255f, 172 / 255f, 117 / 255f, 1f);

    //private readonly Color ACTIVATE_STATE = new(173 / 255f, 163 / 255f, 240 / 255f, 1f);
    //private readonly Color ACTIVATE_STATE = new(196 / 255f, 225 / 255f, 221 / 255f, 1f);
    //private readonly Color ACTIVATE_STATE = new(137 / 255f, 163 / 255f, 115 / 255f, 1f);
    //private readonly Color ACTIVATE_STATE = new(138 / 255f, 166 / 255f, 115 / 255f, 1f);
    //private readonly Color ACTIVATE_STATE = new(196 / 255f, 193 / 255f, 180 / 255f, 1f);


    [SerializeField] private GameObject Btn;

    public void ColorSetting(bool activate) {
        if (activate) {
            Btn.transform.Find("Text").gameObject.GetComponent<TMP_Text>().color = ACTIVE_TEXT;
            Btn.GetComponent<Image>().color = BTN_ACTIVATE_STATE;
        }
        else {
            Btn.transform.Find("Text").gameObject.GetComponent<TMP_Text>().color = DISABLE_TEXT;
            Btn.GetComponent<Image>().color = BTN_DISABLE_STATE;
        }
    }
}
