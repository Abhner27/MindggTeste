public class GameTimeStateController
{
    public delegate void OnGameStateChange();
    public event OnGameStateChange OnPause;
    public event OnGameStateChange OnResume;

    public enum GameState
    {
        Playing,
        Paused
    }

    private GameState _currentGameState;
    public GameState CurrentGameState { get => _currentGameState; private set { } }

    public void Pause()
    {
        if (_currentGameState == GameState.Paused)
            return;

        _currentGameState = GameState.Paused;
        OnPause?.Invoke();
    }
    public void Resume()
    {
        if (_currentGameState == GameState.Playing)
            return;

        _currentGameState = GameState.Playing;
        OnResume?.Invoke();
    }
}
