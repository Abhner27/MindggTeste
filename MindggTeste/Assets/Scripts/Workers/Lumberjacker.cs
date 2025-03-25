using System.Threading;
using UnityEngine;

public class Lumberjacker : Worker
{
    [SerializeField]
    private Tree _tree;
    private float _treeRespawnTime = 5f;
    private float _treeRespawnTimeSaved;

    [SerializeField]
    private Rain _rain;
    private bool _isRaining = false;

    public override event WorkState OnWorkStateChanged;
    public override event NewWork OnNewWorkStarted;
    public override event WorkComplete OnWorkCompleted;
    public event NewWork OnNewTreeRespawnStarted;

    protected override void InitializeData()
    {
        base.InitializeData();
        _timeOfAction = _workerData.LumberjackerTimeOfAction;
        _treeRespawnTime = _workerData.LumberjackerTreeRespawnTime;
        Work();

        if (_rain != null)
        {
            _rain.OnRainStarted += RainBuff;
            _rain.OnRainEnded += RainDebuff;
        }
    }

    protected async override void CheckToWork()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        OnNewTreeRespawnStarted?.Invoke(_treeRespawnTime, _cancellationTokenSource.Token);

        await CustomTimeManager.WaitForGameTime(_treeRespawnTime);
        _tree.Respawn();
        Work();
    }

    protected async override void Work()
    {
        OnWorkStateChanged?.Invoke(true);

        _cancellationTokenSource = new CancellationTokenSource();
        OnNewWorkStarted?.Invoke(_timeOfAction, _cancellationTokenSource.Token);

        await CustomTimeManager.WaitForGameTime(_timeOfAction, _cancellationTokenSource.Token);

        OnWorkStateChanged?.Invoke(false);

        if (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            _tree.Cut();
            OnWorkCompleted?.Invoke();
        }

        CheckToWork();
    }

    public void RainBuff()
    {
        _isRaining = true;
        _treeRespawnTimeSaved = _treeRespawnTime;
        _treeRespawnTime = 0.1f;
    }

    public void RainDebuff()
    {
        _isRaining = false;
        _treeRespawnTime = _treeRespawnTimeSaved;
    }

    public override bool Upgrade()
    {
        if (_isRaining)
        {
            GameManager.Instance.NotificationsManager.EnqueueNewNotification(new Notification("It's Raining!", "Can't upgrade this while raining!", NotificationDatas.Instance.RainSprite, Notification.Templates.Wood));
            GameManager.Instance.Money.Add(UpgradeCost);
            return false;
        }

        base.Upgrade();
        _treeRespawnTime *= _workerData.LumberjackerTreeDecreaseTimeFactor;
        return true;
    }

    private void OnDestroy()
    {
        if (_rain != null)
        {
            _rain.OnRainStarted -= RainBuff;
            _rain.OnRainEnded -= RainDebuff;
        }
    }
}
