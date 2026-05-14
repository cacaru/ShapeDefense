using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;

public class RollCheckOptionControll : MonoBehaviour
{
    [SerializeField] TMP_Text explain;
    [SerializeField] GameObject check_btn;

    // Start is called before the first frame update
    void Start()
    {
        SetOption();
    }

    public void RollCheckOptionChange() {

        datahub.RollCheckOption = !datahub.RollCheckOption;
        SetOption();
    }

    private void SetOption() {
        if (datahub.RollCheckOption) {
            explain.text = RollCheckOptionON;
            check_btn.transform.Find("Label").gameObject.GetComponent<Text>().text = "변환 확인";
        }
        else {
            explain.text = RollCheckOptionOFF;
            check_btn.transform.Find("Label").gameObject.GetComponent<Text>().text = "바로 변환";
        }
    }
}
