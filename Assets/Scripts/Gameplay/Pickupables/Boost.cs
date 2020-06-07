using UnityEngine;
using System.Collections;

namespace Gameplay.Pickupables
{
    public class Boost : MonoBehaviour, IPickupable
    {
        [SerializeField]
        private float _boostTime = 10.0f; // время действия буста в секундах


        [SerializeField]
        private float _speed = 5.0f; // скорость движения по игровому полю

        private PickUpItemType _itemType = PickUpItemType.Booster;

        public PickUpItemType ItemType => _itemType;
        public float BoostTime => _boostTime;

        // перемещаем объект
        private void Update()
        {
            transform.Translate(Vector3.down * Time.deltaTime * _speed);
        }

        // при столкновении
        private void OnCollisionEnter2D(Collision2D other)
        {
            var pickUper = other.gameObject.GetComponent<IPickuper>(); // IPickuper может быть только корабль игрока

            if (pickUper != null)
            {
                pickUper.PickUp(this);
                Destroy(gameObject); //уничтожение объекта после столкновения с кораблем игрока
            }
        }
    }
}

