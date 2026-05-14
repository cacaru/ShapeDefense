using UnityEngine;
using static ShapeDefenseSpace.GameData;

public class GamePause : Singleton<GamePause>
{

    private void Update() {
        // 뒤로가기 버튼 받으면
        if (Input.GetKey(KeyCode.Escape)) {
            // ingame 이면 pause
            if (datahub.Gaming) {
                GameObject.Find("Enemy").GetComponent<RoundProgress>().Pause();
                GameObject.Find("SettingControll").GetComponent<SettingPageControll>().SettingOnClick();
            }
            // 아니면 게임 종료 announce
            else {
                GameObject.Find("Canvas").transform.Find("EndAnnounce").gameObject.SetActive(true);
            }
        }


    }

    public void EnemyPauseSetting(bool value) {

        if (value) {
            datahub.Pause = false;
        }
        else {
            datahub.Pause = true;
        }
    }
}
