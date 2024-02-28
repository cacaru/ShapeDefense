using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private RaycastHit2D hit;
    private string move_name;

    private static SceneChanger _instance = null;

    void Awake()
    {
        if (_instance == null) {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad( _instance );
    }

    void Update()
    {
        if ( Input.GetMouseButtonDown(0) ) {
            //클릭 좌표 찾기
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //해당 좌표에 있는 오브젝트 찾기
            hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            // hit에 걸리는 collider가 있으면 해당 collider의 이름 가져오기
            if ( hit.collider != null ) {
                move_name = hit.collider.gameObject.name;
                //Debug.Log(move_name);
                ChangeScene();
            }
        }
    }

    public void ChangeScene() {
        // 클릭된 씬으로 이동
        switch(move_name) {
            case "UnitPageBtnBg":
                SceneManager.LoadScene("UnitScene");
                break;

            case "StorePageBtnBg":
                SceneManager.LoadScene("ShopScene");
                break;

            case "GameStartPageBtnBg":
                SceneManager.LoadScene("GameStartScene");
                break;

            case "AchivmentPageBtnBg":
                SceneManager.LoadScene("AchivementScene");
                break;

            case "SettingPageBtnBg":
                SceneManager.LoadScene("SettingScene");
                break;

            case "DetailBackBtn":
                SceneManager.LoadScene("UnitScene");
                break;
        }
    }
}
