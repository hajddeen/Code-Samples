using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesController : MonoBehaviour
{
    public UpgradesBought upgradesBought = new UpgradesBought();
    public GameObject gameUI;
    private void Start()
    {
        gameUI.SetActive(false);
    }
    private void Update()
    {
        upgradesBought.ScaleCosts();
    }
    public void ShowShop()
    {
        gameUI.SetActive(true);
    }
}
