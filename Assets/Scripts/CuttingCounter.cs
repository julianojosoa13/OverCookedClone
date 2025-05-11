using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
      [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

      public override void Interact(Player player) {
        if(!HasKitchenObject()) {
            if(player.HasKitchenObject()) {
                KitchenObject kitchenObject = player.GetKitchenObject();
                if(HasRecipeWithInput(kitchenObject.GetKitchenObjectSO())) {
                    kitchenObject.SetKitchenObjectParent(this);
                }
            }
        } else {
            if(!player.HasKitchenObject()) {
                KitchenObject kitchenObject = GetKitchenObject();
                kitchenObject.SetKitchenObjectParent(player);
            }
        }
    } 

    public override void InteractAlternate(Player player) {
        if(HasKitchenObject()) {
            KitchenObject currenKitchenObject = GetKitchenObject();
            KitchenObjectSO cutKitchenObjectSO = GetOutputForInput(currenKitchenObject.GetKitchenObjectSO());

            if(cutKitchenObjectSO != null) {
                currenKitchenObject.DestroySelf();
                KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this); 
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
         foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if(cuttingRecipeSO.input == inputKitchenObjectSO) {
                return true;
            }
        }
        return false;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if(cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }  
}
