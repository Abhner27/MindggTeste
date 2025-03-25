using System.Threading;
using UnityEngine;

public abstract class Worker : MonoBehaviour
{
    protected const string IS_WORKING_ANIM_PARAM = "IsWorking";
    protected const float TIME_BETWEEN_CHECKS = 1f;

    [SerializeField]
    protected WorkersSO _workerData;

    protected float _timeOfAction = 1f;
    public float TimeOfAction { get => _timeOfAction; private set{ } }

    protected int _upgradeCost = 2;
    public int UpgradeCost { get => _upgradeCost; private set { } }

    private int _level = 1;
    public float Level { get => _level; private set { } }

    public delegate void WorkState(bool isWorking);
    public virtual event WorkState OnWorkStateChanged;

    protected CancellationTokenSource _cancellationTokenSource;

    public delegate void NewWork(float time, CancellationToken cancellationToken);
    public virtual event NewWork OnNewWorkStarted;

    public delegate void WorkComplete();
    public virtual event WorkComplete OnWorkCompleted;

    private void Start()
    {
        InitializeData();
    }

    protected virtual void InitializeData()
    {
        _timeOfAction = _workerData.TimeOfAction;
        _upgradeCost = _workerData.UpgradeCost;
    }

    protected abstract void Work();

    protected abstract void CheckToWork();

    public virtual bool Upgrade()
    {
        _level++;
        _upgradeCost = Mathf.RoundToInt(_upgradeCost * _workerData.UpgradeCostIncreaseFactor);
        _timeOfAction *= _workerData.TimeOfActionDecreasePercentage;
        return true;
    }

}
