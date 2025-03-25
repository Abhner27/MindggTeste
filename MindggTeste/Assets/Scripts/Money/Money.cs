using UnityEngine;

public class Money : MonoBehaviour
{
    private int _amount = 0;

    public delegate void ValueChanged(int currentAmount, int changedValue);
    public event ValueChanged OnValueChanged;

    public void Add(int value)
    {
        _amount += value;
        OnValueChanged?.Invoke(_amount, value);
    }

    public bool Reduce(int value)
    {
        if (value > _amount)
            return false;

        _amount -= value;
        OnValueChanged?.Invoke(_amount, -value);
        return true;
    }
}
