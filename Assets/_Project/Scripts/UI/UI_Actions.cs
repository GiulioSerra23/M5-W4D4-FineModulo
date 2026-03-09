
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Actions : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private bool _isActive = false;
    [SerializeField] private KeyCode _key;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetActiveCanvasWithInput()
    {
        if (Input.GetKeyDown(_key))
        {
            if (_isActive)
            {
                OpenUI();
            }
            else
            {
                CloseUI();
            }

            _canvas.gameObject.SetActive(_isActive);
        }           
    }

    public void OpenUI()
    {
        Time.timeScale = 0f;
    }

    public void CloseUI()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        SetActiveCanvasWithInput();
    }
}
