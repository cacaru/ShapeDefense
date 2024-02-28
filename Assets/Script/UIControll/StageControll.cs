using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageControll : MonoBehaviour
{
    [SerializeField] private GameObject ImageObj;
    [SerializeField] private GameObject PreBtn;
    [SerializeField] private GameObject NextBtn;
    [SerializeField] private TMP_Text StageText;

    private DataHub datahub;

    private Color pre_color;
    private Color next_color;
    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
        pre_color = PreBtn.GetComponent<Image>().color;
        next_color = NextBtn.GetComponent<Image>().color;
    }


    public void StageChangeNextBtnClick() {
        datahub.StageNumber++;
        // 현재 최대 스테이지 갯수 2
        if (datahub.StageNumber >= 2) {
            datahub.StageNumber = 2;
            next_color.a = 0;
            NextBtn.GetComponent<Image>().color = next_color;

            pre_color.a = 1;
            PreBtn.GetComponent<Image>().color = pre_color;
        }

        Setting();
    }

    public void StageChangePresBtnClick() {
        datahub.StageNumber--;

        // 현재 최저 스테이지 번호 1
        if (datahub.StageNumber <= 1) {
            datahub.StageNumber = 1;
            next_color.a = 1;
            pre_color.a = 0;
            
            NextBtn.GetComponent<Image>().color = next_color;
            PreBtn.GetComponent<Image>().color = pre_color;
        }

        Setting();
    }

    private void Setting() {
        StageText.text = "스테이지 " + datahub.StageNumber.ToString();
        ImageObj.GetComponent<ImageChanger>().StageNumber = datahub.StageNumber;
    }

}
