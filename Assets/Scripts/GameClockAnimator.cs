using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClockAnimator : MonoBehaviour
{
    private const string IS_SPEEDING = "IsSpeeding";

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetGamePlayingTimer() <= 85)
        {
            anim.SetBool(IS_SPEEDING, true);
        }
    }
}
