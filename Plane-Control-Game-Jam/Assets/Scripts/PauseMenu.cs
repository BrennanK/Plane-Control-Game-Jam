using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// later in script execution order (just to ensure consistency e.g. if
// the player takes damage during update and dies).

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _toggle;

    private bool _exited;

    public static bool Paused { get; private set; }

    private void Awake()
    {
        _exited = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SetPaused(!Paused);
    }

    private void SetPaused(bool pause)
    {
        if (PlayerDeath.Dead)
            return;
        Paused = pause;
        Time.timeScale = pause ? 0 : 1;
        _toggle.SetActive(pause);
    }

    public void OnResumeButton() => SetPaused(false);

    public void OnExitButton()
    {
        if (_exited) // not sure if this is necessary, like if you spam the exit button
            return;
        if (PlayerDeath.Dead)
            return;

        // to do: save here (or maybe when you open the pause menu)
        
        SceneManager.LoadScene(0);
        SetPaused(false);
        _toggle.SetActive(true); // don't make it disappear for like a frame during the load
    }

    
}
