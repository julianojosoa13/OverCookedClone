using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;

    [SerializeField] private float turnRate = 7f;

    [SerializeField] private GameInput gameInput;

    private bool isWalking;
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVector();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        transform.LookAt(transform.position + Vector3.Slerp(transform.forward.normalized, moveDir, Time.deltaTime * turnRate));

        transform.position += moveDir * Time.deltaTime * moveSpeed;

        isWalking = moveDir != Vector3.zero;
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
