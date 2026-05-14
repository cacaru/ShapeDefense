using System.Collections;
using UnityEngine;

public abstract class QuestController : MonoBehaviour
{
    protected GameObject Content;
    protected GameObject AllReciveBtn;
    protected readonly ArrayList already_do = new();
    protected readonly ArrayList can_recive = new();
    protected readonly ArrayList normal = new();

    protected int QuestId = 0;
    public int SetID { get { return QuestId; } set { QuestId = value; } }

    /// <summary>
    /// content에 획득한 quest 목록을 보여주기 위해 prefab을 생성하는 함수
    /// </summary>
    abstract public void Show();

    /// <summary>
    /// 모두 받기를 통해 받을 수 있는 quest를 받는 함수
    /// </summary>
    abstract public void AllRecive();

    /// <summary>
    /// 단일 받기를 통해 quest의 보상을 받는 함수
    /// </summary>
    abstract public void Recive(int id);


    public void PageReset() {
        var item = Content.GetComponentsInChildren<Transform>();
        foreach (var obj in item) {
            if (obj != Content.transform) {
                Destroy(obj.gameObject);
            }
        }
    }
    protected void InitArrayList() {
        int size = already_do.Count;
        for (int i = 0; i < size; i++) {
            already_do.RemoveAt(0);
        }
        size = can_recive.Count;
        for (int i = 0; i < size; i++) {
            can_recive.RemoveAt(0);
        }
        size = normal.Count;
        for (int i = 0; i < size; i++) {
            normal.RemoveAt(0);
        }
    }

}
