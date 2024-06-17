using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Projectile : MonoBehaviour {
    [HideInInspector]
    public GameSettings gameSettings;

    private GameObject target;
    private AudioSource hitSound;
    private float damageToDeal = 5f;
    private bool criticalStrike = false;
    private float speedOfProjectile = 10f;

    private Turret shootingTurret;

    void Start() {
        if (gameSettings == null) gameSettings = FindObjectOfType<GameSettings>();
    }

    public void Seek(GameObject target, float damage, bool isCriticalStrike, AudioSource hitSnd, Turret shooter) {
        this.target = target;
        damageToDeal = damage;
        hitSound = hitSnd;
        criticalStrike = isCriticalStrike;
        shootingTurret = shooter; 
    }

    void PlayHitSound() {
        if (hitSound != null) hitSound.Play();
    }

    void Update() {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.transform.position - transform.position;
        float distanceThisFrame = speedOfProjectile * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame) {
            PlayHitSound();
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget() {
        Enemy enemySettings = target.GetComponent<Enemy>();
        if (enemySettings != null) enemySettings.TakeDamage(damageToDeal, criticalStrike);
        Destroy(gameObject); // Destroy the projectile
    }

    private void AdjustRotation() {
        if (target != null) {
            Vector3 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, (angle * 2.0f)));
        }
    }
}