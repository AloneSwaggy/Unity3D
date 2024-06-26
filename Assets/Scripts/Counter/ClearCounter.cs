using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {//手上有KitchenObject

            if(player.GetKitchenObject()
                .TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject))
            {//手上有盘子
                if (IsHaveKitchenObject() == false)
                {//当前柜台 为空
                    TransferKitchenObject(player, this);
                }
                else
                {//当前柜台 不为空
                    bool isSuccess = plateKitchenObject.AddKitchenObjectSO( GetKitchenObjectSO());
                    if (isSuccess)
                    {
                        DestroyKitchenObject();
                    }
                }
            }
            else
            {//手上是普通食材
                if (IsHaveKitchenObject() == false)
                {//当前柜台 为空
                    TransferKitchenObject(player, this);
                }
                else
                {//当前柜台 不为空
                    if(GetKitchenObject().TryGetComponent<PlateKitchenObject>(out plateKitchenObject))
                    {

                        if(plateKitchenObject.AddKitchenObjectSO( player.GetKitchenObjectSO()))
                        {
                            player.DestroyKitchenObject();
                        }

                    }

                }
            }

            //if (IsHaveKitchenObject() == false)
            //{//当前柜台 为空
            //    TransferKitchenObject(player, this);
            //}
            //else
            //{//当前柜台 不为空

            //}
        }
        else
        {//手上没食材
            if (IsHaveKitchenObject() == false)
            {//当前柜台 为空
                
            }
            else
            {//当前柜台 不为空
                TransferKitchenObject(this, player);
            }
        }
    }

}
