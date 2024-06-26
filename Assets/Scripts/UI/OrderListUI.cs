using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderListUI : MonoBehaviour
{

    [SerializeField] private Transform recipeParent;
    [SerializeField] private RecipeUI recipeUITemplate;

    private Dictionary<Order, RecipeUI> recipeUIDictionary = new Dictionary<Order, RecipeUI>();

    private void Start()
    {
        recipeUITemplate.gameObject.SetActive(false);
        OrderManager.Instance.OnRecipeSpawned += OrderManager_OnRecipeSpawned;
        OrderManager.Instance.OnRecipeSuccessed += OrderManager_OnRecipeSuccessed;
        // OrderMananger.Instance.OnRecipeFailed += OrderManager_OnRecipeFailed;
        OrderManager.Instance.OnOrderFailed += OrderManager_OnOrderFailed;
    }

    private void OrderManager_OnRecipeSuccessed(object sender, System.EventArgs e)
    {
        UpdateUI();
    }

    private void OrderManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateUI();
    }

    // private void OrderManager_OnRecipeFailed(object sender, System.EventArgs e)
    // {
    //     UpdateUI();
    // }

    private void OrderManager_OnOrderFailed(object sender, OrderEventArgs e)
    {
        foreach (Transform child in transform)
        {
            RecipeUI recipeUI = GetRecipeUI(e.Order);
            if (recipeUI != null)
            {
                recipeUI.PlayFailAnimation();
            }
        }
        // 延迟更新UI，确保动画播放完毕
        StartCoroutine(DelayUpdateUI());
        Debug.Log("OrderManager_OnOrderFailed");
        // UpdateUI();
    }

    private IEnumerator DelayUpdateUI()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("DelayUpdateUI");
        UpdateUI();
    }

    // private void UpdateFailUi(object sender, System.EventArgs e)
    // {
    //     foreach (Transform child in transform)
    //     {
    //         RecipeUI recipeUI = child.GetComponent<RecipeUI>();
    //         if (recipeUI != null && recipeUI.Order == e.Order)
    //         {
    //             recipeUI.PlayFailAnimation();
    //         }
    //     }
    // }




    private void UpdateUI()
    {

        foreach (Transform child in recipeParent)
        {
            //
            if (child != recipeUITemplate.transform)
            {
                Destroy(child.gameObject);
                // StartCoroutine(DestroyChild(child.gameObject));
            }
        }
        recipeUIDictionary.Clear();  // 清空字典，确保不会保存已销毁的对象引用
        // List<RecipeSO> recipeSOList = OrderMananger.Instance.GetOrderList();
        List<Order> orderList = OrderManager.Instance.GetOrderList();
        // foreach (RecipeSO recipeSO in recipeSOList)
        // {
        //     RecipeUI recipeUI = GameObject.Instantiate(recipeUITemplate);
        //     recipeUI.transform.SetParent(recipeParent);
        //     recipeUI.gameObject.SetActive(true);
        //     recipeUI.UpdateUI(recipeSO);
        // }
        foreach (Order order in orderList)
        {
            RecipeUI recipeUI = GameObject.Instantiate(recipeUITemplate);
            recipeUI.transform.SetParent(recipeParent);
            recipeUI.gameObject.SetActive(true);
            recipeUI.UpdateUI(order);
            recipeUIDictionary[order] = recipeUI;  // 将 RecipeUI 实例与订单关联起来
        }
    }

    public RecipeUI GetRecipeUI(Order order)
    {
        if (recipeUIDictionary.TryGetValue(order, out RecipeUI recipeUI))
        {
            return recipeUI;
        }
        return null;
    }


    //     // List<RecipeSO> recipeSOList = OrderMananger.Instance.GetOrderList();
    //     List<Order> orderList = OrderManager.Instance.GetOrderList();
    //     // foreach (RecipeSO recipeSO in recipeSOList)
    //     // {
    //     //     RecipeUI recipeUI = GameObject.Instantiate(recipeUITemplate);
    //     //     recipeUI.transform.SetParent(recipeParent);
    //     //     recipeUI.gameObject.SetActive(true);
    //     //     recipeUI.UpdateUI(recipeSO);
    //     // }
    //     foreach (Order order in orderList)
    //     {
    //         // RecipeUI recipeUI = GameObject.Instantiate(recipeUITemplate);
    //         // recipeUI.transform.SetParent(recipeParent);
    //         // recipeUI.gameObject.SetActive(true);
    //         order.recipeUI.UpdateCountDown(order);
    //     }
    // }

    private IEnumerator DestroyChild(GameObject child)
    {
        yield return new WaitForSeconds(3f);
        Destroy(child);
    }

}
