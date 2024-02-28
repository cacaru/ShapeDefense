using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeaderSetting : MonoBehaviour
{
    // header ���� ��ü ����
    [SerializeField]
    private GameObject header;
    // ������ ����� ��� ����
    private DataHub datahub;

    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();

        // data ����
        // �̸�
        header.transform.Find("Name").gameObject.GetComponent<TMP_Text>().text = datahub.User.Nickname;
        // ����
        header.transform.Find("Level").gameObject.GetComponent<TMP_Text>().text = datahub.User.Level.ToString();
        // ���
        header.transform.Find("Gold").gameObject.GetComponent<TMP_Text>().text = datahub.User.Dot.ToString();
        // ����ġ��
        header.transform.Find("EXP").gameObject.GetComponent<Slider>().value = datahub.User.Experience > 0 ? datahub.User.Experience / 100 : 0;
        header.transform.Find("EXP_Text").gameObject.GetComponent<TMP_Text>().text = datahub.User.Experience.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
