using System;
using System.Collections.Generic;
using System.Linq;

public class Enemy {

    private int id;
    private string name;
    private int health;
    private int upgrade_value;
    private int speed;

    public int Id { get { return id; } set {  id = value; } }
    public string Name { get { return name; } set { name = value; } }
    public int Health { get { return health; } set { health = value; } }
    public int UpgradeValue { get { return upgrade_value; } set { upgrade_value = value; } }
    public int Speed { get { return speed; } set {  speed = value; } }
}