using UnityEngine;
using Gameplay.Weapons.Projectiles;
using Gameplay.Weapons;
using Gameplay.Spaceships;

public class Rocket : Projectile
{
    private Transform _target;

    private Vector3 _direction;

    public override void Init(UnitBattleIdentity battleIdentity)
    {
        base.Init(battleIdentity);
        FindTarget(); // ищем цель ракеты
    }

    protected override void Move(float speed)
    {
        if (_target) // если цель нашли
        {
            transform.Translate(speed * Time.deltaTime * Vector3.Normalize(_direction)); // двигаемся в ее первое местоположение
        }
        else
        {
            transform.Translate(speed * Time.deltaTime * Vector3.down); // или двигаемся вперед
        }
    }

    //поиск цели
    private void FindTarget()
    {
        switch (BattleIdentity)
        {
            case UnitBattleIdentity.Ally:
                {
                    LockOnTarget(UnitBattleIdentity.Enemy); // ищем цель типа Враг
                    break;
                }
            case UnitBattleIdentity.Enemy:
                {
                    LockOnTarget(UnitBattleIdentity.Ally); //ищем цель типа Союзник (корабль игрока)
                    break;
                }
            default:
                break;
        }
    }

    private void LockOnTarget(UnitBattleIdentity identity)
    {
        var ships = FindObjectsOfType<Spaceship>();
        foreach (var ship in ships)
        {
            if (ship is IDamagable damagable)
            {
                if (damagable.BattleIdentity == identity)
                {
                    _target = ship.gameObject.transform;
                    _direction = transform.position - _target.position; // при нахождении цели записываем направление к ней, расчитывается только один раз
                    break;
                }
            }
        }
    }
}
