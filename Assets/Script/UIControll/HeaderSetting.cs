using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeaderSetting : MonoBehaviour
{
    // header 영역 객체 연결
    [SerializeField]
    private GameObject header;
    // 정보가 저장된 허브 연결
    private DataHub datahub;

    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();

        // data 연결
        // 이름
        header.transform.Find("Name").gameObject.GetComponent<TMP_Text>().text = datahub.User.Nickname;
        // 레벨
        header.transform.Find("Level").gameObject.GetComponent<TMP_Text>().text = datahub.User.Level.ToString();
        // 골드
        header.transform.Find("Gold").gameObject.GetComponent<TMP_Text>().text = datahub.User.Dot.ToString();
        // 경험치량
        header.transform.Find("EXP").gameObject.GetComponent<Slider>().value = datahub.User.Experience > 0 ? datahub.User.Experience / 100 : 0;
        header.transform.Find("EXP_Text").gameObject.GetComponent<TMP_Text>().text = datahub.User.Experience.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
