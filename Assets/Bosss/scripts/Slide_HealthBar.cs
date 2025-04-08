using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slide_HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;  // Tham chiếu đến slider của UI
    [SerializeField] private Vector3 offset = new Vector3(0, 2, 0);  // Độ lệch để đặt slider trên đầu quái

    private EnemyHealth enemyHealth;
    private Transform enemyTransform;


    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyTransform = transform;

        // Kiểm tra giá trị khởi tạo
        if (enemyHealth != null)
        {
            healthSlider.maxValue = enemyHealth.GetStartingHealth();
            healthSlider.value = healthSlider.maxValue;
            Debug.Log($"Max Health: {healthSlider.maxValue}"); // Ghi log giá trị max
        }
        else
        {
            healthSlider.maxValue = 1; // Đặt giá trị tối đa mặc định
            healthSlider.value = healthSlider.maxValue;
        }
    }


    private void Update()
    {
        if (healthSlider != null )
        {
            // Cập nhật vị trí slider theo vị trí quái và đối mặt với camera
            healthSlider.transform.position = enemyTransform.position + offset;
            healthSlider.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
    }


    public void UpdateSlider(int currentHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.value = Mathf.Clamp(currentHealth, 0, healthSlider.maxValue);
            Debug.Log($"Slider Value: {healthSlider.value}"); // Ghi log giá trị của slider
        }
    }


    public void DestroySlider()
    {
        if (healthSlider != null)
        {
            Destroy(healthSlider.gameObject);
        }
    }
}
