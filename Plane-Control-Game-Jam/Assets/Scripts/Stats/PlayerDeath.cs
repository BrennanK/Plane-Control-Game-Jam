using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour, IOnStatChange
{
    [SerializeField] private float _reloadDelayAfterDie;
    [SerializeField] private GameObject _deathParticles;
    [SerializeField] private GameObject _fadeToBlack;
    [SerializeField] private int _deathParticleInstancesCount;
    [SerializeField] private float _deathParticlesSpawnRadiusAroundPlayer;
    [SerializeField] private float _deathParticlesMinVerticalOffset;
    [SerializeField] private float _deathParticlesMaxVerticalOffset;
    [SerializeField] private bool _randomizeParticleRotation;

    public static bool Dead { get; private set; }

    private float _deathTime;

    private void Awake()
    {
        PlayerStats.Instance.GetStat("health").AddInform(this);
    }

    private void LateUpdate()
    {
        if (!Dead)
            return;

        if (Time.unscaledTime > _deathTime + _reloadDelayAfterDie)
        {
            Time.timeScale = 1;
            Dead = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            // could do some stuff with a loading scene and async scene load
        }
    }

    public void OnStatChange(OneStat stat, int newValue)
    {
        if (newValue <= 0 && !Dead)
            Die();
    }

    private void Die()
    {
        Dead = true;
        _deathTime = Time.unscaledTime;
        Time.timeScale = 0;

        for (int i = 0; i < _deathParticleInstancesCount; i++)
        {
            // spawn the particle in a random position in a cylinder around the player.
            Vector2 offsetInPlane = _deathParticlesSpawnRadiusAroundPlayer * Random.insideUnitCircle;
            float verticalOffset = Random.Range(_deathParticlesMinVerticalOffset, _deathParticlesMaxVerticalOffset);
            Vector3 offset3D = new Vector3(offsetInPlane.x, verticalOffset, offsetInPlane.y);

            Quaternion rotation = Quaternion.identity;
            if (_randomizeParticleRotation)
                rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);

            Instantiate(_deathParticles, transform.position + offset3D, rotation);

        }

        Instantiate(_fadeToBlack);
    }
}
