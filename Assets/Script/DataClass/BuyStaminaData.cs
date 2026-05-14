using UnityEngine;
public class BuyStaminaData : MonoBehaviour
{
    private int buy_stamina = 0;
    private readonly int stamina_coefficient = 50;
    private int need_gold = 0;
    public int StaminaValue { 
        get { return buy_stamina; } 
        set { 
            buy_stamina = value; 
            need_gold = buy_stamina * stamina_coefficient;
        }
    }

    public int NeedGold { get { return need_gold; } }

    public void Reset() {
        buy_stamina = 0;
        need_gold = 0;
    }
}
