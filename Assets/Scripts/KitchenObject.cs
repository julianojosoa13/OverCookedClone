using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO() {
         return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter) {
          if(this.clearCounter != null) {
               this.clearCounter.ClearKitchenObject();
          }

          this.clearCounter = clearCounter;
          
          if(this.clearCounter.HasKitchenObject()) {
               Debug.LogError("Counter has already a kitchen object!");
          }

          this.clearCounter.SetKitchenObject(this);
          
          transform.parent = clearCounter.GetKitchenObjectFollowTransform();
          transform.localPosition = Vector3.zero; 
    }

     public ClearCounter GetClearCounter() {
          return this.clearCounter;
     }

}
