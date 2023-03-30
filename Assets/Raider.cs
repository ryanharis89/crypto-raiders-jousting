using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raider : MonoBehaviour
{
    public int strength;
    public int inteligence;
    public int agility;
    public int charm;
    public int wisdom;
    public int luck;

    private int totalStats;
    public Raider(int strength, int inteligence, int agility, int charm, int wisdom, int luck) {
        this.strength = strength;
        this.inteligence = inteligence;
        this.agility = agility;
        this.charm = charm;
        this.wisdom = wisdom;
        this.luck = luck;
        totalStats = strength + inteligence + agility + charm + wisdom + luck;
    }

    public int GetTotalStats() {
        return totalStats;
    }
}
