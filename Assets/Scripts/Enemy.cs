using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    
    [HideInInspector]
    public float speed;
    public float startHealth = 100f;
    private float health;
    public int worth = 50;
    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    void Start(){
        speed = startSpeed;
        health = startHealth;
    }
    public void TakeDamage(float amount){
        health -= amount;

        if(health <= 0 && !isDead){
            Die();
        }
        
        healthBar.fillAmount = health / startHealth;
    }

    public void Slow(float pct){
        speed = startSpeed * (1f - pct);
    }

    void Die(){
        isDead = true;

        PlayerStats.Money += worth;
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(effect, 5f);
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }


}
