using UnityEngine;

public class TreeRespawnUI : ActioUIBase
{
    [SerializeField]
    private Lumberjacker _lumberjacker;

    private void Start()
    {
        _lumberjacker.OnNewTreeRespawnStarted += AtualizarUI;
    }

    private void OnDestroy()
    {
        _lumberjacker.OnNewTreeRespawnStarted -= AtualizarUI;
    }
}
