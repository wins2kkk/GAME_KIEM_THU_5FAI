using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AK : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private Text bulletCountText; // UI Text element to display the bullet count

    readonly int FIRE_HASH = Animator.StringToHash("Ban");

    private Animator myAnimator;
    private int currentBullets = 40;
    private int maxBullets = 30;
    private int refillAmount = 10;
    private float refillInterval = 30f;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {

        UpdateBulletText();
        StartCoroutine(RefillBullets());
    }

    public void Attack()
    {
        if (currentBullets > 0)
        {
            myAnimator.SetTrigger(FIRE_HASH);
            GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
            newArrow.GetComponent<Projectile>().UpdateProjectileRange(weaponInfo.weaponRange);
            currentBullets--;
            UpdateBulletText();
            AudioManager.instance.PlaySFX("AK47");

        }
        else
        {
            Debug.Log("Out of bullets!");
        }
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            // Rotate left and set y scale to positive
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            ActiveWeapon.Instance.transform.localScale = new Vector3(
                ActiveWeapon.Instance.transform.localScale.x,
                Mathf.Abs(ActiveWeapon.Instance.transform.localScale.x), // Ensure y scale is positive
                ActiveWeapon.Instance.transform.localScale.z
            );
        }
        else
        {
            // Rotate right and set y scale to negative
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, 0);
            ActiveWeapon.Instance.transform.localScale = new Vector3(
                ActiveWeapon.Instance.transform.localScale.x,
                -Mathf.Abs(ActiveWeapon.Instance.transform.localScale.x), // Ensure y scale is negative
                ActiveWeapon.Instance.transform.localScale.z
            );
        }
    }


    private void UpdateBulletText()
    {
        if (bulletCountText != null)
        {
            bulletCountText.text = "Bullets: " + currentBullets.ToString();
        }
    }

    private IEnumerator RefillBullets()
    {
        while (true)
        {
            yield return new WaitForSeconds(refillInterval);
            currentBullets = Mathf.Min(currentBullets + refillAmount, maxBullets);
            UpdateBulletText();
        }
    }
}