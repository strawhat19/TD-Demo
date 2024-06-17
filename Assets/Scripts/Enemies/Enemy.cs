using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
    [HideInInspector]
    public GameSettings gameSettings;

    public float speed = 1f;
    public float damage = 10f;
    public float reward = 100f;
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public TextMeshProUGUI healthText;

    void Start() {
        if (gameSettings == null) gameSettings = FindObjectOfType<GameSettings>();
    }

    void Move() {
        MoveTowardsFinishLine();
    }

    void MoveTowardsFinishLine() {
        if (gameSettings.finishLine != null) {
            transform.position = Vector3.MoveTowards(transform.position, gameSettings.finishLine.position, speed * Time.deltaTime);
        }
    }

    void Update() {
        Move();
        UpdateHealthText();
        DieIfHealth0();
    }

    void DieIfHealth0() {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    void UpdateHealthText() {
        if (healthText != null) {
            healthText.text = $"{gameSettings.RemoveDotZero(currentHealth.ToString("F2"))}";
        }
    }

    // void TriggerHitAnimation() {
    //     animator.SetBool("Hit", true);
    //     SetDamageEffect(true);
    //     Invoke("StopHitAnimation", 0.1f);
    // }

    // void StopHitAnimation() {
    //     animator.SetBool("Hit", false);
    // }

    public void TakeDamage(float damage, bool criticalStrike) {
        // TriggerHitAnimation();
        currentHealth -= damage;
        // ShowDamageTextAnimation(damage, criticalStrike);
        currentHealth = Mathf.Clamp(currentHealth, 0, currentHealth);
        // StartCoroutine(SmoothTransitionToNewHealth(currentHealth - damage, true));
    }
}