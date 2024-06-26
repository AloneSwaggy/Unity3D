using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    [SerializeField] private TextMeshProUGUI upKeyText;
    [SerializeField] private TextMeshProUGUI downKeyText;
    [SerializeField] private TextMeshProUGUI leftKeyText;
    [SerializeField] private TextMeshProUGUI rightKeyText;
    [SerializeField] private TextMeshProUGUI interactKeyText;
    [SerializeField] private TextMeshProUGUI operateKeyText;
    [SerializeField] private TextMeshProUGUI pauseKeyText;
    // [SerializeField] private GameInput1 gameInput1;


    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Show();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsWaitingToStartState())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        UpdateVisual();
        uiParent.SetActive(true);
    }
    private void Hide()
    {
        uiParent.SetActive(false);
    }
    private void UpdateVisual()
    {
        upKeyText.text = GameInput1.Instance.GetBindingDisplayString(GameInput1.BindingType.Up);
        downKeyText.text = GameInput1.Instance.GetBindingDisplayString(GameInput1.BindingType.Down);
        leftKeyText.text = GameInput1.Instance.GetBindingDisplayString(GameInput1.BindingType.Left);
        rightKeyText.text = GameInput1.Instance.GetBindingDisplayString(GameInput1.BindingType.Right);
        interactKeyText.text = GameInput1.Instance.GetBindingDisplayString(GameInput1.BindingType.Interact);
        operateKeyText.text = GameInput1.Instance.GetBindingDisplayString(GameInput1.BindingType.Operate);
        pauseKeyText.text = GameInput1.Instance.GetBindingDisplayString(GameInput1.BindingType.Pause);
        // upKeyText.text = gameInput1.GetBindingDisplayString(GameInput1.BindingType.Up);
        // downKeyText.text = gameInput1.GetBindingDisplayString(GameInput1.BindingType.Down);
        // leftKeyText.text = gameInput1.GetBindingDisplayString(GameInput1.BindingType.Left);
        // rightKeyText.text = gameInput1.GetBindingDisplayString(GameInput1.BindingType.Right);
        // interactKeyText.text = gameInput1.GetBindingDisplayString(GameInput1.BindingType.Interact);
        // operateKeyText.text = gameInput1.GetBindingDisplayString(GameInput1.BindingType.Operate);
        // pauseKeyText.text = gameInput1.GetBindingDisplayString(GameInput1.BindingType.Pause);
    }
}
