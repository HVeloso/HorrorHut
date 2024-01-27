using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "interactableObjectImagesHandler", menuName = "SO/New Interactable Object Images Handler")]
public class InteractableObjectImagesHandler : ScriptableObject
{
    [SerializeField] private Sprite detectedObject;
    [SerializeField] private Sprite closeToThePlayer;
}
