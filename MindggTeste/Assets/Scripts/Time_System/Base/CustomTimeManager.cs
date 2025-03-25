using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using System;

public static class CustomTimeManager
{
    private static float _currentTime = 0f; // Stores the current game time
    private static float _playedTime = 0f; // Stores the time when the game was played
    public static float PlayedTime { get => _playedTime; private set { } }

    private static float _pausedTime = 0f;  // Stores the time when the game was paused

    private static float _timeMultiplier = 1f; // Multiplies the time by a factor, to make it pass faster or slower!

    private static bool _canCountTime = true; // Controls whether time should be counted

    [EventMethod(EventType.Subscribe)]
    public static void Subscribe()
    {
        // Subscribe to pause and resume events
        GameManager.Instance.GameTimeStateController.OnPause += OnPause;
        GameManager.Instance.GameTimeStateController.OnResume += OnResume;
    }

    [InitializeMethod]
    public static void Initialize()
    {
        // Start counting game time
        CountTime();
        CountPlayedTime();
    }

    private static async Task CountTime()
    {
        // Keeps track of the time spent while paused
        while (true)
        {
            _currentTime += Time.deltaTime * _timeMultiplier;
            await Task.Yield();
        }
    }

    private static async Task CountPlayedTime()
    {
        // Continuously updates the game time while not paused
        while (_canCountTime)
        {
            _playedTime = _currentTime - _pausedTime;
            await Task.Yield();
        }
    }

    private static async Task CountPausedTime()
    {
        // Keeps track of the time spent while paused
        while (!_canCountTime)
        {
            _pausedTime = _currentTime - _playedTime;
            await Task.Yield();
        }
    }

    public static float GetDeltaTime() => Time.deltaTime * _timeMultiplier;

    public static void SetTimeMultiplier(float multiplier)
    {
        _timeMultiplier = Mathf.Max(multiplier, 0.1f); // Don't allow negative or 0 values
    }


    public static async Task WaitForGameTime(float tempo)
    {
        // Waits for a specific amount of in-game time to pass
        float tempoDeEspera = _playedTime + tempo;

        while (_playedTime < tempoDeEspera)
        {
            await Task.Yield();
        }
    }

    public static async Task WaitForGameTime(float tempo, CancellationToken token)
    {
        // Waits for a specific amount of time, but allows cancellation
        float tempoDeEspera = _playedTime + tempo;

        while (_playedTime < tempoDeEspera)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }

