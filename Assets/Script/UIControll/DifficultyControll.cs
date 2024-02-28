using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyControll : MonoBehaviour
{
    [SerializeField] private GameObject PreBtn;
    [SerializeField] private GameObject NextBtn;
    [SerializeField] private TMP_Text DifficultyText;

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
    // 난이도 다음 버튼 클릭
    public void DifficultyNextBtnClick() {
        datahub.Difficulty++;
        // 8을 최대로 설정
        if (datahub.Difficulty > 8) { 
            datahub.Difficulty = 8; 
        }
        // 8이면 다음 버튼이 보이지 않도록 설정
        else if (datahub.Difficulty == 8) {
            next_color.a = 0;
            NextBtn.GetComponent<Image>().color = next_color;
        }
        // 2이상부터는 이전 버튼이 보이도록 설정
        else if (datahub.Difficulty < 8 && datahub.Difficulty >= 2) {
            pre_color.a = 1;
            PreBtn.GetComponent<Image>().color = pre_color;
        }

        DifficultyText.text = datahub.Difficulty.ToString();
    }
    public void DifficultyPreBtnClick() {
        datahub.Difficulty--;
        // 1을 최솟값으로 설정
        if (datahub.Difficulty <= 0) datahub.Difficulty = 1;
        // 1이면 이전 버튼이 보이지 않도록 설정
        else if (datahub.Difficulty == 1) {
            pre_color.a = 0;
            PreBtn.GetComponent<Image>().color = pre_color;
        }
        // 2 이상이면 이전 버튼이 보이도록 설정
        else if (datahub.Difficulty > 1) {
            next_color.a = 1;
            NextBtn.GetComponent<Image>().color = next_color;
        }
        DifficultyText.text = datahub.Difficulty.ToString();
    }
}
