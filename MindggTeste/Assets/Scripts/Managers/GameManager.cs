using UnityEngine;
using System.Linq;
using System.Reflection;

public class GameManager : MonoBehaviour
{
    //For creating a singleton
    public static GameManager Instance;

    //For the time state controll
    private GameTimeStateController _gameTimeStateController;
    public GameTimeStateController GameTimeStateController 
    { 
        get => _gameTimeStateController;

        private set { } 
    }

    // For Player Input
    [SerializeField]
    private PlayerActionReader _playerActionReader;
    public PlayerActionReader PlayerActionReader { get => _playerActionReader; private set { } }

    // For Money
    [SerializeField]
    private Money _money;
    public Money Money { get => _money; private set { } }

    // For Notifications
    [SerializeField]
    private NotificationsManager _notificationsManager;
    public NotificationsManager NotificationsManager { get => _notificationsManager; private set { } }

    // For Lumberjacker
    [SerializeField]
    private Lumberjacker _lumberjacker;
    public Lumberjacker Lumberjacker { get => _lumberjacker; private set { } }

    // For Carrier
    [SerializeField]
    private Carrier _carrier;
    public Carrier Carrier { get => _carrier; private set { } }

    // For Carpenter
    [SerializeField]
    private Carpenter _carpenter;
    public Carpenter Carpenter { get => _carpenter; private set { } }

    private void Awake()
    {
        //Create the singleton pattern!
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        //Initialize the time state controller
        _gameTimeStateController = new GameTimeStateController();

        //Invoke the Initialize methods in the static classes!
        InvokeInitializeMethodsByAttribute();

        //Invoke the Subscribe methods in the static classes!
        InvokeEventMethodsByAttribute(EventType.Subscribe);
    }

    private System.Collections.Generic.IEnumerable<System.Type> GetStaticClassesInAssembly()
    {
        // Grab all the static classes from the assembly
        var staticClasses = Assembly.GetExecutingAssembly()
                                    .GetTypes()
                                    .Where(t => t.IsClass && t.IsAbstract && t.IsSealed);
        // static class in C#

        return staticClasses;
    }

    private void InvokeInitializeMethodsByAttribute()
    {
        // Grab all the static classes from the assembly
        var staticClasses = GetStaticClassesInAssembly();

        foreach (var staticClass in staticClasses)
        {
            // In these classes, we grab all methods marked with the attribute 'EvenMethodAttribute'
            var methods = staticClass.GetMethods(BindingFlags.Public | BindingFlags.Static)
                                   .Where(m => m.GetCustomAttribute<InitializeMethodAttribute>()
                                                   != null);

            foreach (var method in methods)
            {
                method.Invoke(null, null); // Invoke the static method!
            }
        }
    }

    private void InvokeEventMethodsByAttribute(EventType type)
    {
        // Grab all the static classes from the assembly
        var staticClasses = GetStaticClassesInAssembly();

        foreach (var staticClass in staticClasses)
        {
            // In these classes, we grab all methods marked with the attribute 'EventMethodAttribute'
            var methods = staticClass.GetMethods(BindingFlags.Public | BindingFlags.Static)
                                     .Where(m => m.GetCustomAttributes<EventMethodAttribute>()
                                                  .Any(attr => attr.Type == type));

            foreach (var method in methods)
            {
                method.Invoke(null, null); // Invoke the static method!
            }
        }
    }

    private void OnDestroy()
    {
        //Invoke the Unsubscribe methods in the static classes!
        InvokeEventMethodsByAttribute(EventType.Unsubscribe);
    }
}