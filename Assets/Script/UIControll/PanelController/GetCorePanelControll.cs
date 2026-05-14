using UnityEngine;

public class GetCorePanelControll : PanelController
{
    [SerializeField] private GameObject Panel;

    private void Start() {
        SetAnimator(Panel.GetComponent<Animator>());
    }

    // 클릭 막기
    public void CorePanelActivate() {
        UnitClickObserver.Instance.Click_Off();
        //Unit_click_observer.Click_Off();
    }

    public void CorePanelDown() {
        UnitClickObserver.Instance.Click_On();
        //Unit_click_observer.Click_On();
    }
}
