using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour, IOnStatChange
{
    private bool _dead;

    private void Awake()
    {
        PlayerStats.Instance.GetStat("health").AddInform(this);
    }

    public void OnStatChange(OneStat stat, int newValue)
    {
        if (newValue <= 0 && !_dead)
        {
            _dead = true;

            // to do: player death animation etc.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
