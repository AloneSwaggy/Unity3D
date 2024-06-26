using System;
using UnityEngine;

public class Order
{
    public RecipeSO Recipe { get; private set; }
    public float RemainingTime { get; set; }

    public float TimeLimit { get; set; } = 40f;

    public bool IsSucceeded { get; set; } = false;

    public bool IsFailed { get; set; } = false;

    public Animator animator { get; set; }


    public Order(RecipeSO recipe, float remainingTime)
    {
        Recipe = recipe;
        RemainingTime = remainingTime;
    }
}
