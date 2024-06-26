using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrderResultUi : MonoBehaviour
{

    public static OrderResultUi Instance { get; private set; }
    private Animator orderAnimation;

    private Order order;

    private void Start()
    {

        orderAnimation = GetComponent<Animator>();
        // OrderManager.Instance.OnRecipeSuccessed += OrderManager_OnRecipeSuccessed;
        // OrderManager.Instance.OnOrderFailed += OrderManager_OnOrderFailed;
    }

    private void OnDestroy()
    {
        OrderManager.Instance.OnRecipeSuccessed -= OrderManager_OnRecipeSuccessed;
        OrderManager.Instance.OnOrderFailed -= OrderManager_OnOrderFailed;
    }

    private void OrderManager_OnOrderFailed(object sender, System.EventArgs e)
    {
        // if (e.Order == order && orderAnimation != null)
        {
            orderAnimation.SetTrigger("Fail");
            // StartCoroutine(DestroyAfterAnimation());
        }
    }

    public void OnOrderFailed()
    {
        orderAnimation.SetTrigger("Fail");
    }

    private void OrderManager_OnRecipeSuccessed(object sender, System.EventArgs e)
    {
        // if (e.Order == order && orderAnimation != null)
        // {
        orderAnimation.SetTrigger("Success");
        // StartCoroutine(DestroyAfterAnimation());
        // }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // 获取动画的时长
        float animationLength = orderAnimation.GetCurrentAnimatorStateInfo(0).length;
        // 等待动画播放完成
        yield return new WaitForSeconds(animationLength);
        // 销毁当前订单UI
        Destroy(gameObject);
    }

    public void SetOrder(Order order)
    {
        this.order = order;
    }
}
