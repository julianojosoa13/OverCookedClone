using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;

    [SerializeField] private float turnRate = 7f;

    [SerializeField] private GameInput gameInput;

    [SerializeField] private LayerMask countersLayerMask;

    private bool isWalking;
    private Vector3 lastInteractDir;


    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void Update()
    {
        HandleMovement();
        // HandleInteract();
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        HandleInteract();
    }

    private void HandleInteract() {
        Vector2 inputVector = gameInput.GetMovementVector();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float interactDistance = 2f;

        if(moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        if(Physics.Raycast(transform.position, lastInteractDir, out  RaycastHit raycastHit,interactDistance, countersLayerMask)) {
            if(raycastHit.transform.TryGetComponent(out ClearCounter counter )) {
                counter.Interact();
            }
        }
    } 

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVector();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float playerRadius = .7f;
        float playerHeight = 2.0f;

        float moveDistance = Time.deltaTime * moveSpeed;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance );

       
        if(!canMove) {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance );

            if(canMove) {
                moveDir = moveDirX;
            } else {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance );
                if(canMove) {
                    moveDir = moveDirZ;
                } else {
                    // Player can't move in any direction
                }
            }
        }

        if(canMove) transform.position += moveDir * moveDistance;

        transform.LookAt(transform.position + Vector3.Slerp(transform.forward.normalized, moveDir, Time.deltaTime * turnRate));

        isWalking = moveDir != Vector3.zero;
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
