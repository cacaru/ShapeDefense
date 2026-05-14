using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static ShapeDefenseSpace.GameData;

public class HeaderSetting : Singleton<HeaderSetting>
{
    // header 영역 객체 연결
    [SerializeField] private GameObject Header;

    // Start is called before the first frame update
    void Start()
    {
        Header.transform.Find("EXP").gameObject.GetComponent<Slider>().interactable = false;
        HeaderSet();
        // 화면이 전환될 때 마다 실행
        SceneManager.sceneLoaded += HeaderSetBox;
    }

    private void HeaderSetBox(Scene scene, LoadSceneMode mode) {
        // 현재 씬에 Map이 포함되면 오브젝트 종료
        if (scene.name.Contains("Map") || scene.name.Equals("UnitDetailScene")) {
            gameObject.SetActive(false);
        }
        else {
            gameObject.SetActive(true);
            HeaderSet();
        }
        
    }

    public void HeaderSet() {
        // data 연결
        // 이름
        Header.transform.Find("Name").gameObject.GetComponent<TMP_Text>().text = datahub.User.Nickname.ToString();
        // 레벨
        Header.transform.Find("Level").gameObject.GetComponent<TMP_Text>().text = datahub.User.Level.ToString();
        // 골드
        Header.transform.Find("Gold").gameObject.GetComponent<TMP_Text>().text = datahub.User.Dot.ToString();
        // 경험치량
        float exp = datahub.User.Experience > 0 ? (float)datahub.User.Experience / datahub.User.NeedExperience : 0;
        Header.transform.Find("EXP").gameObject.GetComponent<Slider>().value = exp;
        Header.transform.Find("EXP_Text").gameObject.GetComponent<TMP_Text>().text = (exp * 100).ToString();
    }
}
