using UnityEngine;

public class Movement : MonoBehaviour
{
    public KeyboardInput input;
    public float moveSpeed;
    public bool isPause;

    public void Init(float moveSpeed)
    {
        input = new KeyboardInput();
        this.moveSpeed = moveSpeed;
        isPause = false;
    }

    private void FixedUpdate()
    {
        if (!isPause) HandleInputKeyboard();
    }

    public void HandleInputKeyboard()
    {
        PlayerAction action = input.GetPlayerAction();
        HandleBehaviour(action);
    }

    public void HandleBehaviour(PlayerAction action)
    {
        switch (action)
        {
            case PlayerAction.Idle:

                break;

            case PlayerAction.Left:
                MoveLeft();
                break;

            case PlayerAction.Right:
                MoveRight();
                break;

            case PlayerAction.Front:
                MoveForward();
                break;

            case PlayerAction.Back:
                MoveBackward();
                break;

            case PlayerAction.Shoot1:

                break;

            case PlayerAction.Shoot2:

                break;
        }
    }

    public void DecreaseMoveSpeed(float speed)
    {
        moveSpeed -= speed;
    }

    public void IncreaseMoveSpeed(float speed)
    {
        moveSpeed += speed;
    }

    public void MoveLeft()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    public void MoveRight()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    public void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public void MoveBackward()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

}
