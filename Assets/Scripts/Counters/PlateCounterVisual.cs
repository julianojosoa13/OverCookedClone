using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform platesVisualPrefab;
    [SerializeField] private Transform counterTopPoint;

    private List<GameObject> platesVisualGameObjetList;

    private void Awake() {
        platesVisualGameObjetList = new List<GameObject>();
    }

    private void Start() {
        plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
    }

    private void PlateCounter_OnPlateSpawned(object sender, EventArgs e) {
        Transform platesVisual = Instantiate(platesVisualPrefab, counterTopPoint);

        float plateOffsetY = 0.15f;
        platesVisual.localPosition = new Vector3(0f, platesVisualGameObjetList.Count * plateOffsetY, 0f);
        platesVisualGameObjetList.Add(platesVisual.gameObject);
    }

    private void PlateCounter_OnPlateRemoved(object sender, EventArgs e) {
        GameObject plateGameObject = platesVisualGameObjetList[platesVisualGameObjetList.Count - 1];
        platesVisualGameObjetList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }
}
