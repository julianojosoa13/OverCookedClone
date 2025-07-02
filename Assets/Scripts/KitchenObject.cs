using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class KitchenObject : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() {
         return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
          if(this.kitchenObjectParent != null) {
               this.kitchenObjectParent.ClearKitchenObject();
          }

          this.kitchenObjectParent = kitchenObjectParent;
          
          if(kitchenObjectParent.HasKitchenObject()) {
               Debug.LogError("IKitchenObjectParent has already a kitchen object!");
          }

          kitchenObjectParent.SetKitchenObject(this);
               
          // transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
          // transform.localPosition = Vector3.zero; 
          
    }

    public bool TryGetPlateKitchenObject(out PlateKitchenObject plateKitchenObject) {
          if(this is PlateKitchenObject) {
               plateKitchenObject = this as PlateKitchenObject;
               return true;
          } else {
               plateKitchenObject = null;
               return false;
          }
    }

     public IKitchenObjectParent GetKitchenObjectParent() {
          return this.kitchenObjectParent;
     }

     public void DestroySelf() {
          kitchenObjectParent.ClearKitchenObject();
          Destroy(gameObject);
     }


     public static void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) {
         KitchenGameMultiplayer.Instance.SpawnKitchenObject( kitchenObjectSO, kitchenObjectParent);
     }

}
