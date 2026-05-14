using System.Collections;
using UnityEngine;

using static ShapeDefenseSpace.PublicData;
using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.CombineTableShowObserver;
using UnityEngine.UI;
using TMPro;

public class UnitClickObserver : SceneSingleton<UnitClickObserver> {
    private RaycastHit2D hit;

    [SerializeField] private GameObject CombineField;
    [SerializeField] private GameObject SellBtn;
    [SerializeField] private GameObject RerollBtn;
    [SerializeField] private GameObject TypeRollBtn;
    [SerializeField] private GameObject SkipBtn;

    [SerializeField] private GameObject CombineArea;
    [SerializeField] private TMP_Text Item_Roll_Dot;
    [SerializeField] private TMP_Text Type_Roll_Dot;
    private Animator animator;
    
    public bool Can_Unit_Click = true;

    private int current_click_unit_id = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = CombineArea.GetComponent<Animator>();

        // 현재 난이도에 따라 dot증량

        /// 기본 50 / 300
        /// 증가량 > 2 / 20
        datahub.ItemRollValue += (datahub.Difficulty - 1) * 2;
        datahub.TypeRollValue += (datahub.Difficulty - 1) * 50;
        //Debug.Log(datahub.Difficulty);
        Item_Roll_Dot.text = datahub.ItemRollValue.ToString();
        Type_Roll_Dot.text = datahub.TypeRollValue.ToString();
    }

    public void Click_On() {
        Can_Unit_Click = true;
    }
    public void Click_Off() {
        Can_Unit_Click = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Can_Unit_Click) {
            //클릭위치 좌표
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(pos, Vector2.zero, 3f, LayerMask.GetMask("Unit"));
            if (hit.collider != null) {
                // unit인지 확인
                // unit 이면 내부의 click이벤트에 접근해서 클릭 실행
                if (hit.collider.name.Contains("Unit") && hit.collider.gameObject.GetComponent<Field>().UnitId > 0) {
                    FrontAreaController.Instance.PannelActivate();
                    // 필드 부르기
                    UnitClickFieldOn(hit.collider.gameObject.GetComponent<Field>().UnitId, hit.collider.gameObject.GetComponent<Field>().Type);

                    hit.collider.gameObject.GetComponent<UnitClick>().ClickReturn();
                }
                else if (hit.collider.name.Equals("CanvasField")) {
                    // do nothing
                }
                else {
                    CleanField();
                }
            }
            else {
                CleanField();
            }
        }
    }


    private void Off() {
        // 조합 필드 불러오기
        animator.SetBool(ANI_ACTIVATE, false);
        FrontAreaController.Instance.PannelDown();
    }

    // area on animate active
    public void UnitClickFieldOn(int unit_id, int unit_type) {

        animator.SetBool(ANI_ACTIVATE, true);
        // 상세 필드 불러오기
        InfoPanelControll.Instance.InfoPanelActivate(unit_id, unit_type, 1);
    }

    // area off animate active
    public void UnitClickFieldOff() {
        StopCoroutine(RollBtnWatcher());
        // 필드 돌리기
        Off();
        // 정보창 돌리기
        InfoPanelControll.Instance.InfoPanelDown(1);
    }

    public void UnitCombineFieldSetting(int cur_id, int waiting_pos, Vector3 pos) {
        // 보여주기 이전에 이미 보여지고 있는 목록이 있으면 지우기
        CleanContent(CombineField);
        //Debug.Log(cur_id);
        current_click_unit_id = cur_id;
        if (cur_id <= 0) {
            // 화면만 지우고 아무것도 하지 않음
            return;
        }

        // 유닛의 주변으로 범위표기
        AttackAreaControll.Instance.Show(pos, current_click_unit_id);

        // 현재 선택 값을 datahub에 저장
        datahub.CombineWaitingPos = waiting_pos;
        // 조합식 보여주기
        ShowCombineTable(current_click_unit_id, false, CombineField, 1);

        // 판매 버튼 보여주기
        UnitSell.Instance.UnitId = current_click_unit_id;
        SellBtn.SetActive(true);

        // 조각 변환 버튼 보여주기
        if(current_click_unit_id >= 300 && current_click_unit_id <= 599) {
            StartCoroutine(RollBtnWatcher());
        }
        else {
            RerollBtn.SetActive(false);
        }

        // 공격 형식 변환 보여주기
        if(current_click_unit_id >= 3000 && current_click_unit_id <= 6999) {
            StartCoroutine(TypeRollBtnWatcher());
        }
        else {
            TypeRollBtn.SetActive(false);
        }
    }
    
    IEnumerator RollBtnWatcher() {
        while (true) {
            if(current_click_unit_id < 300 || current_click_unit_id > 1000) {
                RerollBtn.SetActive(false);
                break;
            }
            yield return wff;
            // 클릭한 유닛이 아이템이고 골드가 50이상이라면 변환 버튼 활성화
            if (datahub.Dot >= 50) {
                //SkipBtn.SetActive(false);
                ItemReroll.Instance.unit_id = current_click_unit_id;
                RerollBtn.SetActive(true);
            }
            else {
                RerollBtn.SetActive(false);
            }
        }
    }


    IEnumerator TypeRollBtnWatcher() {
        while (true) {
            if (current_click_unit_id < 3000) {
                TypeRollBtn.SetActive(false);
                break;
            }
            yield return wff;
            // 클릭한 유닛이 C급 이상이고 필요재화 이상 있다면 버튼 활성화
            if (datahub.Dot >= datahub.TypeRollValue) {
                //SkipBtn.SetActive(false);
                TypeReroll.Instance.unit_id = current_click_unit_id;
                TypeRollBtn.SetActive(true);
            }
            else {
                TypeRollBtn.SetActive(false);
            }
        }
    }

    public void CleanField() {
        UnitClickFieldOff();
        // 범위 제거
        AttackAreaControll.Instance.Hide();

        // 내부 설정값 제거
        datahub.CombineWaitingPos = 0;
        CleanContent(CombineField);

        // 조합창 내부 버튼 비활성화
        SellBtn.SetActive(false);
        RerollBtn.SetActive(false);
        StopCoroutine(RollBtnWatcher());
        StopCoroutine(TypeRollBtnWatcher());
    }

    public void CleanList() {

        CleanContent(CombineField);
    }
}
