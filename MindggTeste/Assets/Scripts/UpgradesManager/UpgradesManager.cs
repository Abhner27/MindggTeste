using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public delegate void UpgradePurchase();
    public event UpgradePurchase OnUpgradePurchased;

    public void Upgrade(Worker worker)
    {
        //If you cant reduce the money amount
        if (!GameManager.Instance.Money.Reduce(worker.UpgradeCost))
            return;

        if (!worker.Upgrade())
            return;

        OnUpgradePurchased?.Invoke();
    }
}
