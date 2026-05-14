using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;

public class CombineCheckOptionControll : MonoBehaviour
{
    [SerializeField] TMP_Text explain;
    [SerializeField] GameObject check_btn;

    // Start is called before the first frame update
    void Start()
    {
        SetOption();
    }

    public void CombineCheckOptionChange() {

        datahub.CombineCheckOption = !datahub.CombineCheckOption;
        SetOption();
    }

    private void SetOption() {
        if (datahub.CombineCheckOption) {
            explain.text = CombineCheckOptionON;
            check_btn.transform.Find("Label").gameObject.GetComponent<Text>().text = "조합 확인";
        }
        else {
            explain.text = CombineCheckOptionOFF;
            check_btn.transform.Find("Label").gameObject.GetComponent<Text>().text = "바로 조합";
        }
    }
}
