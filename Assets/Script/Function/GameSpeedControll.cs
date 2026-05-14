using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ShapeDefenseSpace.GameData;


public class GameSpeedControll : MonoBehaviour , IPointerClickHandler
{

    [SerializeField] private GameObject Button;
    [SerializeField] private RoundProgress pause_controller;

    private readonly string speed_icon_path_0 = "Sprite/Icon/speed_icon_0";
    private readonly string speed_icon_path_1 = "Sprite/Icon/speed_icon_1";
    private readonly string speed_icon_path_2 = "Sprite/Icon/speed_icon_2";

    public void SpeedControll() {
        
        datahub.SpeedUp++;

        if(datahub.SpeedUp == 1){
            pause_controller.ReStart();
            Time.timeScale = 1;
            Button.GetComponent<Image>().sprite = Resources.Load<Sprite>(speed_icon_path_1);
            datahub.SpeedRate = 1;
        }

        else if(datahub.SpeedUp == 2) {
            Time.timeScale = 2;
            Button.GetComponent<Image>().sprite = Resources.Load<Sprite>(speed_icon_path_2);
            datahub.SpeedRate = 2;
        }

        else {
            datahub.SpeedUp = 0;
            // enemy, bullet, attack을 정지시켜야함
            pause_controller.Pause();
            Button.GetComponent<Image>().sprite = Resources.Load<Sprite>(speed_icon_path_0);
            datahub.SpeedRate = 0;
        }

    }

    public void OnPointerClick(PointerEventData eventData) {
        var target = eventData.pointerCurrentRaycast.gameObject.name;

        if (target.Equals("GameSpeedControllBg")) {
            SpeedControll();
        }
    }
}
