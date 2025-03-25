using UnityEngine;

[RequireComponent(typeof(Worker))]
public class WorkerActionUI : ActioUIBase
{
    private Worker _worker;

    [SerializeField]
    private AddItemAnim _addItemAnim;

    private void Awake()
    {
        _worker = GetComponent<Worker>();

        _worker.OnNewWorkStarted += AtualizarUI;
        _worker.OnWorkCompleted += WorkDone;
    }

    private void WorkDone()
    {
        _addItemAnim?.Play();
    }

    private void OnDestroy()
    {
        _worker.OnNewWorkStarted -= AtualizarUI;
        _worker.OnWorkCompleted -= WorkDone;
    }
}