using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;

public class DifficultyControll : MonoBehaviour
{
    [SerializeField] private GameObject PreBtn;
    [SerializeField] private GameObject NextBtn;
    [SerializeField] private TMP_Text DifficultyText;
    [SerializeField] private TMP_Text Use_stamina;

    private Color pre_color;
    private Color next_color;
    // Start is called before the first frame update
    void Start()
    {
        pre_color = PreBtn.GetComponent<Image>().color;
        next_color = NextBtn.GetComponent<Image>().color;
        LoadDifficulty();
    }

    // 난이도 다음 버튼 클릭
    public void DifficultyNextBtnClick() {
        datahub.Difficulty += 1;
        SetBtn();
        WillUseingStaminaSet();
    }
    public void DifficultyPreBtnClick() {
        datahub.Difficulty -= 1;
        SetBtn();
        WillUseingStaminaSet();
    }

    private void SetBtn() {
        // 최종을 최대로 설정
        if (datahub.Difficulty >= MAX_Difficulty) {
            datahub.Difficulty = MAX_Difficulty;
            next_color.a = 0;
            NextBtn.GetComponent<Image>().color = next_color;
        }
        // 최종이면 다음 버튼이 보이지 않도록 설정
        else if (datahub.Difficulty <= 1) {
            datahub.Difficulty = 1;
            pre_color.a = 0;
            PreBtn.GetComponent<Image>().color = pre_color;
        }
        // 2이상부터는 이전 버튼이 보이도록 설정
        else if (datahub.Difficulty < MAX_Difficulty && datahub.Difficulty >= 2) {
            pre_color.a = 1;
            PreBtn.GetComponent<Image>().color = pre_color;

            next_color.a = 1;
            NextBtn.GetComponent<Image>().color = next_color;
        }

        DifficultyText.text = datahub.Difficulty.ToString();
    }

    public void LoadDifficulty() {
        SetBtn();
        WillUseingStaminaSet();
    }

    private void WillUseingStaminaSet() {
        int dif = int.Parse(DifficultyText.text) - 1;
        Use_stamina.text = (4 + dif).ToString();
    }
}
