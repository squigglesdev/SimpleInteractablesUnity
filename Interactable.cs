using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Interactable : MonoBehaviour
{
    // Enum for static/animated default sprite
    public enum SpriteType { Static, Animated }

    // Enum for interactable types: toggle or radius
    public enum InteractableType { Toggle, Radius }

    // Enum for interactable actions: narrative or kinetic
    public enum InteractableAction { Narrative, Kinetic }

    // Interactable type (toggle or radius)
    [Header("Interactable Type")]
    public InteractableType type;

    // Interactable action (narrative or kinetic)
    //[Header("Interactable Action")]
    //public InteractableAction action;

    // References to player and sprite renderer
    [Header("References")]
    //[HideIfEnumValue("action", HideIf.Equal, (int) InteractableAction.Kinetic)]
    //public NarrativeSystem narrativeSystem;
    //[HideIfEnumValue("action", HideIf.Equal, (int) InteractableAction.Narrative)]
    //public int thing;
    public GameObject player;
    public SpriteRenderer renderer;

    // Interactable radius (for radius type)
    [Header("Settings")]
    public float radius;

    // Whether the interactable is activated
    public bool activated = false;

    // Sprites for different states
    [Header("Sprites")]
    public SpriteType unselectedSprite;
    [HideIfEnumValue("unselectedSprite", HideIf.Equal, (int) SpriteType.Animated)]
    public Sprite defaultSprite;
    [HideIfEnumValue("unselectedSprite", HideIf.Equal, (int) SpriteType.Static)]
    public Animator defaultAnimatedSprite;
    public SpriteType selectedSprite;
    [HideIfEnumValue("selectedSprite", HideIf.Equal, (int) SpriteType.Animated)]
    public Sprite selectSprite;
    [HideIfEnumValue("selectedSprite", HideIf.Equal, (int) SpriteType.Static)]
    public Animator selectedAnimatedSprite;
    public SpriteType activatedSprite;
    [HideIfEnumValue("activatedSprite", HideIf.Equal, (int) SpriteType.Animated)]
    public Sprite activateSprite;
    [HideIfEnumValue("activatedSprite", HideIf.Equal, (int) SpriteType.Static)]
    public Animator activatedAnimatedSprite;

    // Whether the player is within the interactable's radius
    private bool inRadius;
    void OnValidate()
    {
        if (defaultAnimatedSprite == null){
            defaultAnimatedSprite = GetComponent<Animator>();
        }
        if(selectedAnimatedSprite == null){
            selectedAnimatedSprite = GetComponent<Animator>();
        }
        if (activatedAnimatedSprite == null){
            activatedAnimatedSprite = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate distance between player and interactable
        float distance = Vector2.Distance(transform.position, player.transform.position);

        // Check if player is within interactable's radius
        if (distance <= radius)
        {
            // Set inRadius to true and update sprite
            inRadius = true;
            if (selectedSprite == SpriteType.Static)
            {
                selectedAnimatedSprite.enabled = false;
                renderer.sprite = selectSprite;

            }
            else if (selectedSprite == SpriteType.Animated){
                selectedAnimatedSprite.enabled = true;
                selectedAnimatedSprite.SetBool("activated", false);
                selectedAnimatedSprite.SetBool("selected", true);
            }
        }
        else if (distance >= radius && type == InteractableType.Radius)
        {
            // Set activated to false if outside radius (for radius type only)
            activated = false;
        }
        else
        {
            // Set inRadius to false and update sprite
            inRadius = false;
            if (unselectedSprite == SpriteType.Static)
            {
                renderer.sprite = defaultSprite;
                defaultAnimatedSprite.enabled = false;
            }
            else if (unselectedSprite == SpriteType.Animated){
                defaultAnimatedSprite.enabled = true;
                defaultAnimatedSprite.SetBool("selected", false);
                defaultAnimatedSprite.SetBool("activated", false); 
            }
        }

        // Update sprite based on activated state
        if (activated)
        {
            if (activatedSprite == SpriteType.Static)
            {
                renderer.sprite = activateSprite;
                activatedAnimatedSprite.enabled =  false;
            }
            else if (activatedSprite == SpriteType.Animated){
                activatedAnimatedSprite.enabled =  true;
                defaultAnimatedSprite.SetBool("activated", true);
                defaultAnimatedSprite.SetBool("selected", false);
            }
        }
        else if (distance >= radius)
        {
            if (unselectedSprite == SpriteType.Static)
            {
                renderer.sprite = defaultSprite;
                defaultAnimatedSprite.enabled = false;
            }
            else if (unselectedSprite == SpriteType.Animated){
                defaultAnimatedSprite.enabled = true;
                defaultAnimatedSprite.SetBool("selected", false);
                defaultAnimatedSprite.SetBool("activated", false); 
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && inRadius){
            activated = !activated;
        }
    }
}
