using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        // Kiểm tra xem cảnh hiện tại có phải là Scene 0 hay không
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            // Nếu đúng, chuyển sang Scene 1 sau 10 giây
            Invoke("LoadScene1", 10f);
        }
    }

    void LoadScene1()
    {
        // Chuyển sang Scene 1
        SceneManager.LoadScene(1);
    }
}
