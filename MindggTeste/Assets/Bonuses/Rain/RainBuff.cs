public class RainBuff : RainEventsBase
{
    protected override void _rain_OnRainStarted()
    {
        GameManager.Instance.Lumberjacker.RainBuff();
    }

    protected override void _rain_OnRainEnded()
    {
        GameManager.Instance.Lumberjacker.RainDebuff();
    }
}