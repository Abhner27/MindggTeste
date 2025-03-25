using UnityEngine;

public class Rain : MonoBehaviour
{
    private Vector2 _startPosition = new Vector2(-13.5f, 6.8f);
    private Vector2 _rainPosition = new Vector2(-3.5f, 6.8f);
    private Vector2 _finalPosition = new Vector2(9f, 6.8f);
    private float _speedOfMovement = 1f;

    private MoveToPostion _moveToPosition;

    private Color _initialColor = Color.white;
    private Color _rainColor = Color.gray;
    private float _speedOfTransition = 1.5f;

    [SerializeField]
    private ChangeColorOverTimeEffect[] _cloudsAndBackground;

    [SerializeField]
    private float _rainDuration = 40f;

    public delegate void RainState();
    public event RainState OnRainStarted;
    public event RainState OnRainEnded;

    private void Start()
    {
        _moveToPosition = GetComponent<MoveToPostion>();

        _startPosition = transform.position;
    }


    public void StartRain()
    {
        _moveToPosition.Move(_rainPosition, _speedOfMovement);
        _moveToPosition.OnArrived += OnRainPostionArrive;

        GameManager.Instance.NotificationsManager.EnqueueNewNotification(new Notification("It's gonna rain!", "Rain makes trees grow instantly!", NotificationDatas.Instance.RainSprite, Notification.Templates.Paper));
    }

    private async void OnRainPostionArrive()
    {
        _moveToPosition.OnArrived -= OnRainPostionArrive;

        foreach (ChangeColorOverTimeEffect changeColorOverTimeEffect in _cloudsAndBackground)
        {
            changeColorOverTimeEffect.UpdateColor(_rainColor, _speedOfTransition);
        }

        await CustomTimeManager.WaitForGameTime(_speedOfTransition);

        OnRainStarted?.Invoke();
        EndRain();
    }

    private async void EndRain()
    {
        await CustomTimeManager.WaitForGameTime(_rainDuration);

        OnRainEnded?.Invoke();

        foreach (ChangeColorOverTimeEffect changeColorOverTimeEffect in _cloudsAndBackground)
        {
            changeColorOverTimeEffect.UpdateColor(_initialColor, _speedOfTransition);
        }

        await CustomTimeManager.WaitForGameTime(_speedOfTransition);

        _moveToPosition.OnArrived += ResetPosition;
        _moveToPosition.Move(_finalPosition, _speedOfMovement);
    }

    private void ResetPosition()
    {
        _moveToPosition.OnArrived -= ResetPosition;
        transform.position = _startPosition;
    }
}
