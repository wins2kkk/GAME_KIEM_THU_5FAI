using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public int damageAmount = 10;
    public Animator spikeAnimator;
    public float delay = 3.0f;
    private bool istrap = false;

    void Start()
    {
        if (spikeAnimator == null)
        {
            spikeAnimator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Gọi hàm để làm tổn thương người chơi
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                StartCoroutine(ActivateSpikeWithDelay(playerHealth));
                istrap = true;
                //playerHealth.TakeDamage(damageAmount);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                istrap = false;
            }

        }
    }
    public void takedamege()
    {
        if (istrap) 
        {
            PlayerHealth.istance.TakeDamage(damageAmount);
        }
    }
    private IEnumerator ActivateSpikeWithDelay(PlayerHealth playerHealth)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            // Kích hoạt hoạt hình của bẫy
            spikeAnimator.SetTrigger("ActivateSpike");
            // Làm tổn thương người chơi
        }
    }
}
