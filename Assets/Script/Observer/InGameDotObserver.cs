using TMPro;
using UnityEngine;
using static ShapeDefenseSpace.GameData;

public class InGameDotObserver : SceneSingleton<InGameDotObserver>
{

    [SerializeField] private TMP_Text dot;
    [SerializeField] private TMP_Text upgrade_pannel_dot;
    [SerializeField] private TMP_Text combine_pannel_dot;
    

    [SerializeField] private TMP_Text need_dot;

    //private bool FirstSet = true;

    private void Start() {
        if (datahub.User.StatusStartDotLevel > 0) {
            datahub.Dot += datahub.Dot * datahub.User.StatusStartDotLevel * 0.05f;
        }
        int dot_value = (int)datahub.Dot;
        dot.text = dot_value.ToString();
        upgrade_pannel_dot.text = dot_value.ToString();
        combine_pannel_dot.text = dot_value.ToString();
    }

    public void DotObserver() {

        dot.text = (datahub.Dot).ToString("####0.#");
        upgrade_pannel_dot.text = (datahub.Dot).ToString("####0.#");
        combine_pannel_dot.text = (datahub.Dot).ToString("####0.#");

        // dot 소지하고 있기 업적 클리어
        if(datahub.Dot >= 1000) {
            // 1000이상이라면 일단 보내기
            achieve_observer.OwnDotCheck();
        }
    }

    public void NeedDotObserver() {
        need_dot.text = datahub.NeedDot.ToString();
    }
}
