using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiitchenObject : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] private KitchenObjectSO kitchenObjectSO;

   public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
   }
}
