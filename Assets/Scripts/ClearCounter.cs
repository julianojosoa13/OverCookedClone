using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter otherClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;

    void Update(){
        if(Input.GetKeyDown(KeyCode.T) && testing) {
            if(kitchenObject != null) {
                kitchenObject.SetClearCounter(otherClearCounter);
            }
        }
    }

    public void Interact() {
        if(kitchenObject == null) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        } else {
            Debug.Log(kitchenObject.GetClearCounter());
        }
    }

    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        this.kitchenObject = null
    }

    public bool HasKitchenObject() {
        return kitchenOject != null;
    }
}
