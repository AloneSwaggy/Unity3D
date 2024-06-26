using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public string recipleName;

    // 增加菜品等待时间
    // public float timeToAdd;

    public List<KitchenObjectSO> kitchenObjectSOList;
}
