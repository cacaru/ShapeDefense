using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;

public class GameStartChecker : MonoBehaviour, IPointerClickHandler {

    [SerializeField] private GameObject StaminaAnnounce;

    public void OnPointerClick(PointerEventData eventData) {
        GameObject now = eventData.pointerCurrentRaycast.gameObject;
        if (now != null) {
            string name = now.name;
            if (name == "Start_btn") {
                // 난이도별 스테미너 체크
                int used_stamina = 4 + (datahub.Difficulty - 1);
                //Debug.Log(used_stamina);
                // 스테미너 체크
                if (datahub.User.Stamina >= used_stamina) {
                    // 스테미너를 4 소비하고 씬 이동
                    // db 의 정보를 수정
                    string query = UtilityHub.query_builder.Append("UPDATE user SET stamina=")
                                                           .Append(datahub.User.Stamina-used_stamina)
                                                           .ToString();
                    UtilityHub.query_builder.Clear();
                    modifyDB.ControllDB(query, "user");

                    // 스태미나 소모 업적 카운트
                    achieve_observer.StaminaQuestCheck(used_stamina);
                    datahub.InitCounter();
                    datahub.Gaming = true;
                    // 랜덤한 필드로 이동함!
                    /*
                    datahub.StageNumber = 8;
                    datahub.Difficulty = 1;
                    SceneManager.LoadScene("MapScene8");
                    */
                    
                    int random_stage_number = Random.Range(1, 9);
                    datahub.StageNumber = random_stage_number;
                    datahub.NowScene = random_stage_number switch {
                        1 => SCENE_NUMBER.FIELD_1,
                        2 => SCENE_NUMBER.FIELD_2,
                        3 => SCENE_NUMBER.FIELD_3,
                        4 => SCENE_NUMBER.FIELD_4,
                        5 => SCENE_NUMBER.FIELD_5,
                        6 => SCENE_NUMBER.FIELD_6,
                        7 => SCENE_NUMBER.FIELD_7,
                        8 => SCENE_NUMBER.FIELD_8,
                        _ => 0,
                    };
                    SceneManager.LoadScene("MapScene" +  random_stage_number);
                    
                }

                // 스태미나 부족으로 인한 시작 불가 announce
                else {
                    StaminaAnnounce.SetActive(true);
                }
            }
        }
    }

    public void AnnounceExit() {
        StaminaAnnounce.SetActive(false);
    }
}
