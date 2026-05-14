using System.Collections;
using TMPro;
using UnityEngine;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.PublicData;
using static ShapeDefenseSpace.GameData;

public class AnnounceControll : SceneSingleton<AnnounceControll>
{
    [SerializeField] private GameObject AnnouncePanel;
    [SerializeField] private GameObject CombineConfirm;
    [SerializeField] private GameObject RollConfirm;
    [SerializeField] private GameObject TypeRollConfirm;
    [SerializeField] private GameObject ImpossibleAnnounce;

    private Animator animator;

    public int CombineID = 0;
    public int WaitingPos = 0;

    private readonly string CannotCombineText = "재료가 부족하여 \r\n조합할 수 없습니다.";
    private readonly string CannotSummonText = "공간이 부족하여\r\n유닛을 소환할 수 없습니다.";
    // Start is called before the first frame update
    void Start()
    {
        animator = AnnouncePanel.GetComponent<Animator>();
    }

    /// <summary>
    /// 안내창 열기
    /// </summary>
    /// <param name="type">
    ///     1 > 조합 불가 안내 
    ///     2 > 조합 확인창 
    ///     3 > 소환 불가 안내
    ///     4 > 변환 안내
    ///     5 > 공격 타입 변환 안내
    /// </param>
    /// 
    public void AnnounceOn(int type) {
        switch (type) {
            case 1:
                ImpossibleAnnounce.transform.Find("Announce").gameObject.GetComponent<TMP_Text>().text = CannotCombineText;
                ImpossibleAnnounce.SetActive(true);
                // 1초뒤 안내창 해제
                StartCoroutine(AutoClose());
                break;

            case 2:
                UnitClickObserver.Instance.Click_Off();
                CombineConfirm.SetActive(true);
                break;

            case 3:
                ImpossibleAnnounce.transform.Find("Announce").gameObject.GetComponent<TMP_Text>().text = CannotSummonText;
                ImpossibleAnnounce.SetActive(true);
                // 1초뒤 안내창 해제
                StartCoroutine(AutoClose());
                break;
            case 4:
                UnitClickObserver.Instance.Click_Off();
                RollConfirm.SetActive(true);
                break;

            case 5:
                UnitClickObserver.Instance.Click_Off();
                TypeRollConfirm.SetActive(true);
                break;
        }
        animator.SetBool(ANI_ACTIVATE, true);
    }

    public void AnnounceOff() {
        ImpossibleAnnounce.SetActive(false);
        CombineConfirm.SetActive(false);
        animator.SetBool(ANI_ACTIVATE, false);
    }

    IEnumerator AutoClose() {
        yield return wfs_1_5;
        AnnounceOff();
    }

    // combine controll
    // 조합확인 이벤트
    public void CombineStart() {
        CombineConfirm.SetActive(false);
        UnitClickObserver.Instance.Click_On();
        animator.SetBool(ANI_ACTIVATE, false);
        datahub.CombineWaitingPos = WaitingPos;
        UtilityHub.CombineCheck(CombineID);
    }

    public void CombineCancel() {
        CombineConfirm.SetActive(false);
        UnitClickObserver.Instance.Click_On();
        animator.SetBool(ANI_ACTIVATE, false);
    }

    // reroll cofirm
    public void RollStart() {
        RollConfirm.SetActive(false);
        UnitClickObserver.Instance.Click_On();
        animator.SetBool(ANI_ACTIVATE, false);

        // Roll 
        datahub.ItemRollActive = true;
        ItemReroll.Instance.Reroll();
    }

    public void RollCancel() {
        datahub.ItemRollActive = false;
        RollConfirm.SetActive(false);
        UnitClickObserver.Instance.Click_On();
        animator.SetBool(ANI_ACTIVATE, false);
    }

    // reroll cofirm
    public void TypeRollStart() {
        TypeRollConfirm.SetActive(false);
        UnitClickObserver.Instance.Click_On();
        animator.SetBool(ANI_ACTIVATE, false);

        // Roll
        TypeReroll.Instance.Reroll();
    }

    public void TypeRollCancel() {
        TypeRollConfirm.SetActive(false);
        UnitClickObserver.Instance.Click_On();
        animator.SetBool(ANI_ACTIVATE, false);
    }

}