            await Task.Yield();
        }
    }

    public static async Task WaitForGameTime(float tempo, CancellationToken token, IProgress<float> progress)
    {
        // Waits for a specific amount of time, allows cancellation, and reports progress
        float tempoDeEspera = _playedTime + tempo;

        while (_playedTime < tempoDeEspera)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }

            await Task.Yield();
            progress.Report(1f - ((tempoDeEspera - _playedTime) / tempo));
        }
    }

    private static void OnPause()
    {
        // Pauses time counting
        _canCountTime = false;
        CountPausedTime();
    }

    private static void OnResume()
    {
        // Resumes time counting
        _canCountTime = true;
        CountPlayedTime();
    }

    [EventMethod(EventType.Unsubscribe)]
    public static void Unsubscribe()
    {
        // Unsubscribes from pause and resume events
        GameManager.Instance.GameTimeStateController.OnPause -= OnPause;
        GameManager.Instance.GameTimeStateController.OnResume -= OnResume;
    }

    #region TIMERS

    public abstract class Timers
    {
        protected const float TIME_CHANGE_RATE = 0.1f;

        protected float _maxTime = 0f;
        protected float _time = 0f;

        public abstract Task Start(CancellationToken token);
        public abstract Task Start(IProgress<float> progress);
        public abstract Task Start(CancellationToken token, IProgress<float> progress);
    }

    public class Countdown : Timers
    {
        public Countdown(float time)
        {
            _maxTime = _time = time;
        }

        public Countdown(float time, float maxTime)
        {
            _time = time;
            _maxTime = maxTime;
        }

        public override async Task Start(CancellationToken token)
        {
            while (_time > 0f)
            {
                _time -= TIME_CHANGE_RATE;
                await CustomTimeManager.WaitForGameTime(TIME_CHANGE_RATE);

                if (token.IsCancellationRequested)
                {
                    return;
                }
            }
        }

        public override async Task Start(IProgress<float> progress)
        {
            while (_time > 0f)
            {
                progress.Report(_time / _maxTime);

                _time += TIME_CHANGE_RATE;

                await CustomTimeManager.WaitForGameTime(TIME_CHANGE_RATE);
            }
        }

        public override async Task Start(CancellationToken token, IProgress<float> progress)
        {
            while (_time > 0f)
            {
                progress.Report(_time / _maxTime);

                _time -= TIME_CHANGE_RATE;

                await CustomTimeManager.WaitForGameTime(TIME_CHANGE_RATE);

                if (token.IsCancellationRequested)
                {
                    return;
                }
            }
        }

        public void AddAmountToTime(float addAmount)
        {
            if (addAmount < 0f)
                return;

            _time += addAmount;

            if (_time > _maxTime)
                _time = _maxTime;
        }
    }

    public class Countup : Timers
    {
        public Countup(float time)
        {
            _maxTime = time;
            _time = 0f;
        }

        public override async Task Start(CancellationToken token)
        {
            while (_time < _maxTime)
            {
                _time += TIME_CHANGE_RATE;
                await CustomTimeManager.WaitForGameTime(TIME_CHANGE_RATE);

                if (token.IsCancellationRequested)
                {
                    return;
                }
            }
        }

        public override async Task Start(IProgress<float> progress)
        {
            while (_time < _maxTime)
            {
                progress.Report(_time / _maxTime);

                _time += TIME_CHANGE_RATE;

                await CustomTimeManager.WaitForGameTime(TIME_CHANGE_RATE);
            }
        }

        public override async Task Start(CancellationToken token, IProgress<float> progress)
        {
            while (_time < _maxTime)
            {
                progress.Report(_time / _maxTime);

                _time += TIME_CHANGE_RATE;

                await CustomTimeManager.WaitForGameTime(TIME_CHANGE_RATE);

                if (token.IsCancellationRequested)
                {
                    return;
                }
            }
        }

        public void AddAmountToMaxTime(float addAmount)
        {
            _maxTime += addAmount;
        }
    }

    #endregion
}
/* EXAMPLE OF USE OF THE "Countdown" AND "Countup" CLASSES!!!
 
 
//How the Countdown and Countup classes can be used:
public class CountdownOrCountUpTestClassExample
{
    //It must create an tokensource
    private CancellationTokenSource _tokenSource;
    //And a reference to the countdown or countUp class!
    private CustomTimeManager.Countup _countUp;

    //Then it should have a method where they are initialized:
    public async void Test()
    {
        //So first we create an instance of the class passing the time we want to count from/to
        _countUp = new CustomTimeManager.Countup(10f);

        //Then we also create an instance of the tokenSource for cancelling our actions!
        _tokenSource = new CancellationTokenSource();

        //Finally we create a progress of the type float to get the percentege of how much of the countdown or the countUp are completed! (from 0 to 1)
        Progress<float> progress = new Progress<float>();

        //We add it to a method where we can do logic with this progress number that is passed!
        progress.ProgressChanged += Progress_ProgressChanged;

        //Then we can wait for the conclusion of the countdown or countUp! 
        //(However, it is not necessary to wait! We can keep this method sync!)
        await _countUp.Start(_tokenSource.Token, progress);

        //Then we can do some logic in case the action gets cancelled at any point!
        if (_tokenSource.IsCancellationRequested)
        {
            //For now we just debug a message!
            Debug.Log("Operation cancelled!");
        }
    }

    private void Progress_ProgressChanged(object sender, float e)
    {
        //For now we just debug the value!  ( 0f -> 1f )
        Debug.Log(e); 
    }

    //Method to cancel the action!
    public void Cancel() => _tokenSource.Cancel();

    //Method to add time to the countDown or countUp!
    //(It can be used in realtime!)
    public void Add(float value) => _countUp.AddAmountToMaxTime(value);
}
*/
