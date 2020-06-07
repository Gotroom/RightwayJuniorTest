using System.Collections;
using System.Collections.Generic;
using Gameplay.ShipControllers;
using Gameplay.ShipSystems;
using Gameplay.Weapons;
using UnityEngine;


public class EnemyKamikazeController : ShipController, IDamageDealer
{
    // класс корабля, который наносит урон при столкновении и двигается влево-вправо

    [SerializeField]
    private float _damage; // урон при столкновении с игроком

    [SerializeField]
    private Vector2 _horizontalMovementRange; // максимальное смещение относительно места спавна

    private bool _isMovingRight = true; // флаг движения влево-вправо

    public UnitBattleIdentity BattleIdentity => UnitBattleIdentity.Enemy; // т.к. сам корабль - снаряд

    public float Damage => _damage;

    // корабль не умеет стрелять
    protected override void ProcessFire(WeaponSystem fireSystem)
    {
        return;
    }

    //обработка движения
    protected override void ProcessHandling(MovementSystem movementSystem)
    {
        movementSystem.LongitudinalMovement(Time.deltaTime); //по оси Y
        // по оси X
        if (_isMovingRight) // вправо
        {
            if ((transform.position - _initialPosition).x < _horizontalMovementRange.y) // проверка на достижение левой границы
            {
                movementSystem.LateralMovement(Time.deltaTime * Vector3.left.x);
            }
            else
            {
                _isMovingRight = false;
            }
        }
        else //влево
        {
            if ((transform.position - _initialPosition).x > _horizontalMovementRange.x) // проверка на достижение правой границы
            {
                movementSystem.LateralMovement(Time.deltaTime * Vector3.right.x); 
            }
            else
            {
                _isMovingRight = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var damagableObject = collision.gameObject.GetComponent<IDamagable>(); 

        if (damagableObject != null
            && damagableObject.BattleIdentity != BattleIdentity) // проверяем на столкновение с кораблем игрока
        {
            damagableObject.ApplyDamage(this);
            Destroy(gameObject);
        }
    }
}
