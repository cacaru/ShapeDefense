using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ShapeDefenseSpace.GameData;

public class StaminaShow : SceneSingleton<StaminaShow>
{
    [SerializeField] private GameObject stamina_slider;
    [SerializeField] private TMP_Text stamina_text;
    [SerializeField] private GameObject StartBtn;

    // Start is called before the first frame update
    void Start()
    {
        // 스테미나 설정
        float v = (float)datahub.User.Stamina / datahub.User.MaxStamina;
        stamina_slider.GetComponent<Slider>().value = v;
        stamina_text.text = datahub.User.Stamina.ToString();
    }
    public void ReShow() {
        // 스테미나 설정
        float v = (float)datahub.User.Stamina / datahub.User.MaxStamina;
        stamina_slider.GetComponent<Slider>().value = v;
        stamina_text.text = datahub.User.Stamina.ToString();
    }
}
