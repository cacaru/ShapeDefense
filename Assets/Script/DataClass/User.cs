using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    #region VALUE
    private string nickname;            // �̸�
    private int dot;                    // ���(��)
    private int level;                  // ����
    private float experience;           // ����ġ
    private int stamina;                // ����
    private int max_stamina;            // �ִ� ������
    #endregion

    #region property
    public string Nickname { get { return nickname; } set {  nickname = value; } }
    public int Dot { get { return dot; } set { dot = value; } }
    public int Level { get { return level; } set { level = value; } }
    public float Experience { get {  return experience; } set {  experience = value; } }
    public int Stamina { get {  return stamina; } set {  stamina = value; } }
    public int MaxStamina { get { return max_stamina; } set {  max_stamina = value; } }
    #endregion
}
