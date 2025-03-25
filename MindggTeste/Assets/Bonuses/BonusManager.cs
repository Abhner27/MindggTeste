using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [Header("Rain")]
    [SerializeField]
    private float _minRainBonusTime = 90f;
    [SerializeField]
    private float _maxRainBonusTime = 120f;
    [SerializeField]
    private float _rainBonusDurationTime = 10f;

    [SerializeField]
    private GameObject _rainBonus;

    [Header("Chest")]
    [SerializeField]
    private float _minChestBonusTime = 30f;
    [SerializeField]
    private float _maxChestBonusTime = 60f;
    [SerializeField]
    private float _chestBonusDurationTime = 7f;

    [SerializeField]
    private GameObject _chestBonus;


    private void Start()
    {
        _rainBonus.SetActive(false);
        _chestBonus.SetActive(false);
        ShowChestBonus();
        ShowRainBonus();
    }

    private async void ShowChestBonus()
    {
        if (!Application.isPlaying) return;

        float waitTime = Random.Range(_minChestBonusTime, _maxChestBonusTime);
        await CustomTimeManager.WaitForGameTime(waitTime);
        _chestBonus.SetActive(true);
        await CustomTimeManager.WaitForGameTime(_chestBonusDurationTime);
        _chestBonus.SetActive(false);
        ShowChestBonus();
    }

    private async void ShowRainBonus()
    {
        if (!Application.isPlaying) return;

        float waitTime = Random.Range(_minRainBonusTime, _maxRainBonusTime);
        await CustomTimeManager.WaitForGameTime(waitTime);
        _rainBonus.SetActive(true);
        await CustomTimeManager.WaitForGameTime(_rainBonusDurationTime);
        _rainBonus.SetActive(false);
        ShowRainBonus();
    }
}
