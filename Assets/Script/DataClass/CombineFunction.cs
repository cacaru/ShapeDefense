using UnityEngine;


/// <summary>
///  Unit Comp Memory Type
/// </summary>

public class CombineFunction {
    // 조합식 번호
    private int id;
    public int Id { get { return id; } set { id = value; } }

    // 조합할 유닛 번호
    private int a = -1;
    private int b = -1;
    private int c = -1;
    private int d = -1;

    // 조합에 필요한 유닛 수
    private int need_count = 0;

    private int result;

    public int A { get { return a; } set { a = value; } }
    public int B { get { return b; } set { b = value; } }
    public int C { get { return c; } set { c = value; } }
    public int D { get { return d; } set { d = value; } }
    public int NeedCount { get { return need_count; } set { need_count = value; } }
    public int Result { get { return result; } set { result = value; } }

    public void Print() {
        Debug.Log("a : [" + a + "]  b : [" + b + "]  c : [" + c + "]  d : [ " + d + "]  NeedCount : [" + need_count + "] Result [" + result + "]");
    }
}