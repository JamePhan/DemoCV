using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput
{
    private readonly Dictionary<KeyCode, PlayerAction> actionForKey = new Dictionary<KeyCode, PlayerAction>
    {
         { KeyCode.None, PlayerAction.Idle },
         { KeyCode.A, PlayerAction.Left },
         { KeyCode.D, PlayerAction.Right },
         { KeyCode.S, PlayerAction.Back },
         { KeyCode.W, PlayerAction.Front },
         //{ KeyCode.Space, PlayerAction.Shoot },
         { KeyCode.Mouse0, PlayerAction.Shoot1 },
         { KeyCode.Mouse1, PlayerAction.Shoot2 },
    };

    public KeyboardInput()
    {

    }

    public PlayerAction GetPlayerAction()
    {
        return actionForKey[GetActionKeyDown()];
    }

    private KeyCode GetActionKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.W)) return KeyCode.W;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A)) return KeyCode.A;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D)) return KeyCode.D;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.S)) return KeyCode.S;
        //if (Input.GetKeyDown(KeyCode.Space)) return KeyCode.Space;
        if (Input.GetKeyDown(KeyCode.Mouse0)) return KeyCode.Mouse0;
        if (Input.GetKeyDown(KeyCode.Mouse1)) return KeyCode.Mouse1;
        return KeyCode.None;
    }

    public PlayerAction CancelPlayerAction()
    {
        return actionForKey[GetActionKeyUp()];
    }

    private KeyCode GetActionKeyUp()
    {
        if (Input.GetKeyUp(KeyCode.A)) return KeyCode.A;
        if (Input.GetKeyUp(KeyCode.D)) return KeyCode.D;
        return KeyCode.None;
    }

}
