using TMPro;
using UnityEngine;

using static ShapeDefenseSpace.GameData;

public class CoreObserver : SceneSingleton<CoreObserver>
{
    [SerializeField] private TMP_Text core_text;
    [SerializeField] private TMP_Text unicore_text;
    [SerializeField] private GameObject Btn;

    private void Start() {
        GetCoreOn();
    }

    public void GetCoreOn() {

        core_text.text = datahub.CoreCount.ToString();
        unicore_text.text = datahub.UnicoreCount.ToString();

        if(datahub.CoreCount <= 0 && datahub.UnicoreCount <= 0) {
            Btn.SetActive(false);
            return;
        }

        Btn.SetActive(true);
    }
}
