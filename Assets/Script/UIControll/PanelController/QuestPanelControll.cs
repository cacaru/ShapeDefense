using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestPanelControll : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image DailySelect;
    [SerializeField] private Image WeeklySelect;
    [SerializeField] private Image AchieveSelect;

    [SerializeField] private GameObject DailyQuest;
    [SerializeField] private GameObject WeeklyQuest;
    [SerializeField] private GameObject Achievement;

    private Color EffectOn = new(0, 87 / 255f, 1f, 1f);
    private Color EffectOff = new(217 / 255f, 206 / 255f, 198 / 255f, 0f);

    public void OnPointerClick(PointerEventData eventData) {
        var target = eventData.pointerCurrentRaycast.gameObject.name;

        if (target.Contains("normal")) {
            switch (target) {
                case "Dailynormal":
                    DailySelect.color = EffectOn;
                    WeeklySelect.color = EffectOff;
                    AchieveSelect.color = EffectOff;

                    // daliy quest 보여주기
                    DailyQuest.SetActive(true);
                    WeeklyQuest.SetActive(false);
                    Achievement.SetActive(false);
                    break;
                case "Weeklynormal":
                    DailySelect.color = EffectOff;
                    WeeklySelect.color = EffectOn;
                    AchieveSelect.color = EffectOff;

                    // weekly quest 보여주기
                    DailyQuest.SetActive(false);
                    WeeklyQuest.SetActive(true);
                    Achievement.SetActive(false);
                    break;
                case "Achievenormal":
                    DailySelect.color = EffectOff;
                    WeeklySelect.color = EffectOff;
                    AchieveSelect.color = EffectOn;

                    // achievement 보여주기
                    DailyQuest.SetActive(false);
                    WeeklyQuest.SetActive(false);
                    Achievement.SetActive(true);
                    break;
            }
        }
    }

}
