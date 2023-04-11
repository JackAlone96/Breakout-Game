using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    public int hit;
    public int points;
    private int particleAliveTimer = 2;
    private UIManager uiManagerScript;
    private AudioController audioController;
    private MoveBall moveBallScript;
    private PowerupManager powerupManager;
    public AudioClip breakClip;
    public GameObject breakParticleEffect;

    private void Start()
    {
        uiManagerScript = GameObject.Find("UIManager").GetComponent<UIManager>();
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
        moveBallScript = GameObject.Find("Ball").GetComponent<MoveBall>();
        powerupManager = GameObject.Find("PowerupManager").GetComponent<PowerupManager>();
    }

    
    // Reduce the hit points of a block and destroy it
    public void HitBlock()
    {
        hit--;
        if (hit == 0)
        {
            audioController.PlayClip(breakClip);            
            SpawnBreakEffect();
            uiManagerScript.UpdateScore(moveBallScript.combo * points);
            Destroy(gameObject);
            powerupManager.SpawnPowerup(transform);
        }
    }

    // Method to spawn and then destroy the particle effect after some time
    public void SpawnBreakEffect()
    {
        GameObject breakEffect = Instantiate(breakParticleEffect, transform.position, Quaternion.identity);
        Destroy(breakEffect, particleAliveTimer);
    }
}
