using UnityEngine;
using UnityEngine.SceneManagement;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;

public class SceneChanger : Singleton<SceneChanger>
{
    private RaycastHit2D hit;
    private string move_name;

    private bool start_page_checker = true;
    
    private readonly string Start_Page_Name = "GameStartScene";

    private FirstSceneObserver scene_observer;

    public void StartPageSet() {
        start_page_checker = true;
    }

    void Update()
    {
        if ( Input.GetMouseButtonDown(0) ) {
            //클릭 좌표 찾기
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //해당 좌표에 있는 오브젝트 찾기
            hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if ( hit.collider != null ) {
                move_name = hit.collider.gameObject.name;
                //Debug.Log(move_name);
                ChangeScene();
            }
        }
        // 현재 씬이 첫 씬(GameStartScene) 이라면 first fage setting 켜기
        
        if (SceneManager.GetActiveScene().name.Equals(Start_Page_Name) && start_page_checker) {
            //sceneObserver.enabled = true;
            start_page_checker = false;
        }
        
        if ( !SceneManager.GetActiveScene().name.Equals(Start_Page_Name) && !start_page_checker) {
            start_page_checker = true;
        }
    }

    public void ChangeScene() {
        // 클릭된 씬으로 이동
        switch(move_name) {
            case "StorePageBtnBg":
                SceneManager.LoadScene("ShopScene");
                break;

            case "GameStartPageBtnBg":
                SceneManager.LoadScene(Start_Page_Name);
                //sceneObserver.enabled = true;
                break;

            case "SettingPageBtnBg":
                SceneManager.LoadScene("SettingScene");
                break;

            case "DetailBackBtn":
                SceneManager.LoadScene(Start_Page_Name);
                datahub.FromUnitDetail = true;
                //sceneObserver.enabled = true;
                break;
        }
    }

}
