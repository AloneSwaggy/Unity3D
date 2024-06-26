using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform kitchenObjectParent;
    [SerializeField] private Image iconUITemplate;
    [SerializeField] private Image progressImage;

    private Animator Animation;


    private Order order { get; set; }

    // private OrderResultUI orderResultUI;
    private void Start()
    {
        iconUITemplate.gameObject.SetActive(false);
        Animation = GetComponent<Animator>();
        // if (orderAnimation == null)
        // {
        //     Debug.LogError("Animator component not found on RecipeUI object.");
        // }
        // OrderManager.Instance.OnOrderTimeEnd += OrderManager_OnOrderTimeEnd;
    }

    // private void OrderManager_OnOrderTimeEnd(object sender, System.EventArgs e)
    // {
    //     updateOrderResultUI();
    // }


    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateUI(Order order)
    {
        this.order = order;
        RecipeSO recipeSO = order.Recipe;
        recipeNameText.text = recipeSO.recipleName;
        Animation = GetComponent<Animator>();
        Debug.Log("Calling UpdateCountdownUI");
        // updateOrderResultUI();
        UpdateCountdownUI();
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Image newIcon = GameObject.Instantiate(iconUITemplate);
            newIcon.transform.SetParent(kitchenObjectParent);
            newIcon.sprite = kitchenObjectSO.sprite;
            newIcon.gameObject.SetActive(true);
        }

    }

    private const string Success = "Success";
    private const string Fail = "Fail";

    private const string IS_SHOW = "IsShow";
    private void updateOrderResultUI()
    {
        Animation = GetComponent<Animator>();
        Debug.Log("Updating order result UI");
        if (order.IsSucceeded)
        {
            if (Animation == null)
            {
                Debug.Log("Animator component not found on RecipeUI object.");
                return;
            }
            if (Animation != null)
            {
                Debug.Log("Setting Success animation");
                Animation.SetBool(Success, true);
            }
        }
        if (order.IsFailed)
        {
            if (Animation == null)
            {
                Debug.Log("Animator component not found on RecipeUI object.");
                return;
            }
            if (Animation != null)
            {
                Debug.Log("Setting Success animation");
                Debug.Log(Animation.GetBool(Fail));
                Animation.gameObject.SetActive(true);
                Animation.SetBool(Fail, true);
                Debug.Log(Animation.GetBool(Fail));
            }
        }
    }

    public void PlayFailAnimation()
    {
        Debug.Log("Playing Fail Animation");
        Animation.SetBool(Fail, true);
    }


    private void Update()
    {
        if (order != null)
        {
            UpdateCountdownUI();
        }
    }
    private void UpdateCountdownUI()
    {
        if (progressImage == null)
        {
            Debug.LogError("progressImage is not assigned!");
            return;
        }
        Debug.Log("Updating countdown UI");
        // float progress = order.RemainingTime / order.TimeLimit;
        // Debug.Log("Progress: " + progress);
        progressImage.fillAmount = order.RemainingTime / order.TimeLimit;
    }
}
