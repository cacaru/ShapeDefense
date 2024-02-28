using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  Unit Comp Memory Type
/// </summary>

public class CombineFunction {

    // ������ ���� ��ȣ
    private int a = -1;
    private int b = -1;
    private int c = -1;

    // ���տ� �ʿ��� ���� ��
    private int need_count = 0;

    private int result;

    public int A { get { return a; } set { a = value; } }
    public int B { get { return b; } set { b = value; } }
    public int C { get { return c; } set { c = value; } }
    public int NeedCount { get { return need_count; } set { need_count = value; } }
    public int Result { get { return result; } set { result = value; } }

    // ���� �������� Ȯ�� �� �Լ�
    /*
    public bool CompChecker(int a) {
        if ( a == this.a) {
            // b�� c�� ���� ���°� �´��� Ȯ��
            if ( b >= 0 || c >= 0) {
                return false;
            }
            return true;
        }
        return false;
    }
    public bool CompChecker(int a, int b) {
        if (a == this.a && b == this.b) {
            // c�� ���� �ִ��� Ȯ��
            if ( c >= 0 ) {
                return false;
            }
            // c�� d�� ���� ������ ���� ����
            return true;
        }
        return false;
    }

    public bool CompChecker(int a, int b, int c) {
        if (a == this.a && b == this.b && c == this.c) {
            return true;
        }
        return false;
    }
    */
    public void Print() {
        Debug.Log("a : [" + a + "]  b : [" + b + "]  c : [" + c + "] NeedCount : [" + need_count + "] Result [" + result + "]");
    }
}