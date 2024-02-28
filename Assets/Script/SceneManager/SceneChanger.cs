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
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (_instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad( _instance );
    }

    void Update()
    {
        if ( Input.GetMouseButtonDown(0) ) {
            //Ŭ�� ��ǥ ã��
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //�ش� ��ǥ�� �ִ� ������Ʈ ã��
            hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            // hit�� �ɸ��� collider�� ������ �ش� collider�� �̸� ��������
            if ( hit.collider != null ) {
                move_name = hit.collider.gameObject.name;
                //Debug.Log(move_name);
                ChangeScene();
            }
        }
    }

    public void ChangeScene() {
        // Ŭ���� ������ �̵�
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
