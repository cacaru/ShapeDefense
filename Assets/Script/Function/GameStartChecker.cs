using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameStartChecker : MonoBehaviour, IPointerClickHandler {

    private DataHub datahub;

    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
    }


    public void OnPointerClick(PointerEventData eventData) {
        GameObject now = eventData.pointerCurrentRaycast.gameObject;
        if (now != null) {
            string name = now.name;
            if (name == "Start_btn") {
                // ���׹̳� üũ
                if (datahub.User.Stamina >= 4) {
                    // ���׹̳ʸ� 4 �Һ��ϰ� �� �̵�
                    // db �� ������ ����
                    string query = "UPDATE user SET stamina = " + (datahub.User.Stamina-4).ToString();
                    //Debug.Log(query);
                    GameObject.Find("GameObject").GetComponent<ModifyDB>().ControllDB(query, "user");

                    // �� �̵��� ���õ� ���̵��� �̵�
                    switch (datahub.StageNumber) {
                        case 1:
                            SceneManager.LoadScene("MapScene1");
                            break;
                        case 2:
                            SceneManager.LoadScene("MapScene2");
                            break;
                    }
                }
            }
        }
    }
}
