using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;

    [SerializeField] private Player player;

    private const string IS_WALKING = "isWalking";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bool isPlayerWalking = player.IsWalking();
        animator.SetBool(IS_WALKING, isPlayerWalking);
    }


}
