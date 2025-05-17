using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
   [SerializeField] private Transform container;
   [SerializeField] private Transform recipeTemplate;

   private void Awake() {
    recipeTemplate.gameObject.SetActive(false);
   }
   private void Start() {
    DeliveryManager.Instance.OnWaitingRecipeListChanged += DeliveryManager_OnWaitingRecipeListChanged;
   }

   private void DeliveryManager_OnWaitingRecipeListChanged(object sender, EventArgs e) {
    UpdateVisual();
   }

   private void UpdateVisual() {
        foreach(Transform child in container) {

            if(child == recipeTemplate) continue;

            Destroy(child.gameObject);
        }

        List<RecipeSO> waitingRecipeSOList = DeliveryManager.Instance.GetWaitingRecipeSOList();

        foreach(RecipeSO recipeSO in waitingRecipeSOList) {
            Transform recipeTransfrom = Instantiate(recipeTemplate, container);
            recipeTransfrom.gameObject.SetActive(true);
            recipeTransfrom.gameObject.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
   }
}
