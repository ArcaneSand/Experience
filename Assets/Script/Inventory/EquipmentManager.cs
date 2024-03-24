using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager I;
    private void Awake() {
        I = this;
        ArmorSprite.gameObject.SetActive(false);
        WeaponSprite.gameObject.SetActive(false);
    }
    public int bareHand;
    public bool isWearingArmor = false;
    public Image ArmorSprite;
    public Armor currentArmor;
    public TextMeshProUGUI armorText;
    public bool isWearingWeapon = false;
    public Weapon currentWeapon;
    public Image WeaponSprite;
    public TextMeshProUGUI weaponText;

    public void SetWeapon(Weapon weapon){
        if(currentWeapon==weapon){
            UnEquipWeapon();
            return;
        }
        currentWeapon = weapon;
        isWearingWeapon = true;
        CharactorManager.I.Atk = weapon.damage;
        WeaponSprite.gameObject.SetActive(true);
        WeaponSprite.sprite = weapon.sprite[0];
        weaponText.text = "Attack: " + weapon.damage.ToString();
    }

    public void SetArmor(Armor armor){
        if(currentArmor==armor){
            UnEquipArmor();
            return;
        }
        currentArmor = armor;
        isWearingArmor = true; 
        CharactorManager.I.Def = armor.armor;
        ArmorSprite.gameObject.SetActive(true);
        ArmorSprite.sprite = armor.sprite[0];
        armorText.text = "Armor: " + armor.armor.ToString();
    }

    public void UnEquipWeapon(){
        currentWeapon = null;
        isWearingWeapon = false;
        CharactorManager.I.Atk = bareHand;
        WeaponSprite.gameObject.SetActive(false);
        weaponText.text = "Attack: " + bareHand.ToString();
    }
    public void UnEquipArmor(){
        currentArmor = null;
        isWearingArmor = false;
        CharactorManager.I.Def = 0;
        ArmorSprite.gameObject.SetActive(false);
        armorText.text = "Armor: 0";
    }
}
