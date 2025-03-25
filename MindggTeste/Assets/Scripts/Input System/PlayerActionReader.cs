using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionReader : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    //For Interacting with mouse click!
    public delegate void PlayerInteract(Vector2 pos);
    public event PlayerInteract OnPlayerLeftClick;

    private void Awake()
    {
        //Initialize
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        _playerInputActions.Player.Select.performed += Select;
    }

    private void Select(InputAction.CallbackContext obj)
    {
        Vector2 pos = GetMousePos();

        OnPlayerLeftClick?.Invoke(pos);
    }


    private Vector2 GetMousePos()
    {
        // This pos comes in screen coordinates
        Vector2 pos = _playerInputActions.Player.MousePos.ReadValue<Vector2>();

        // Transforms it to world coordinates
        pos = Camera.main.ScreenToWorldPoint(pos);
        return pos;
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Select.performed -= Select;
    }
}