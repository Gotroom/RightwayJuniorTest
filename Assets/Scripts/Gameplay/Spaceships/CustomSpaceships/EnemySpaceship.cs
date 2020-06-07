using UnityEngine;
using System;
using Gameplay.Spaceships;
using Gameplay.Weapons;

public class EnemySpaceship : Spaceship
{

    [SerializeField]
    private int _destroyPoints = 10;

    [SerializeField]
    private int _dropChancePercent = 30;

    [SerializeField]
    private GameObject[] _drop;

    public static Action<int> IncreaseScore;

    public override void ApplyDamage(IDamageDealer damageDealer)
    {
        IncreaseScore.Invoke(_destroyPoints);
        SpawnPickupable(); // спавним на месте погибшего врага дроп
        Destroy(gameObject);
    }

    private void SpawnPickupable()
    {
        if (UnityEngine.Random.Range(0, 100) <= _dropChancePercent) // проверяем шанс дропа
        {
            var index = UnityEngine.Random.Range(0, _drop.Length); // дропаем что-то одно
            Instantiate(_drop[index], transform.position, Quaternion.identity);
        }
    }

}
