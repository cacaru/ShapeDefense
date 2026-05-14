using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    private int stage_number;

    private readonly string STAGE1 = "Sprite/Stage/stage_1";
    private readonly string STAGE2 = "Sprite/Stage/stage_2";
    private readonly string STAGE3 = "Sprite/Stage/stage_3";
    private readonly string STAGE4 = "Sprite/Stage/stage_4";
    private readonly string STAGE5 = "Sprite/Stage/stage_5";
    private readonly string STAGE6 = "Sprite/Stage/stage_6";
    private readonly string STAGE7 = "Sprite/Stage/stage_7";
    private readonly string STAGE8 = "Sprite/Stage/stage_8";


    public int StageNumber {
        get { return stage_number; }
        set {
            stage_number = value;

            string path = "";
            switch (stage_number) {
                case 0:
                    break;
                case 1:
                    path = STAGE1;
                    break;
                case 2:
                    path = STAGE2;
                    break;
                case 3:
                    path = STAGE3;
                    break;
                case 4:
                    path = STAGE4;
                    break;
                case 5:
                    path = STAGE5;
                    break;
                case 6:
                    path = STAGE6;
                    break;
                case 7:
                    path = STAGE7;
                    break;
                case 8:
                    path = STAGE8;
                    break;
            }
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);

        }
    }
}
