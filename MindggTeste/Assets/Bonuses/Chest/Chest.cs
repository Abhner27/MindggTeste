using UnityEngine;

public class Chest : MonoBehaviour
{
    private const string OPEN_ANIMATOR_BOOL_NAME = "IsOpened";
    private Animator _animator;
    private GameObject _chestGraphics;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _chestGraphics = transform.GetChild(0).gameObject;
        _chestGraphics.SetActive(false);
    }

    public async void Open()
    {
        _chestGraphics.SetActive(true);
        _animator.SetBool(OPEN_ANIMATOR_BOOL_NAME, true);

        await CustomTimeManager.WaitForGameTime(1f);

        GameManager.Instance.Money.Add(Mathf.RoundToInt(CustomTimeManager.PlayedTime * 2f));

        await CustomTimeManager.WaitForGameTime(1f);

        _animator.SetBool(OPEN_ANIMATOR_BOOL_NAME, false);

        await CustomTimeManager.WaitForGameTime(0.5f);

        _chestGraphics.SetActive(false);
    }
}