using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; private set;}

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnythingPlacedHere += BaseCounter_OnAnythingPlacedHere;
        PlateKitchenObject.OnAnyIngredientAdded += PlateKitchenObject_OnAnyIngredientAdded;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    public void PlayFootStepsSound(Vector3 position, float volume) {
        PlaySound(audioClipRefsSO.footStep, position, volume);
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e) {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void PlateKitchenObject_OnAnyIngredientAdded(object sender, EventArgs e) {
        PlateKitchenObject plateKitchenObject = sender as PlateKitchenObject;
        PlaySound(audioClipRefsSO.objectPickUp, plateKitchenObject.transform.position);
    }
    private void Player_OnPickedSomething(object sender, EventArgs e) {
        Transform playerKitchenObjectFollowTransform = Player.Instance.GetKitchenObjectFollowTransform();
        PlaySound(audioClipRefsSO.objectPickUp, playerKitchenObjectFollowTransform.position);
    }

     private void BaseCounter_OnAnythingPlacedHere(object sender, EventArgs e) {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.GetKitchenObjectFollowTransform().position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e) {
        CuttingCounter cuttingCounter = (CuttingCounter) sender;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e) {
        DeliveryCounter delilverCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, delilverCounter.transform.position);
    }

     private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e) {
        DeliveryCounter delilverCounter = DeliveryCounter.Instance;

        PlaySound(audioClipRefsSO.deliveryFail, delilverCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
