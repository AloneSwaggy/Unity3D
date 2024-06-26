using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeSuccessed;
    public event EventHandler OnRecipeFailed;

    // ����������ʱ�¼�
    // public event EventHandler OnOrderFailed;
    public event EventHandler<OrderEventArgs> OnOrderFailed;

    public event EventHandler OnOrderTimeEnd;




    [SerializeField] private RecipeListSO recipeSOList;
    [SerializeField] private int orderMaxCount = 4;
    [SerializeField] private float orderRate = 10;
    [SerializeField] private float orderTimeLimit = 40f; // ÿ��������ʱ������

    // private List<RecipeSO> orderRecipeSOList = new List<RecipeSO>();
    private List<Order> orderList = new List<Order>();

    private float orderTimer = 0;
    private bool isStartOrder = false;
    private int orderCount = 0;
    private int successDeliveryCount = 0;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGamePlayingState())
        {
            StartSpawnOrder();
        }
    }

    private void Update()
    {
        if (isStartOrder)
        {
            OrderUpdate();
        }
    }

    private void OrderUpdate()
    {
        // ���������߼�
        orderTimer -= Time.deltaTime;
        if (orderTimer <= 0f)
        {
            Debug.Log("OrderTimer <= 0f");
            orderTimer = orderRate;
            OrderANewRecipe();
        }
        UpdateOrderTimes();
    }

    private void OrderANewRecipe()
    {
        // if (orderCount >= orderMaxCount) return;
        if (orderList.Count >= orderMaxCount) return;
        Debug.Log("OrderANewRecipe");

        orderCount++;
        int index = UnityEngine.Random.Range(0, recipeSOList.recipeSOList.Count);
        // orderRecipeSOList.Add(recipeSOList.recipeSOList[index]);
        RecipeSO newRecipe = recipeSOList.recipeSOList[index];
        Order newOrder = new Order(newRecipe, orderTimeLimit);
        orderList.Add(newOrder);
        // ��������ʱ�������¼�
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    // ���¶���ʣ��ʱ��
    private void UpdateOrderTimes()
    {
        for (int i = 0; i < orderList.Count; i++)
        {
            Order order = orderList[i];
            order.RemainingTime -= Time.deltaTime;
            if (order.RemainingTime <= 0)
            {
                // OnOrderFailed?.Invoke(this, EventArgs.Empty);
                // OnOrderFailed?.Invoke(this, new OrderEventArgs(order));
                orderList.RemoveAt(i);
            }
            //   ʣ��ʱ��С�ڵ���2ʱ�����������ȴ���ʱ�¼�
            if (order.RemainingTime <= 2)
            {
                OnOrderFailed?.Invoke(this, new OrderEventArgs(order));
            }
        }

    }




    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        // RecipeSO correctRecipe = null;
        Order correctOrder = null;
        // foreach (RecipeSO recipe in orderRecipeSOList)
        // {
        //     if (IsCorrect(recipe, plateKitchenObject))
        //     {
        //         correctRecipe = recipe; break;
        //     }
        // }
        foreach (Order order in orderList)
        {
            if (IsCorrect(order.Recipe, plateKitchenObject))
            {
                correctOrder = order; break;
            }
        }

        // if (correctRecipe == null)
        if (correctOrder == null)
        {
            print("�ϲ�ʧ��");
            OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            // orderRecipeSOList.Remove(correctRecipe);
            orderList.Remove(correctOrder);
            OnRecipeSuccessed?.Invoke(this, EventArgs.Empty);
            successDeliveryCount++;
            correctOrder.IsSucceeded = true;
            print("�ϲ˳ɹ�");
        }
    }

    private bool IsCorrect(RecipeSO recipe, PlateKitchenObject plateKitchenObject)
    {
        List<KitchenObjectSO> list1 = recipe.kitchenObjectSOList;
        List<KitchenObjectSO> list2 = plateKitchenObject.GetKitchenObjectSOList();

        if (list1.Count != list2.Count) return false;

        foreach (KitchenObjectSO kitchenObjectSO in list1)
        {
            if (list2.Contains(kitchenObjectSO) == false)
            {
                return false;
            }
        }

        return true;
    }

    public List<Order> GetOrderList()
    {
        return orderList;
    }

    public void StartSpawnOrder()
    {
        isStartOrder = true;
    }
    public int GetSuccessDeliveryCount()
    {
        return successDeliveryCount;
    }

}


