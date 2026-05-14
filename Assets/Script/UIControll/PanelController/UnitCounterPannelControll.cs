using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ShapeDefenseSpace.PublicData;
using static ShapeDefenseSpace.GameData;
public class UnitCounterPannelControll : PanelController
{
    // Start is called before the first frame update
    void Start()
    {
        SetAnimator(GetComponent<Animator>());
    }

    public override void PanelActivate() {
        UnitClickObserver.Instance.Click_Off();
        datahub.IsShowUnitCount = true;
        // 오브젝트 활성화
        UnitCounterPool.Instance.ShowUnitCount();
        base.PanelActivate();
    }

    public override void PanelDown() {
        UnitClickObserver.Instance.Click_On();
        datahub.IsShowUnitCount = false;
        UnitCounterPool.Instance.OffObject();
        base.PanelDown();
    }

}
