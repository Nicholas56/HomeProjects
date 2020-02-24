using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public int health=100;
    public int mana=50;
    public int strength=10;
    public int constitution=10;
    public int agility=10;
    public int wisdom=10;
    public int intelligence=10;

    public int charLevel;
    public int expPoints;

    public CharacterClass charClass;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddToStats(int hp,int mp, int st, int co, int ag, int wi, int it)
    {
        health += hp;mana += mp;strength += st;constitution += co;agility += ag;wisdom += wi;intelligence += it;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
