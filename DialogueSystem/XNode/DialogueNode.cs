﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueNode : BaseNode 
{
    [Input] public int entry;
    [Output] public int exit;
    public string speakerName;
    public string dialogueLine;
    public Sprite sprite;
    public GameObject cameraEffectPrefab;
    public GameObject displayEffects; 
    public AudioClip soundEffect;
    public AudioClip musicClip;


    public override string GetString()
    {
        Debug.Log("Dialogue Line Length: " + dialogueLine.Length);
        return "DialogueNode/" + speakerName + "/" + dialogueLine;
    }
    public override Sprite GetSprite()
    {
        return sprite;
    }

public void TriggerEffects()
{
    if (cameraEffectPrefab != null)
    {
        Instantiate(cameraEffectPrefab); // Trigger camera effect
    }

    // Access the AudioManager to play music and sound effects
    AudioManager audioManager = FindObjectOfType<AudioManager>();

    if (audioManager != null)
    {
        if (musicClip != null)
        {
            // Only change the music if a different music clip is specified
            if (audioManager.CurrentMusicClip != musicClip)
            {
                audioManager.PlayMusic(musicClip);
                Debug.Log("New music started in dialogue node");
            }
        }

        if (soundEffect != null) // Check if there's a sound effect to play
        {
            audioManager.PlaySoundEffect(soundEffect);
            Debug.Log("Sound effect played in dialogue node");
        }
    }
    else
    {
        Debug.LogWarning("AudioManager not found in the scene.");
    }

    if (displayEffects != null)
    {
        Instantiate(displayEffects); // Trigger display effect
    }
}


}