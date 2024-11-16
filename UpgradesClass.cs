using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradesBought
{
    //Script holding silver
    public Silver inGameSilver;
    //Attack related
    [Header("Damage")]
    public int attackUpgradesCount;
    public int bodyDamageUpgradesCount;
    public int attackSpeedUpgradesCount;
    public int rangeUpgradesCount;
    //Health related
    [Header("Health")]
    public int healthUpgradesCount;
    public int healthRegenUpgradesCount;
    public int armorUpgradesCount;
    //Base costs
    [Header("BaseCosts")]
    public int baseAttackCost;
    public int baseBodyDamageCost;
    public int baseAttackSpeedCost;
    public int baseRangeCost;
    public int baseHealthCost;
    public int baseHealthRegenCost;
    public int baseArmorCost;
    //Current costs
    [Header("CurrentCosts")]
    public int attackCost;
    public int bodyDamageCost;
    public int attackSpeedCost;
    public int rangeCost;
    public int healthCost;
    public int healthRegenCost;
    public int armorCost;

    public void AddStats(int id)
    {
        switch (id) 
        {
            case 0:
                if (inGameSilver.silverAmount >= attackCost)
                {
                    attackUpgradesCount += 1;
                    inGameSilver.silverAmount -= attackCost;
                }
                break;
            case 1:
                if (inGameSilver.silverAmount >= bodyDamageCost)
                {
                    bodyDamageUpgradesCount += 1;
                    inGameSilver.silverAmount -= bodyDamageCost;
                }
                break;
            case 2:
                if (inGameSilver.silverAmount >= attackSpeedCost)
                {
                    attackSpeedUpgradesCount += 1;
                    inGameSilver.silverAmount -= attackSpeedCost;
                }
                break;
            case 3:
                if (inGameSilver.silverAmount >= rangeCost)
                {
                    rangeUpgradesCount += 1;
                    inGameSilver.silverAmount -= rangeCost;
                }
                break;
            case 4:
                if (inGameSilver.silverAmount >= healthCost)
                {
                    healthUpgradesCount += 1;
                    inGameSilver.silverAmount -= healthCost;
                }
                break;
            case 5:
                if (inGameSilver.silverAmount >= healthRegenCost)
                {
                    healthRegenUpgradesCount += 1;
                    inGameSilver.silverAmount -= healthRegenCost;
                }
                break;
            case 6:
                if (inGameSilver.silverAmount >= armorCost)
                {
                    armorUpgradesCount += 1;
                    inGameSilver.silverAmount -= armorCost;
                }
                break;
            default: break;
        }
    }
    public void ScaleCosts()
    {
        attackCost = baseAttackCost * attackUpgradesCount;
        bodyDamageCost = baseBodyDamageCost * bodyDamageUpgradesCount;
        attackSpeedCost = baseAttackSpeedCost * attackSpeedUpgradesCount;
        rangeCost = baseRangeCost * rangeUpgradesCount;
        healthCost = baseHealthCost * healthUpgradesCount;
        healthRegenCost = baseHealthRegenCost * healthRegenUpgradesCount;
        armorCost = baseArmorCost * armorUpgradesCount;
    }
}
