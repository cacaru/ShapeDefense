using UnityEngine;
using static ShapeDefenseSpace.PublicData;

public class PageControll : SceneSingleton<PageControll> 
{
    [SerializeField] private Animator helper_animator;
    [SerializeField] private Animator library_animator;
    [SerializeField] private Animator unit_animator;
    [SerializeField] private Animator quest_animator;
    [SerializeField] private Animator stat_animator;

    private Animator header_animator;
    private Animator footer_animator;
    [SerializeField] private Animator btnarea_animator;
    [SerializeField] private Animator startarea_animator;
    private bool already_activate = false;

    delegate void PageChangeDelegate(string modal_val);
    PageChangeDelegate PageOn;
    PageChangeDelegate PageOff;

    private void Start() {
        PageOn += PageClean;
        PageOn += PannelShow;

        PageOff += PannelHide;
        PageOff += PageAreaShow;
    }

    public void PageClean(string empty_val) {
        
        header_animator = HeaderSetting.Instance.gameObject.transform.Find("HeaderObj").gameObject.GetComponent<Animator>();
        footer_animator = MenuHandler.Instance.gameObject.transform.Find("Footer").gameObject.GetComponent<Animator>();
        
        header_animator.SetBool(ANI_ACTIVATE, true);
        footer_animator.SetBool(ANI_ACTIVATE, true);
        btnarea_animator.SetBool(ANI_ACTIVATE, true);
        startarea_animator.SetBool(ANI_ACTIVATE, true);
    }

    public void PageAreaShow(string empty_val) {
        header_animator = HeaderSetting.Instance.gameObject.transform.Find("HeaderObj").gameObject.GetComponent<Animator>();
        footer_animator = MenuHandler.Instance.gameObject.transform.Find("Footer").gameObject.GetComponent<Animator>();

        header_animator.SetBool(ANI_ACTIVATE, false);
        footer_animator.SetBool(ANI_ACTIVATE, false);
        btnarea_animator.SetBool(ANI_ACTIVATE, false);
        startarea_animator.SetBool(ANI_ACTIVATE, false);
    }

    private void PannelShow(string modal) {
        if (already_activate) return;
        already_activate = true;
        switch (modal) {
            case "helper":
                helper_animator.SetBool(ANI_ACTIVATE, true);
                break;
            case "library":
                library_animator.SetBool("StartPageActivate", true);
                break;
            case "unit":
                unit_animator.SetBool(ANI_ACTIVATE, true);
                break;
            case "quest":
                quest_animator.SetBool(ANI_ACTIVATE, true);
                break;
            case "stat":
                stat_animator.SetBool(ANI_ACTIVATE, true);
                break;
        }
    }

    private void PannelHide(string modal) {
        switch (modal) {
            case "helper":
                helper_animator.SetBool(ANI_ACTIVATE, false);
                break;
            case "library":
                library_animator.SetBool("StartPageActivate", false);
                break;
            case "unit":
                unit_animator.SetBool(ANI_ACTIVATE, false);
                break;
            case "quest":
                quest_animator.SetBool(ANI_ACTIVATE, false);
                break;
            case "stat":
                stat_animator.SetBool(ANI_ACTIVATE, false);
                break;
        }
        already_activate = false;
    }

    // panel을 보이기
    public void PanelActivate(string modal) {
        PageOn(modal);
    }

    // panel을 감추기
    public void PanelDown(string modal) {
        PageOff(modal);
    }

    public void UnitDetailPannelDown() {
        UnitDetailPageSetting.Instance.gameObject.GetComponent<Animator>().SetBool(ANI_ACTIVATE, false);
        unit_animator.SetBool("On", false);
    }

}
