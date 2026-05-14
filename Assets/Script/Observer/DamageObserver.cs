using UnityEngine;
using static ShapeDefenseSpace.GameData;

public class DamageObserver : SceneSingleton<DamageObserver>
{

    public void DamageUpdate() {
        // unitfield 를 돌면서 id를 자기 자신으로 갱신하기
        int size = datahub.StageFieldNumber;
        for(int i = 1; i <= size; i++) {
            GameObject unit = datahub.StageField[i] as GameObject;
            if (unit.GetComponent<Field>().UnitId > 0) {
                unit.GetComponent<UnitAttack>().Id = unit.GetComponent<Field>().UnitId;
            }
        }
    }
}
