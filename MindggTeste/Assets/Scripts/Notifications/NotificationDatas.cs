using UnityEngine;

[CreateAssetMenu(fileName = "NotificationData", menuName = "NotificationData")]
public class NotificationDatas : ScriptableObject
{
    public static NotificationDatas Instance;

    [Header("Seasons")]

    public Sprite RainSprite;

    private void OnEnable()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
}
