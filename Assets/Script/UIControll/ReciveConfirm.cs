using ShapeDefenseSpace;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;

/// <summary>
/// 업적 받기 결과 받아가기
/// </summary>
public class ReciveConfirm : SceneSingleton<ReciveConfirm>
{
    [SerializeField] private TMP_Text test;
    [SerializeField] private GameObject ConfirmWindow;
    [SerializeField] private TMP_Text ConfirmAnnounceText;
    [SerializeField] private GameObject PieceArea;
    [SerializeField] private TMP_Text PieceAnnounceText;
    [SerializeField] private GameObject Content;
    [SerializeField] private GameObject ConfirmBtn;

    [SerializeField] private GameObject QuestController;

    // 확인창 열기
    public void OpenConfirmWindow(string announce, int total_reward_gold , int type) {
        ConfirmWindow.SetActive(true);

        // quest 창 리셋
        QuestController.GetComponent<DailyQuestControll>().PageReset();
        QuestController.GetComponent<WeeklyQuestControll>().PageReset();
        QuestController.GetComponent<AchievementControll>().PageReset();

        switch (type) {
            // 보상이 골드만 있는 경우
            case 1:
                StartCoroutine(Loading(announce));
                break;
            case 2:
                // 보상이 조각도 있는 경우
                StartCoroutine(LoadingWithPiece(total_reward_gold));
                break;
        }
    }

    IEnumerator Loading(string announce) {
        while(modifyDB.GetState() == STATE.DB_MODIFYING) {
            yield return wfs_1;
        }
        ConfirmAnnounceText.gameObject.SetActive(true);
        PieceArea.SetActive(false);
        ConfirmAnnounceText.text = announce;

        // 확인 버튼 띄우기
        ConfirmBtn.SetActive(true);
    }

    IEnumerator LoadingWithPiece(int total_reward_gold) {
        while (modifyDB.GetState() == STATE.DB_MODIFYING) {
            yield return wfs_1;
        }
        ConfirmAnnounceText.gameObject.SetActive(false);
        
        PieceAnnounceText.text = UtilityHub.query_builder.Append("골드 + ").Append(total_reward_gold.ToString()).ToString();
        UtilityHub.query_builder.Clear();
        
        int size = datahub.UnitCounter.Count;
        for(int i = 0; i < size; i++) {
            if (datahub.UnitCounter[datahub.Unit_Ids[i]] > 0) {
                GameObject pre_unit = Instantiate(_unit_piece_obj, Content.transform.position, Content.transform.rotation);
                pre_unit.transform.SetParent(Content.transform, false);
                pre_unit.transform.Find("Grade").GetComponent<Image>().color = i switch {
                    >= 1001 and <= 1006 => color_e,
                    >= 2001 and <= 2003 => color_d,
                    >= 3001 and <= 3015 => color_c,
                    >= 4001 and <= 4012 => color_b,
                    >= 5001 and <= 5006 => color_a,
                    >= 6001 and <= 6006 => color_s,
                    _ => core_color
                };
                pre_unit.transform.Find("Unit").GetComponent<Image>().sprite = UtilityHub.GetSprite(datahub.Unit_Ids[i]);
                pre_unit.transform.Find("Counter").GetComponent<TMP_Text>().text = UtilityHub.query_builder.Append("x ")
                                                                                                           .Append(datahub.UnitCounter[datahub.Unit_Ids[i]].ToString())
                                                                                                           .ToString();
                UtilityHub.query_builder.Clear();
            }
        }

        PieceArea.SetActive(true);

        // 확인 버튼 띄우기
        ConfirmBtn.SetActive(true);
    }

    public void Init() {
        ConfirmAnnounceText.gameObject.SetActive(true);
        PieceArea.SetActive(false);

        var list = Content.GetComponentsInChildren<Transform>();
        foreach (var item in list) {
            if (item != Content.transform) {
                Destroy(item.gameObject);
            }
        }
    }
}
