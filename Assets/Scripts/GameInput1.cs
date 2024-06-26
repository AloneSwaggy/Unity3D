using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput1 : GameInput
{
    private GameControl.Player1Actions player1Actions;

    public static GameInput1 Instance { get; private set; }

    private const string GAMEINPU_BINDINGS = "GameInputBindings";

    private GameControl gameControl;


    private void Awake()
    {
        Instance = this;
        gameControl = new GameControl();
        player1Actions = gameControl.Player1;
        player1Actions.Enable();
    }
    protected override void InitializeGameControl()
    {
        // var gameControl = new GameControl();
        // player1Actions = gameControl.Player1;
        // player1Actions.Enable();
        SetupCallbacks(player1Actions.Interact, player1Actions.Operate, player1Actions.Pause);
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        print("开始绑定");
    //        gameControl.Player.Disable();
    //        gameControl.Player.Move.PerformInteractiveRebinding(1).OnComplete(callback =>
    //        {
    //            print(callback.action.bindings[1].path);
    //            print(callback.action.bindings[1].overridePath);

    //            callback.Dispose();
    //            print("绑定完成");
    //            gameControl.Player.Enable();
    //        }).Start();
    //    }
    //}
    public override void ReBinding(BindingType bindingType, Action onComplete)
    {
        gameControl.Player1.Disable();
        InputAction inputAction = null;
        int index = -1;
        switch (bindingType)
        {
            case BindingType.Up:
                index = 1;
                inputAction = gameControl.Player1.Move;
                break;
            case BindingType.Down:
                index = 2;
                inputAction = gameControl.Player1.Move;
                break;
            case BindingType.Left:
                index = 3;
                inputAction = gameControl.Player1.Move;
                break;
            case BindingType.Right:
                index = 4;
                inputAction = gameControl.Player1.Move;
                break;
            case BindingType.Interact:
                index = 0;
                inputAction = gameControl.Player1.Interact;
                break;
            case BindingType.Operate:
                index = 0;
                inputAction = gameControl.Player1.Operate;
                break;
            case BindingType.Pause:
                index = 0;
                inputAction = gameControl.Player1.Pause;
                break;
            default:
                break;
        }

        inputAction.PerformInteractiveRebinding(index).OnComplete(callback =>
        {
            callback.Dispose();
            gameControl.Player1.Enable();
            onComplete?.Invoke();

            PlayerPrefs.SetString(GAMEINPU_BINDINGS, gameControl.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }).Start();
    }

    public override string GetBindingDisplayString(BindingType bindingType)
    {
        switch (bindingType)
        {
            case BindingType.Up:
                Debug.Log(player1Actions.Move.bindings[1].ToDisplayString());
                return gameControl.Player1.Move.bindings[1].ToDisplayString();
            case BindingType.Down:
                Debug.Log(player1Actions.Move.bindings[2].ToDisplayString());
                return gameControl.Player1.Move.bindings[2].ToDisplayString();
            case BindingType.Left:
                return gameControl.Player1.Move.bindings[3].ToDisplayString();
            case BindingType.Right:
                return gameControl.Player1.Move.bindings[4].ToDisplayString();
            case BindingType.Interact:
                return gameControl.Player1.Interact.bindings[0].ToDisplayString();
            case BindingType.Operate:
                return gameControl.Player1.Operate.bindings[0].ToDisplayString();
            case BindingType.Pause:
                return gameControl.Player1.Pause.bindings[0].ToDisplayString();
            default:
                break;
        }
        return "";
    }

    // public void ReBinding(BindingType bindingType, Action onComplete)
    // {
    //     gameControl.Player1.Disable();
    //     InputAction inputAction = null;
    //     int index = -1;
    //     switch (bindingType)
    //     {
    //         case BindingType.Up:
    //             index = 1;
    //             inputAction = gameControl.Player1.Move;
    //             break;
    //         case BindingType.Down:
    //             index = 2;
    //             inputAction = gameControl.Player1.Move;
    //             break;
    //         case BindingType.Left:
    //             index = 3;
    //             inputAction = gameControl.Player1.Move;
    //             break;
    //         case BindingType.Right:
    //             index = 4;
    //             inputAction = gameControl.Player1.Move;
    //             break;
    //         case BindingType.Interact:
    //             index = 0;
    //             inputAction = gameControl.Player1.Interact;
    //             break;
    //         case BindingType.Operate:
    //             index = 0;
    //             inputAction = gameControl.Player1.Operate;
    //             break;
    //         case BindingType.Pause:
    //             index = 0;
    //             inputAction = gameControl.Player1.Pause;
    //             break;
    //         default:
    //             break;
    //     }

    //     inputAction.PerformInteractiveRebinding(index).OnComplete(callback =>
    //     {
    //         callback.Dispose();
    //         gameControl.Player1.Enable();
    //         onComplete?.Invoke();

    //         PlayerPrefs.SetString(GAMEINPU_BINDINGS, gameControl.SaveBindingOverridesAsJson());
    //         PlayerPrefs.Save();
    //     }).Start();
    // }


    // public string GetBindingDisplayString(BindingType bindingType)
    // {
    //     switch (bindingType)
    //     {
    //         case BindingType.Up:
    //             return gameControl.Player1.Move.bindings[1].ToDisplayString();
    //         case BindingType.Down:
    //             return gameControl.Player1.Move.bindings[2].ToDisplayString();
    //         case BindingType.Left:
    //             return gameControl.Player1.Move.bindings[3].ToDisplayString();
    //         case BindingType.Right:
    //             return gameControl.Player1.Move.bindings[4].ToDisplayString();
    //         case BindingType.Interact:
    //             return gameControl.Player1.Interact.bindings[0].ToDisplayString();
    //         case BindingType.Operate:
    //             return gameControl.Player1.Operate.bindings[0].ToDisplayString();
    //         case BindingType.Pause:
    //             return gameControl.Player1.Pause.bindings[0].ToDisplayString();
    //         default:
    //             break;
    //     }
    //     return "";
    // }

    //private void Start()
    //{
    //    print(gameControl.Player.Move.bindings[1].ToDisplayString());
    //    print(gameControl.Player.Move.bindings[2].ToDisplayString());
    //    print(gameControl.Player.Move.bindings[3].ToDisplayString());
    //    print(gameControl.Player.Move.bindings[4].ToDisplayString());
    //    print(gameControl.Player.Interact.bindings[0].ToDisplayString());

    //}

    // private void OnDestroy()
    // {
    //     gameControl.Player1.Interact.performed -= Interact_Performed;
    //     gameControl.Player1.Operate.performed -= Operate_Performed;
    //     gameControl.Player1.Pause.performed -= Pause_Performed;

    //     gameControl.Dispose();
    // }
    protected override void UnsubscribeCallbacks()
    {
        player1Actions.Interact.performed -= Interact_Performed;
        player1Actions.Operate.performed -= Operate_Performed;
        player1Actions.Pause.performed -= Pause_Performed;
    }

    // private void Pause_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    // {
    //     OnPauseAction?.Invoke(this, EventArgs.Empty);
    // }

    // private void Operate_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    // {
    //     OnOperateAction?.Invoke(this, EventArgs.Empty);
    // }

    // private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    // {
    //     OnInteractAction?.Invoke(this, EventArgs.Empty);
    // }

    // public Vector3 GetMovementDirectionNormalized()
    // {
    //     Vector2 inputVector2 = gameControl.Player1.Move.ReadValue<Vector2>();

    //     Vector3 direction = new Vector3(inputVector2.x, 0, inputVector2.y);

    //     direction = direction.normalized;// 1,0,1   0.7,0,0.7

    //     return direction;
    // }
    public override Vector3 GetMovementDirectionNormalized()
    {
        Vector2 inputVector2 = player1Actions.Move.ReadValue<Vector2>();
        Vector3 direction = new Vector3(inputVector2.x, 0, inputVector2.y);
        direction = direction.normalized;
        return direction;
    }

}
