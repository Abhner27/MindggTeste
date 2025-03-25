using UnityEngine;

public class DeveloperTestDebug : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            GameManager.Instance.Money.Add(1000);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            TwoTimes();
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ThreeTimes();
        else if (Input.GetKeyDown(KeyCode.Space))
            NormalTime();

    }

    public void NormalTime()
    {
        CustomTimeManager.SetTimeMultiplier(1f);
    }

    public void TwoTimes()
    {
        CustomTimeManager.SetTimeMultiplier(2f);
    }
    public void ThreeTimes()
    {
        CustomTimeManager.SetTimeMultiplier(3f);
    }
}
