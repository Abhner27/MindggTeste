using UnityEngine;

[RequireComponent(typeof(CarrierMovement))]
public class CarrierAnim : WorkerAnim
{
    private const string GRAB_OR_PLACE_ANIMATION_TRIGGER = "GrabOrPlace";
    private const string WALK_ANIMATION_MULTIPLIER = "SpeedMultiplier";

    private CarrierMovement _workerMovement;

    protected override void Awake()
    {
        base.Awake();

        _workerMovement = GetComponent<CarrierMovement>();
        SetSpeedMultiplier(_workerMovement.Speed);

        Carrier carrier = (Carrier)_worker;
        carrier.OnLogGrabbed += PlayGrabAnimation;
        carrier.OnLogDelivered += PlayGrabAnimation;
        carrier.OnUpgradePurchased += SetSpeedMultiplier;
    }

    private void PlayGrabAnimation() => _animator.SetTrigger(GRAB_OR_PLACE_ANIMATION_TRIGGER);
    private void SetSpeedMultiplier(float speed) => _animator.SetFloat(WALK_ANIMATION_MULTIPLIER, speed);

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Carrier carrier = (Carrier)_worker;
        carrier.OnLogGrabbed -= PlayGrabAnimation;
        carrier.OnLogDelivered -= PlayGrabAnimation;
        carrier.OnUpgradePurchased -= SetSpeedMultiplier;
    }
}
