using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ShapeDefenseSpace.GameData;

public class PillarSetter : SceneSingleton<PillarSetter>
{
    [SerializeField] private GameObject PillarP;

    private List<Transform> pillars = new();
    private int size;
    private int speed = 150;
    public bool End = false;
    private int pillar_counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        var tmp = PillarP.GetComponentsInChildren<Transform>();
        size = tmp.Length;
        for(int i = 0; i< size; i++) {
            if (!tmp[i].gameObject.name.Equals(PillarP.gameObject.name)) {
                pillars.Add(tmp[i]);
            }
        }
        size = pillars.Count;
    }

    public void Activate() {
        StartCoroutine(Active(0));
        StartCoroutine(Check());
    }

    public void Shower() {
        StartCoroutine(Active(-2400));
        StartCoroutine(Check());
    }

    IEnumerator Check() {
        while (true) {
            if(pillar_counter == size) {
                break;
            }
            yield return wff;
        }
        End = true;
    }

    IEnumerator Active(int val) {
        int counter = 0;
        int random_num;

        List<int> checker = new();
        for (int i = 0; i < size; i++) {
            checker.Add(0);
        }

        while (true) {
            if (counter == size) {
                break;
            }

            random_num = Random.Range(0, size);
            if (checker[random_num] != 0) {
                continue;
            }

            // 기둥 아래로 내리기
            StartCoroutine(Down(pillars[random_num], val));
            checker[random_num] = 1;
            counter++;

            yield return wfs_0_1;
        }
    }


    IEnumerator Down(Transform pillar, int val) {
        var pos = pillar.position;
        while (pillar.position.y > val) {
            pos.y -= speed;
            pillar.position = pos;
            yield return wff;
        }
        pillar_counter++;
    }
}
