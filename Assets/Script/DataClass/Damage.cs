
public class Damage  {

    public float damage;
    public int damage_type;

    public void SetDamage(float damage, int damage_type) {
        this.damage = damage;
        this.damage_type = damage_type;

    }

    public void Init() {
        damage = 0;
        damage_type = 0;
    }

    public Damage() {
        damage = 0;
        damage_type = 0;
    }
    public Damage(float damage, int damage_type) {
        this.damage = damage;
        this.damage_type = damage_type;
    }
}