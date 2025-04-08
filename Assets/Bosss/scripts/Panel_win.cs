using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Panel_win : MonoBehaviour
{
    public GameObject panel; // Tham chiếu tới panel UI
    private bool hasTriggered = false; // Biến để kiểm tra xem panel đã hiện chưa
    public static bool gameIsPaused = false; // Đổi tên biến cho dễ hiểu hơn
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        panel.SetActive(false); // Đảm bảo panel bắt đầu ở chế độ inactive
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            ShowWinPanel();
        }
    }

    private void ShowWinPanel()
    {
        panel.SetActive(true); // Hiển thị panel
        hasTriggered = true; // Đặt biến thành true để không hiện panel lần nữa
        text.text =PlayerPrefs.GetInt("coin").ToString();
        PauseGame(); // Dừng game
    }

    private void PauseGame()
    {
        Time.timeScale = 0; // Dừng game lại
        gameIsPaused = true;
    }
}
