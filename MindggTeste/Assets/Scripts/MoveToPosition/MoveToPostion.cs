using UnityEngine;

public class MoveToPostion : MonoBehaviour
{
    private const float ARRIVED_DISTANCE = 0.1f;

    private bool _canMove = false;
    private Vector2 _posToMove;
    public float Speed = 0.5f;

    public delegate void ArrivedAtPosition();
    public event ArrivedAtPosition OnArrived;

    protected void SetCanMove(bool canMove) => _canMove = canMove;

    protected void SetMovePosition(float xPos)
    {
        _posToMove = new Vector2(xPos, transform.position.y);

        if (transform.position.x < xPos)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }

    protected void SetMovePosition(Vector2 pos)
    {
        _posToMove = pos;

        if (transform.position.x < pos.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }

    public void Move(Vector2 targetPosition, float speed = 1f)
    {
        SetMovePosition(targetPosition);
        SetSpeed(speed);
        SetCanMove(true);
    }

    protected void SetSpeed(float speed) => Speed = speed;

    private void FixedUpdate()
    {
        if (!_canMove)
            return;

        if (Vector2.Distance(transform.position, _posToMove) > ARRIVED_DISTANCE)
            transform.position = Vector2.MoveTowards(transform.position, _posToMove, Speed * CustomTimeManager.GetDeltaTime());
        else
        {
            _canMove = false;
            OnArrived?.Invoke();
        }
    }
}