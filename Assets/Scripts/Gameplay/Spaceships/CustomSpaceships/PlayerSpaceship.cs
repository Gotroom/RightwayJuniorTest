using UnityEngine;
using System;
using System.Collections;
using Gameplay.Spaceships;
using Gameplay.Pickupables;
using Gameplay.Weapons;

public class PlayerSpaceship : Spaceship, IPickuper
{

    [SerializeField]
    private float _endurance = 100; // кол-во жизней

    public static Action<float> EnduranceChanged; // событие при изменении кол-ва жизней
    public static Action Die; // событие при поражении (_endurance == 0)
    public static Action<bool> Boosted; // изменения состояние буста от предмета

    protected override void Start()
    {
        base.Start();
        EnduranceChanged.Invoke(_endurance); // чтобы в UI отобразилось начальное здоровье
    }

    public override void ApplyDamage(IDamageDealer damageDealer)
    {
        _endurance -= damageDealer.Damage; // уменьшаем кол-во здоровья
        EnduranceChanged.Invoke(_endurance);
        if (_endurance <= 0)
        {
            Die.Invoke();
        }
    }

    public void PickUp(IPickupable item) // при поднятии предмета
    {
        switch (item.ItemType)
        {
            case PickUpItemType.Healthpack:
                {
                    if (item is Healthpack healthPack)
                    {
                        _endurance += healthPack.HealingPower; // если подняли лечилку, то увеличиваем здоровье
                        EnduranceChanged.Invoke(_endurance);
                    }
                    break;
                }
            case PickUpItemType.Booster:
                {
                    if (item is Boost booster)
                    {
                        StartCoroutine(BoostPickedUp(booster.BoostTime)); // на время включаем буст
                    }
                    break;
                }
            default:
                break;
        }
    }

    IEnumerator BoostPickedUp(float time)
    {
        Boosted.Invoke(true); // сообщаем, что буст начался
        yield return new WaitForSeconds(time);
        Boosted.Invoke(false); // и окончился
    }
}
