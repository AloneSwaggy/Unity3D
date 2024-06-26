using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI Instance { get; private set; }

    [SerializeField] private GameObject uiParent;
    [SerializeField] private Button soundButton;
    [SerializeField] private TextMeshProUGUI soundButtonText;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicButtonText;
    [SerializeField] private Button closeButton;

    [SerializeField] private Button upKeyButton;
    [SerializeField] private Button downKeyButton;
    [SerializeField] private Button leftKeyButton;
    [SerializeField] private Button rightKeyButton;
    [SerializeField] private Button interactKeyButton;
    [SerializeField] private Button operateKeyButton;
    [SerializeField] private Button pauseKeyButton;

    [SerializeField] private TextMeshProUGUI upKeyButtonText;
    [SerializeField] private TextMeshProUGUI downKeyButtonText;
    [SerializeField] private TextMeshProUGUI leftKeyButtonText;
    [SerializeField] private TextMeshProUGUI rightKeyButtonText;
    [SerializeField] private TextMeshProUGUI interactKeyButtonText;
    [SerializeField] private TextMeshProUGUI operateKeyButtonText;
    [SerializeField] private TextMeshProUGUI pauseKeyButtonText;

    [SerializeField] private GameObject rebindingHint;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Hide();
        UpdateVisual();
        soundButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });

        upKeyButton.onClick.AddListener(() => { ReBinding(GameInput1.BindingType.Up); });
        downKeyButton.onClick.AddListener(() => { ReBinding(GameInput1.BindingType.Down); });
        leftKeyButton.onClick.AddListener(() => { ReBinding(GameInput1.BindingType.Left); });
        rightKeyButton.onClick.AddListener(() => { ReBinding(GameInput1.BindingType.Right); });
        interactKeyButton.onClick.AddListener(() => { ReBinding(GameInput1.BindingType.Interact); });
        operateKeyButton.onClick.AddListener(() => { ReBinding(GameInput1.BindingType.Operate); });
        pauseKeyButton.onClick.AddListener(() => { ReBinding(GameInput1.BindingType.Pause); });
    }


    public void Show()
    {
        uiParent.SetActive(true);
    }
    private void Hide()
    {
        uiParent.SetActive(false);
    }

    void UpdateVisual()
    {
        soundButtonText.text = "音效大小：" + SoundManager.Instance.GetVolume();
        musicButtonText.text = "音乐大小：" + MusicManager.Instance.GetVolume();

        upKeyButtonText.text = GameInput1.Instance.GetBindingDisplayString(GameInput1.BindingType.Up);
        downKeyButtonText.text = GameInput1.Instance.GetBindingDisplayString(GameInput1.BindingType.Down);
        leftKeyButtonText.text = GameInput1.Instance.GetBindingDisplayString(GameInput1.BindingType.Left);
        rightKeyButtonText.text = GameInput1.Instance.GetBindingDisplayString(GameInput1.BindingType.Right);
        interactKeyButtonText.text = GameInput1.Instance.GetBindingDisplayString(GameInput1.BindingType.Interact);
        operateKeyButtonText.text = GameInput1.Instance.GetBindingDisplayString(GameInput1.BindingType.Operate);
        pauseKeyButtonText.text = GameInput1.Instance.GetBindingDisplayString(GameInput1.BindingType.Pause);
    }

    private void ReBinding(GameInput1.BindingType bindingType)
    {
        rebindingHint.SetActive(true);
        GameInput1.Instance.ReBinding(bindingType, () =>
        {
            rebindingHint.SetActive(false);
            UpdateVisual();
        });
    }
}
