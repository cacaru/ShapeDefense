using UnityEngine;

public class UpgradePanelControll : PanelController
{
    [SerializeField] private GameObject Panel;
    
    void Start() {
        SetAnimator(Panel.GetComponent<Animator>());
    }

    public override void PanelActivate() {
        FrontAreaController.Instance.PannelActivate();
        UnitClickObserver.Instance.Click_Off();
        base.PanelActivate();
    }

    public override void PanelDown() {
        FrontAreaController.Instance.PannelDown();
        UnitClickObserver.Instance.Click_On();
        base.PanelDown();
    }
}