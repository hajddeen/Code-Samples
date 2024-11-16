using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    private UpgradesController upgradesController;
    public int statToUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        upgradesController = GameObject.Find("----PlayerManager----").GetComponent<UpgradesController>();
    }
    public void ButtonClicked()
    {
        upgradesController.upgradesBought.AddStats(statToUpgrade);
    }
    public void HideUI()
    {
        upgradesController.gameUI.SetActive(false);
    }
}
