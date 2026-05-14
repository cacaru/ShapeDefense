using UnityEngine;
using System.Collections.Generic;
using static ShapeDefenseSpace.GameData;

public class UnitUpgradeObserver : SceneSingleton<UnitUpgradeObserver>
{
    [SerializeField] private GameObject Ani;

    private bool can_recive = false;
    // Start is called before the first frame update
    void Start()
    {
        SetAni();
    }

    public void SetAni() {
        can_recive = false;
        // 모든 unit들을 돌며 강화 가능한지 확인
        var list = new List<int>(datahub.Unit_dic.Keys);
        int size = list.Count;
        for(int i = 0; i < size; i++) {
            Unit unit = datahub.Unit_dic[list[i]];
            if(unit.NeedPiece == 0) {
                continue;
            }
            if(unit.Piece >= unit.NeedPiece * (unit.UpgradeValue + 1) &&
               datahub.User.Dot >= unit.NeedGold
                ) {
                can_recive = true;
                break;
            }
        }

        if (can_recive) {
            Ani.SetActive(true);
        }
        else {
            Ani.SetActive(false);
        }
    }
}
