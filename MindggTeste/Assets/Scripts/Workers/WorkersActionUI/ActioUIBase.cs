using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ActioUIBase : MonoBehaviour
{
    [SerializeField]
    private Image _fillImage;
    protected async void AtualizarUI(float tempo, CancellationToken token)
    {
        Progress<float> progress = new Progress<float>();
        progress.ProgressChanged += Progress_ProgressChanged;

        await CustomTimeManager.WaitForGameTime(tempo, token, progress);

        //Reset if cancelled!
        if (token.IsCancellationRequested)
            _fillImage.fillAmount = 0f;
    }

    protected void Progress_ProgressChanged(object sender, float e)
    {
        if (_fillImage != null)
            _fillImage.fillAmount = e;

        //Hide image after completed!
        if (_fillImage.fillAmount == 1f)
            _fillImage.fillAmount = 0f;
    }
}