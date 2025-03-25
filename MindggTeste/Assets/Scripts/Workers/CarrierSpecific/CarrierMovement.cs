using UnityEngine;

[RequireComponent(typeof(Carrier))]
public class CarrierMovement : MoveToPostion
{
    private Carrier _carrier;

    private void Awake()
    {
        _carrier = GetComponent<Carrier>();

        _carrier.OnWorkStateChanged += SetCanMove;
        _carrier.OnUpgradePurchased += SetSpeed;
        _carrier.OnMoveTargetUpdated += SetMovePosition;
    }

    private void OnDestroy()
    {
        _carrier.OnWorkStateChanged -= SetCanMove;
        _carrier.OnUpgradePurchased -= SetSpeed;
        _carrier.OnMoveTargetUpdated -= SetMovePosition;
    }
}
