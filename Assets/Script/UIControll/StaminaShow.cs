using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaminaShow : MonoBehaviour
{
    [SerializeField]
    private GameObject stamina_slider;
    [SerializeField]
    private TMP_Text stamina_text;

    private DataHub datahub;

    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();

        // 스테미나 설정
        float v = (float)datahub.User.Stamina / datahub.User.MaxStamina;
        stamina_slider.GetComponent<Slider>().value = v;
        stamina_text.text = datahub.User.Stamina.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReShow() {
        // 스테미나 설정
        float v = (float)datahub.User.Stamina / datahub.User.MaxStamina;
        stamina_slider.GetComponent<Slider>().value = v;
        stamina_text.text = datahub.User.Stamina.ToString();
    }
}
