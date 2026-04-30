using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions _input;
    private WeaponScript _currentWeapon;

    public GameObject[] Weapons;
    public WeaponScript[] CurrentWeapon;

    void Awake() => _input = new InputSystem_Actions();

    void OnEnable()
    {
        _input.Player.Enable();
        _input.Player.Attack.performed += HandleFire;
        _input.Player.Attack.canceled += HandleFire;
        _input.Player.Interact.performed += HandleAim;
        _input.Player.Interact.canceled += HandleAim;

        _input.Player.Weapon1.performed += ctx => EquipWeaponByIndex(0);
        _input.Player.Weapon2.performed += ctx => EquipWeaponByIndex(1);
        _input.Player.Weapon3.performed += ctx => EquipWeaponByIndex(2);
        _input.Player.Weapon4.performed += ctx => EquipWeaponByIndex(3);
        _input.Player.Weapon5.performed += ctx => EquipWeaponByIndex(4);
        _input.Player.Weapon6.performed += ctx => EquipWeaponByIndex(5);
        _input.Player.Weapon7.performed += ctx => EquipWeaponByIndex(6);
        _input.Player.Weapon8.performed += ctx => EquipWeaponByIndex(7);
        _input.Player.Weapon9.performed += ctx => EquipWeaponByIndex(8);
    }

    void OnDisable()
    {
        _input.Player.Attack.performed -= HandleFire;
        _input.Player.Attack.canceled -= HandleFire;
        _input.Player.Interact.performed -= HandleAim;
        _input.Player.Interact.canceled -= HandleAim;
        

        _input.Player.Weapon1.performed -= ctx => EquipWeaponByIndex(0);
        _input.Player.Weapon2.performed -= ctx => EquipWeaponByIndex(1);
        _input.Player.Weapon3.performed -= ctx => EquipWeaponByIndex(2);
        _input.Player.Weapon4.performed -= ctx => EquipWeaponByIndex(3);
        _input.Player.Weapon5.performed -= ctx => EquipWeaponByIndex(4);
        _input.Player.Weapon6.performed -= ctx => EquipWeaponByIndex(5);
        _input.Player.Weapon7.performed -= ctx => EquipWeaponByIndex(6);
        _input.Player.Weapon8.performed -= ctx => EquipWeaponByIndex(7);
        _input.Player.Weapon9.performed -= ctx => EquipWeaponByIndex(8);
        _input.Player.Disable();
    }

    public void EquipWeapon(WeaponScript weapon)
    {
        _currentWeapon = weapon;
    }

    private void HandleFire(InputAction.CallbackContext ctx)
    {
        _currentWeapon?.FireShot(ctx);
    }

    private void HandleAim(InputAction.CallbackContext ctx)
    {
        _currentWeapon?.AimIn(ctx);
    }


    public void EquipWeaponByIndex(int index)
    {
        _currentWeapon?.StopFireCoroutine();
        if (index < 0 || index >= Weapons.Length) { Debug.LogWarning("Invalid weapon index"); return; }

        foreach (var weapon in Weapons)
            weapon.SetActive(false);

        EquipWeapon(CurrentWeapon[index]);
        Weapons[index].SetActive(true);
    }

 

  
}