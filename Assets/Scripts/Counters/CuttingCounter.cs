using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
      public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
      public event EventHandler OnCut;

      public class OnProgressChangedEventArgs: EventArgs {
        public float progressNormalized;
      };

      [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

      private int cuttingProgress;

      public override void Interact(Player player) {
        if(!HasKitchenObject()) {
            if(player.HasKitchenObject()) {
                KitchenObject kitchenObject = player.GetKitchenObject();
                if(HasRecipeWithInput(kitchenObject.GetKitchenObjectSO())) {
                    kitchenObject.SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                        progressNormalized = cuttingProgress
                    });
                }
            }
        } else {
            if(!player.HasKitchenObject()) {
                KitchenObject kitchenObject = GetKitchenObject();
                kitchenObject.SetKitchenObjectParent(player);
                cuttingProgress = 0;
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                    progressNormalized = cuttingProgress
                });
            }
        }
    } 

    public override void InteractAlternate(Player player) {
        if(HasKitchenObject()) {
            KitchenObject currentKitchenObject = GetKitchenObject();
            KitchenObjectSO cutKitchenObjectSO = GetOutputForInput(currentKitchenObject.GetKitchenObjectSO());

            if(cutKitchenObjectSO != null) {
                cuttingProgress++;

                OnCut?.Invoke(this, EventArgs.Empty);

                CuttingRecipeSO recipe = GetCuttingRecipeWithInput(currentKitchenObject.GetKitchenObjectSO());

                // private float progress = (float) cuttingProgress / recipe.cuttingProgressMax;

                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                    progressNormalized = (float) cuttingProgress / recipe.cuttingProgressMax
                });

                if(cuttingProgress == recipe.cuttingProgressMax) {
                    currentKitchenObject.DestroySelf();
                    KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this); 
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
         CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(inputKitchenObjectSO);
         return cuttingRecipeSO != null;
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(inputKitchenObjectSO);
        if(cuttingRecipeSO != null) {
            return cuttingRecipeSO.output;
        }
        return null;
    } 

    private CuttingRecipeSO GetCuttingRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if(cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO;
            }
        }
        return null;
    } 
}
