using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ShapeDefenseSpace.GameData;

// home 화면 등 에서 뒤로가기를 눌렀을 때 
public class EndControll : MonoBehaviour
{
    // 종료하기
    public void GameEnd() {
        // 시간 기록하기
        stamina_observer.RecoardEndTime();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnDestroy() {
        // 시간 기록하기
        stamina_observer.RecoardEndTime();
    }

    // 창 끄기
    public void EndAnnounceExit() {
        gameObject.SetActive(false);
    }
}
