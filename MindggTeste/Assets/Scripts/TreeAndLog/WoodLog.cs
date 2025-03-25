using System.Threading;
using UnityEngine;

public class WoodLog : MonoBehaviour
{
    private void Start() => GameManager.Instance.Carrier.AddToWoodLogQueue(this);
    public float GetXPosition()
    {
        GameManager.Instance.Carrier.OnLogGrabbed += Carrier_OnLogGrabbed;
        return transform.position.x;
    }

    private void Carrier_OnLogGrabbed()
    {
        GameManager.Instance.Carrier.OnLogGrabbed -= Carrier_OnLogGrabbed;
        Destroy(gameObject);
    }
}
