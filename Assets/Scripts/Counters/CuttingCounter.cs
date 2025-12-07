using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Multiplayer.Tools.NetStatsMonitor;
using Unity.Netcode;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;
    public static event EventHandler OnAnyCut;


    public static new void ResetStaticData()
    {
        OnAnyCut = null;
    }


    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                KitchenObject kitchenObject = player.GetKitchenObject();
                if (HasRecipeWithInput(kitchenObject.GetKitchenObjectSO()))
                {
                    kitchenObject.SetKitchenObjectParent(this);
                    ResetCutCounterProgressServerRpc();
                }
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                KitchenObject kitchenObject = GetKitchenObject();
                kitchenObject.SetKitchenObjectParent(player);

                ResetCutCounterProgressServerRpc();
            }
            else
            {
                if (player.GetKitchenObject().TryGetPlateKitchenObject(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        KitchenObject.DestroyKitchenObject(GetKitchenObject());
                    }
                }
                else
                {
                    if (GetKitchenObject().TryGetPlateKitchenObject(out PlateKitchenObject plate))
                    {
                        plate.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO());
                    }
                }
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void ResetCutCounterProgressServerRpc()
    {
        ResetCutCounterProgressClientRpc();
    }

    [ClientRpc]
    private void ResetCutCounterProgressClientRpc()
    {
        cuttingProgress = 0;
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = cuttingProgress
        });
    }



    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            CutObjectServerRpc();
            TestCuttingProgressDoneServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void CutObjectServerRpc()
    {
        CutObjectClientRpc();
    }

    [ClientRpc]
    private void CutObjectClientRpc()
    {
        KitchenObject currentKitchenObject = GetKitchenObject();
        KitchenObjectSO cutKitchenObjectSO = GetOutputForInput(currentKitchenObject.GetKitchenObjectSO());

        if (cutKitchenObjectSO != null)
        {
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeSO recipe = GetCuttingRecipeWithInput(currentKitchenObject.GetKitchenObjectSO());

            // private float progress = (float) cuttingProgress / recipe.cuttingProgressMax;

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / recipe.cuttingProgressMax
            });

            TestCuttingProgressDoneServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void TestCuttingProgressDoneServerRpc()
    {

        CuttingRecipeSO recipe = GetCuttingRecipeWithInput(GetKitchenObject().GetKitchenObjectSO());

        if (recipe == null) return;

        if (cuttingProgress == recipe.cuttingProgressMax)
        {
            KitchenObject.DestroyKitchenObject(GetKitchenObject());

            KitchenObject.SpawnKitchenObject(recipe.output, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        return null;
    }

    private CuttingRecipeSO GetCuttingRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
