using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class KitchenObject : NetworkBehaviour
{
     // Start is called before the first frame update
     [SerializeField] private KitchenObjectSO kitchenObjectSO;

     private IKitchenObjectParent kitchenObjectParent;
     private FollowTransform followTransform;

     protected virtual void Awake()
     {
          followTransform = GetComponent<FollowTransform>();
     }

     public KitchenObjectSO GetKitchenObjectSO()
     {
          return kitchenObjectSO;
     }

     public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
     {
          SetKitchenObjectParentServerRpc(kitchenObjectParent.GetNetworkObject());
     }

     [ServerRpc(RequireOwnership = false)]
     private void SetKitchenObjectParentServerRpc(NetworkObjectReference kitchenObjectParentNetworkObjectReference)
     {
          SetKitchenObjectParentClientRpc(kitchenObjectParentNetworkObjectReference);
     }

     [ClientRpc]
     private void SetKitchenObjectParentClientRpc(NetworkObjectReference kitchenObjectParentNetworkObjectReference)
     {
          kitchenObjectParentNetworkObjectReference.TryGet(out NetworkObject kitchenObjectNetworkObject);
          IKitchenObjectParent kitchenObjectParent = kitchenObjectNetworkObject.GetComponent<IKitchenObjectParent>();

          if (this.kitchenObjectParent != null)
          {
               this.kitchenObjectParent.ClearKitchenObject();
          }

          this.kitchenObjectParent = kitchenObjectParent;

          if (kitchenObjectParent.HasKitchenObject())
          {
               Debug.LogError("IKitchenObjectParent has already a kitchen object!");
          }

          kitchenObjectParent.SetKitchenObject(this);
          followTransform.SetTargetTransform(kitchenObjectParent.GetKitchenObjectFollowTransform());
     }

     public bool TryGetPlateKitchenObject(out PlateKitchenObject plateKitchenObject)
     {
          if (this is PlateKitchenObject)
          {
               plateKitchenObject = this as PlateKitchenObject;
               return true;
          }
          else
          {
               plateKitchenObject = null;
               return false;
          }
     }

     public IKitchenObjectParent GetKitchenObjectParent()
     {
          return this.kitchenObjectParent;
     }

     public void DestroySelf()
     {

          Destroy(gameObject);
     }

     public void ClearKitchenObjectOnParent()
     {
          kitchenObjectParent.ClearKitchenObject();
     }


     public static void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
     {
          KitchenGameMultiplayer.Instance.SpawnKitchenObject(kitchenObjectSO, kitchenObjectParent);
     }

     public static void DestroyKitchenObject(KitchenObject kitchenObject)
     {
          KitchenGameMultiplayer.Instance.DestroyKitchenObject(kitchenObject);
     }

}
