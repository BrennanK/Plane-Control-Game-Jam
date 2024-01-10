using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private PlayerWeaponSettings[] _initialWeapons;
    [SerializeField] private bool _onlyFireOnce; // for testing purposes
    [SerializeField] private float _startTime; // for testing purposes

    private List<PlayerWeapon> _weapons = new();

    private void Awake()
    {
        for (int i = 0; i < _initialWeapons.Length; i++)
            AddWeapon(_initialWeapons[i]);
    }

    private void Update()
    {
        if (Time.time < _startTime)
            return;
        for (int i = 0; i < _weapons.Count; i++)
            _weapons[i].Tick(_onlyFireOnce);
    }

    public void AddWeapon(PlayerWeaponSettings weaponSettings)
    {
        _weapons.Add(new PlayerWeapon(_playerRigidbody, weaponSettings));
    }
}
