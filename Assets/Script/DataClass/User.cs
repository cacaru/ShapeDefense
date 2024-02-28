using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    #region VALUE
    private string nickname;            // 이름
    private int dot;                    // 골드(점)
    private int level;                  // 레벨
    private float experience;           // 경험치
    private int stamina;                // 번개
    private int max_stamina;            // 최대 번개량
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
