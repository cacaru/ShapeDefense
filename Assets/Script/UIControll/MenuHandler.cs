using ShapeDefenseSpace;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static ShapeDefenseSpace.GameData;

public class MenuHandler : Singleton<MenuHandler>, IPointerClickHandler
{
    [SerializeField] private Transform Footer;

    private string move_name;

    public void OnPointerClick(PointerEventData eventData) {
        move_name = eventData.pointerCurrentRaycast.gameObject.name;
        ChangeScene();
    }

    // Start is called before the first frame update
    void Start() {
        // 최초 실행
        SelectedOn();

        // 화면이 전환될 때 마다 실행
        SceneManager.sceneLoaded += SelectedOnBox;
    }

    private void SelectedOnBox(Scene scenem, LoadSceneMode mode) {
        SelectedOn();
    }

    private void SelectedOn() {
        // all off
        AllItemOff();
        Color color;
        switch (SceneManager.GetActiveScene().name) {
            case "UnitScene":
                gameObject.SetActive(true);
                // 배경
                color = Footer.Find("Layout").Find("Unit").gameObject.GetComponent<Image>().color;
                color.a = 1;
                Footer.Find("Layout").Find("Unit").gameObject.GetComponent<Image>().color = color;
                // 이름 키우기
                Footer.Find("Layout").Find("Unit").Find("Text").gameObject.GetComponent<TMP_Text>().fontSize = 60;
                break;

            case "ShopScene":
                datahub.NowScene = SCENE_NUMBER.SHOP;
                gameObject.SetActive(true);
                // 배경
                color = Footer.Find("Layout").Find("Shop").gameObject.GetComponent<Image>().color;
                color.a = 1;
                Footer.Find("Layout").Find("Shop").gameObject.GetComponent<Image>().color = color;
                // 이름 키우기
                Footer.Find("Layout").Find("Shop").Find("Text").gameObject.GetComponent<TMP_Text>().fontSize = 60;
                break;

            case "GameStartScene":
                datahub.NowScene = SCENE_NUMBER.LOBBY;
                gameObject.SetActive(true);
                // 배경
                color = Footer.Find("Layout").Find("Game").gameObject.GetComponent<Image>().color;
                color.a = 1;
                Footer.Find("Layout").Find("Game").gameObject.GetComponent<Image>().color = color;
                // 이름 키우기
                Footer.Find("Layout").Find("Game").Find("Text").gameObject.GetComponent<TMP_Text>().fontSize = 60;
                break;

            case "AchivementScene":
                gameObject.SetActive(true);
                // 배경
                color = Footer.Find("Layout").Find("Achieve").gameObject.GetComponent<Image>().color;
                color.a = 1;
                Footer.Find("Layout").Find("Achieve").gameObject.GetComponent<Image>().color = color;
                // 이름 키우기
                Footer.Find("Layout").Find("Achieve").Find("Text").gameObject.GetComponent<TMP_Text>().fontSize = 60;
                break;

            case "SettingScene":
                datahub.NowScene = SCENE_NUMBER.SETTING;
                gameObject.SetActive(true);
                // 배경
                color = Footer.Find("Layout").Find("Setting").gameObject.GetComponent<Image>().color;
                color.a = 1;
                Footer.Find("Layout").Find("Setting").gameObject.GetComponent<Image>().color = color;
                // 이름 키우기
                Footer.Find("Layout").Find("Setting").Find("Text").gameObject.GetComponent<TMP_Text>().fontSize = 60;
                break;

            // 다른 화면이라면 footer를 꺼야함
            default:
                gameObject.SetActive(false);
                break;
        }
    }

    private void AllItemOff() {
        Color color;

        // unit
        color = Footer.Find("Layout").Find("Unit").gameObject.GetComponent<Image>().color;
        color.a = 0;
        Footer.Find("Layout").Find("Unit").gameObject.GetComponent<Image>().color = color;
        // 이름 키우기
        Footer.Find("Layout").Find("Unit").Find("Text").gameObject.GetComponent<TMP_Text>().fontSize = 53;

        // shop
        color = Footer.Find("Layout").Find("Shop").gameObject.GetComponent<Image>().color;
        color.a = 0;
        Footer.Find("Layout").Find("Shop").gameObject.GetComponent<Image>().color = color;
        // 이름 키우기
        Footer.Find("Layout").Find("Shop").Find("Text").gameObject.GetComponent<TMP_Text>().fontSize = 53;

        // game
        color = Footer.Find("Layout").Find("Game").gameObject.GetComponent<Image>().color;
        color.a = 0;
        Footer.Find("Layout").Find("Game").gameObject.GetComponent<Image>().color = color;
        // 이름 키우기
        Footer.Find("Layout").Find("Game").Find("Text").gameObject.GetComponent<TMP_Text>().fontSize = 53;

        // achieve
        // 배경
        color = Footer.Find("Layout").Find("Achieve").gameObject.GetComponent<Image>().color;
        color.a = 0;
        Footer.Find("Layout").Find("Achieve").gameObject.GetComponent<Image>().color = color;
        // 이름 키우기
        Footer.Find("Layout").Find("Achieve").Find("Text").gameObject.GetComponent<TMP_Text>().fontSize = 53;

        // setting
        // 배경
        color = Footer.Find("Layout").Find("Setting").gameObject.GetComponent<Image>().color;
        color.a = 0;
        Footer.Find("Layout").Find("Setting").gameObject.GetComponent<Image>().color = color;
        // 이름 키우기
        Footer.Find("Layout").Find("Setting").Find("Text").gameObject.GetComponent<TMP_Text>().fontSize = 53;
    }
    private void ChangeScene() {
        // 클릭된 씬으로 이동
        switch (move_name) {
            case "Unit":
                SceneManager.LoadScene("UnitScene");
                break;

            case "Shop":
                SceneManager.LoadScene("ShopScene");
                break;

            case "Game":
                SceneManager.LoadScene("GameStartScene");
                break;

            case "Achieve":
                SceneManager.LoadScene("AchivementScene");
                break;

            case "Setting":
                SceneManager.LoadScene("SettingScene");
                break;
        }
    }
    
}
