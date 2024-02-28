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
                // 스테미너 체크
                if (datahub.User.Stamina >= 4) {
                    // 스테미너를 4 소비하고 씬 이동
                    // db 의 정보를 수정
                    string query = "UPDATE user SET stamina = " + (datahub.User.Stamina-4).ToString();
                    //Debug.Log(query);
                    GameObject.Find("GameObject").GetComponent<ModifyDB>().ControllDB(query, "user");

                    // 씬 이동은 선택된 난이도로 이동
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
