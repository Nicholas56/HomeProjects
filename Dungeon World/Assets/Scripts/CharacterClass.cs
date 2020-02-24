using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClass : ScriptableObject
{
    public string className;

    public (int,int,int,int,int,int,int) statBonus;

    public (int, int, int, int, int, int, int) levelGain;
}
