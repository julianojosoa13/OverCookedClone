using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake() {
       CuttingCounter.ResetStaticData();
       BaseCounter.ResetStaticData();
       TrashCounter.ResetStaticData();
    }
}
