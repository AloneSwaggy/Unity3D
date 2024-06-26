using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    [SerializeField] private Player player;

    private ParticleSystem particle;
    // Update is called once per frame
    void FixedUpdate()
    {
        // transform.position = Player.Instance.transform.position;
        transform.position = player.transform.position;
    }

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (GameManager.Instance.GetGamePlayingTimer() <= 85)
        {
            particle.startSize = 0.6f;
            particle.maxParticles = 2000;
        }
    }
}
