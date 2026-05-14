using System.Collections;

using TMPro;
using UnityEngine;
using static ShapeDefenseSpace.GameData;

public class StaminaLeftTime : MonoBehaviour
{
    [SerializeField] private TMP_Text Left;

    public void ShowLeftTime() {
        int min = datahub.RemainTime / 60;
        int sec = datahub.RemainTime % 60;
        Left.text = string.Format("{0}:{1:D2}", min, sec);
        Left.gameObject.SetActive(true);
        StartCoroutine(ShowTime());
        StartCoroutine(ShowTimeObj());
    }

    IEnumerator ShowTimeObj() {
        yield return wfsr_5;
        Left.gameObject.SetActive(false);
    }

    IEnumerator ShowTime() {
        int counter = 0;
        while (true) {
            counter++;
            if (counter >= 6) {
                break;
            }
            yield return wfs_1;
            int min = datahub.RemainTime / 60;
            int sec = datahub.RemainTime % 60;
            Left.text = string.Format("{0}:{1:D2}", min, sec);
        }
        
    }
}
