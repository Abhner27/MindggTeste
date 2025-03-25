using System.Threading;
using UnityEngine;

public class Carpenter : Worker
{
    public override event WorkState OnWorkStateChanged;

    private int _amountOfLogs = 0;
    private int _plankValue = 10;

    public delegate void PlankPriceChange(int plankValue);
    public event PlankPriceChange OnPlankPriceChanged;

    public override event NewWork OnNewWorkStarted;
    public override event WorkComplete OnWorkCompleted;

    protected override void InitializeData()
    {
        base.InitializeData();

        _timeOfAction = _workerData.CarpenterTimeOfAction;
        _plankValue = _workerData.CarpenterPlanksSellValue;

        OnPlankPriceChanged?.Invoke(_plankValue);

        GameManager.Instance.Carrier.OnLogDelivered += AddLog;
        CheckToWork();
    }

    public void AddLog() => _amountOfLogs++;

    protected async override void CheckToWork()
    {
        while (_amountOfLogs == 0)
        {
            await CustomTimeManager.WaitForGameTime(TIME_BETWEEN_CHECKS);
        }

        Work();
    }

    protected async override void Work()
    {
        OnWorkStateChanged?.Invoke(true);

        _cancellationTokenSource = new CancellationTokenSource();
        OnNewWorkStarted?.Invoke(_timeOfAction, _cancellationTokenSource.Token);

        await CustomTimeManager.WaitForGameTime(_timeOfAction);

        OnWorkStateChanged?.Invoke(false);

        SellPlank();

        CheckToWork();
    }

    private void SellPlank()
    {
        GameManager.Instance.Money.Add(_plankValue);
        _amountOfLogs--;
        OnWorkCompleted?.Invoke();
    }

    public override bool Upgrade()
    {
        base.Upgrade();
        _plankValue = Mathf.RoundToInt(_plankValue * _workerData.CarpenterPlanksSellIncreasePercentage);
        OnPlankPriceChanged?.Invoke(_plankValue);
        return true;
    }

    private void OnDestroy()
    {
        GameManager.Instance.Carrier.OnLogDelivered -= AddLog;
    }
}