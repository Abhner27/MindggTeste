using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarrierMovement))]
public class Carrier : Worker
{
    private const float CARPENTER_X_POSITION = -2.5f;

    private CarrierMovement _workerMovement;
    private Queue<WoodLog> _woodLogsQueue = new Queue<WoodLog>();
    [SerializeField]
    private GameObject _woodLog;

    public override event WorkState OnWorkStateChanged;

    public delegate void UpgradePurchase(float speed);
    public event UpgradePurchase OnUpgradePurchased;

    public delegate void MoveTargetUpdate(float x);
    public event MoveTargetUpdate OnMoveTargetUpdated;

    public delegate void LogState();
    public event LogState OnLogGrabbed;
    public event LogState OnLogDelivered;

    protected override void InitializeData()
    {
        base.InitializeData();

        _workerMovement = GetComponent<CarrierMovement>();

        _timeOfAction = _workerData.CarrierTimeOfAction;
        _workerMovement.Speed = _workerData.CarrierSpeed;

        CheckToWork();
    }

    public void AddToWoodLogQueue(WoodLog woodLog) => _woodLogsQueue.Enqueue(woodLog);

    protected async override void CheckToWork()
    {
        while (_woodLogsQueue.Count == 0)
        {
            await CustomTimeManager.WaitForGameTime(TIME_BETWEEN_CHECKS);
        }

        Work();
    }

    protected override void Work()
    {
        _workerMovement.OnArrived += ArriveAtLog;

        OnMoveTargetUpdated?.Invoke(_woodLogsQueue.Dequeue().GetXPosition());
        OnWorkStateChanged?.Invoke(true);
    }

    private async void ArriveAtLog()
    {
        _workerMovement.OnArrived -= ArriveAtLog;

        OnWorkStateChanged?.Invoke(false);
        OnLogGrabbed?.Invoke();
        _woodLog.SetActive(true);

        await CustomTimeManager.WaitForGameTime(_timeOfAction);

        GoToCarpenter();
    }

    private void GoToCarpenter()
    {
        _workerMovement.OnArrived += ArriveAtCarpenter;

        OnMoveTargetUpdated?.Invoke(CARPENTER_X_POSITION);
        OnWorkStateChanged?.Invoke(true);
    }

    private async void ArriveAtCarpenter()
    {
        _workerMovement.OnArrived -= ArriveAtCarpenter;

        OnWorkStateChanged?.Invoke(false);
        OnLogDelivered?.Invoke();
        _woodLog.SetActive(false);

        await CustomTimeManager.WaitForGameTime(TIME_BETWEEN_CHECKS);
        CheckToWork();
    }

    public override bool Upgrade()
    {
        base.Upgrade();
        OnUpgradePurchased?.Invoke(_workerMovement.Speed * _workerData.CarrierSpeedIncreaseFactor);
        return true;
    }
}
