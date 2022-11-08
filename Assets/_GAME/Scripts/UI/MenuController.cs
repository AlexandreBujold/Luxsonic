using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Button quitButton = null;
    [SerializeField]
    private Button restartButton = null;

    private void OnEnable()
    {
        if (quitButton)
            quitButton.onClick.AddListener(Quit);

        if (restartButton)
            restartButton.onClick.AddListener(Restart);
    }

    private void OnDisable()
    {
        if (quitButton)
            quitButton.onClick.RemoveListener(Quit);

        if (restartButton)
            restartButton.onClick.RemoveListener(Restart);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        //With more time this would be more elegant, but it works
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
