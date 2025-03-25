using UnityEngine;

[CreateAssetMenu(fileName = "WorkersData", menuName = "WorkersData")]
public class WorkersSO : ScriptableObject
{
    [Header("General")]
    [SerializeField]
    private float _timeOfAction = 10f;
    public float TimeOfAction { get => _timeOfAction; private set { } }

    [SerializeField]
    [Range(0.01f, 0.99f)]
    private float _timeOfActionDecreasePercentage = 0.98f;
    public float TimeOfActionDecreasePercentage { get => _timeOfActionDecreasePercentage; private set { } }

    [SerializeField]
    private int _upgradeCost = 2;
    public int UpgradeCost { get => _upgradeCost; private set { } }

    [SerializeField]
    [Range(1.3f, 2f)]
    private float _upgradeCostIncreaseFactor = 1.5f;
    public float UpgradeCostIncreaseFactor { get => _upgradeCostIncreaseFactor; private set { } }


    [Header("Lumberjacker")]
    [SerializeField]
    private float _lumberjackerTimeOfAction = 10f;
    public float LumberjackerTimeOfAction { get => _lumberjackerTimeOfAction; private set { } }

    [SerializeField]
    private float _lumberjackerTreeRespawnTime = 7f;
    public float LumberjackerTreeRespawnTime { get => _lumberjackerTreeRespawnTime; private set { } }

    [SerializeField]
    [Range(0.01f, 0.99f)]
    private float _lumberjackerTreeDecreaseTimeFactor = 0.98f;
    public float LumberjackerTreeDecreaseTimeFactor { get => _lumberjackerTreeDecreaseTimeFactor; private set { } }

    [Header("Carrier")]
    [SerializeField]
    private float _carrierTimeOfAction = 1f;
    public float CarrierTimeOfAction { get => _carrierTimeOfAction; private set { } }

    [SerializeField]
    private float _carrierSpeed = 0.5f;
    public float CarrierSpeed { get => _carrierSpeed; private set { } }

    [SerializeField]
    [Range(1.01f, 2f)]
    private float _carrierSpeedIncreaseFactor = 1.2f;
    public float CarrierSpeedIncreaseFactor { get => _carrierSpeedIncreaseFactor; private set { } }

    [Header("Carpenter")]
    [SerializeField]
    private float _carpenterTimeOfAction = 10f;
    public float CarpenterTimeOfAction { get => _carpenterTimeOfAction; private set { } }

    [SerializeField]
    private int _carpenterPlanksSellValue = 5;
    public int CarpenterPlanksSellValue { get => _carpenterPlanksSellValue; private set { } }

    [SerializeField]
    [Range(1.01f, 2f)]
    private float _carpenterPlanksSellIncreasePercentage = 1.1f;
    public float CarpenterPlanksSellIncreasePercentage { get => _carpenterPlanksSellIncreasePercentage; private set { } }

}