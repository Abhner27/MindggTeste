using UnityEngine;

[RequireComponent(typeof(Worker))]
public class WorkerAnim : MonoBehaviour
{
    protected const string IS_WORKING_ANIM_PARAM = "IsWorking";

    protected Worker _worker;

    [SerializeField]
    protected Animator _animator;

    protected virtual void Awake()
    {
        _worker = GetComponent<Worker>();

        _worker.OnWorkStateChanged += SetIsWorkingAnimation;
    }

    private void SetIsWorkingAnimation(bool isWorking) => _animator.SetBool(IS_WORKING_ANIM_PARAM, isWorking);

    protected virtual void OnDestroy()
    {
        _worker.OnWorkStateChanged -= SetIsWorkingAnimation;
    }
}