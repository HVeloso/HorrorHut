using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ObjectOfPhysicalInteractionWithThePlayer))]
public class HoleBehaviour : MonoBehaviour
{
    private ObjectOfPhysicalInteractionWithThePlayer interactionLogicScript;

    [Header("Player Animation Parameters")]
    [SerializeField] private string animationTriggerName;

    private Animator playerAnimator;

    private void Awake()
    {
        interactionLogicScript.OnInteracted += MovePlayerDuringAnimation;

        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    private void MovePlayerDuringAnimation()
    {
        playerAnimator.SetTrigger(animationTriggerName);
    }
}
