using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ShapeDefenseSpace;
public class StageControll : MonoBehaviour
{
    [SerializeField] private GameObject ImageObj;
    [SerializeField] private GameObject PreBtn;
    [SerializeField] private GameObject NextBtn;
    [SerializeField] private TMP_Text StageText;

    private Color pre_color;
    private Color next_color;

    private int stage_number = 1;
    // Start is called before the first frame update
    void Start()
    {
        pre_color = PreBtn.GetComponent<Image>().color;
        next_color = NextBtn.GetComponent<Image>().color;
    }


    public void StageChangeNextBtnClick() {
        //datahub.StageNumber++;
        stage_number++;
        BtnControll();
        Setting();
    }

    public void StageChangePresBtnClick() {
        //datahub.StageNumber--;
        stage_number--;
        BtnControll();
        Setting();
    }

    private void Setting() {
        StageText.text = UtilityHub.query_builder.Append("스테이지 ")
                                                 .Append(stage_number.ToString())
                                                 .ToString();
        UtilityHub.query_builder.Clear();
        ImageObj.GetComponent<ImageChanger>().StageNumber = stage_number;
    }

    private void BtnControll() {
        // default -> 양쪽 전부 켜기
        next_color.a = 1;
        NextBtn.GetComponent<Image>().color = next_color;

        pre_color.a = 1;
        PreBtn.GetComponent<Image>().color = pre_color;

        // 최대까지 가면 next btn 지우기
        if (stage_number >= 8) {
            stage_number = 8;
            next_color.a = 0;
            NextBtn.GetComponent<Image>().color = next_color;

        }
        // 최소라면 pre btn 지우기
        else if(stage_number <= 1) {
            stage_number = 1;
            pre_color.a = 0;
            PreBtn.GetComponent<Image>().color = pre_color;
        }
    }

}
