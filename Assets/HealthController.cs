using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HealthController : NetworkBehaviour {


	public bool destroyOnDeath;
	public RectTransform healthBar;

	public const int maxHealth = 100;

	[SyncVar(hook="OnChangeHealth")]
    public int currentHealth = maxHealth;

    private NetworkStartPosition[] spawnPoints;

    void Start ()
    {
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

	public void TakeDamage(int damage){
		if(!isServer){
			return;
		}
		currentHealth -= damage;
		if(currentHealth <= 0){
			if(destroyOnDeath){
				Destroy(gameObject);
			}else{
				currentHealth = maxHealth;
				RpcRespawn();
			}
		}
	}

	void OnChangeHealth(int health){
		healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
	}

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // Set the spawn point to origin as a default value
            Vector3 spawnPoint = Vector3.zero;

            // If there is a spawn point array and the array is not empty, pick one at random
            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            // Set the player’s position to the chosen spawn point
            transform.position = spawnPoint;
        }
    }

}
