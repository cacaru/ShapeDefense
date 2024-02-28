using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    private int stage_number;

    public int StageNumber {
        get { return stage_number; }
        set {
            stage_number = value;

            string path = "";
            switch (stage_number) {
                case 0:
                    break;
                case 1:
                    path = "Sprite/Stage/stage_1";
                    break;
                case 2:
                    path = "Sprite/Stage/stage_2";
                    break;
            }

            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);

        }
    }
}
