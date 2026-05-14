using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;

public class StartPageLoad : MonoBehaviour, IPointerClickHandler
{
    private void Awake() {
        Application.targetFrameRate = 60;
    }

    public void OnPointerClick(PointerEventData eventData) {
        datahub.NowScene = SCENE_NUMBER.LOBBY;
        SceneManager.LoadScene("GameStartScene");
    }

}
