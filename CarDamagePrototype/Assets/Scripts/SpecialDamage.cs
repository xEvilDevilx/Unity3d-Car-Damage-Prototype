using System;

using UnityEngine;

namespace CDP
{
    /// <summary>
    /// Implements a custom damage functionality for mark any car details like special behaviour damage
    /// </summary>
    public class SpecialDamage : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _detailsForDisable;
        [SerializeField]
        private Sprite _damagedSprite;
        private bool _isDamaged = false;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!_isDamaged)
            {
                CarController carController = GetComponentInParent<CarController>();
                float currSpeed = Math.Abs(carController.CurrentSpeed);

                CarController colCarController = col.gameObject.GetComponent<CarController>();
                float colCurrSpeed = colCarController != null ? Math.Abs(colCarController.CurrentSpeed) : 0f;

                if (currSpeed > 3.5f || (colCurrSpeed > 3.5f))
                {
                    Damage();
                }
            }
        }

        private void Damage()
        {
            foreach (var detail in _detailsForDisable)
            {
                detail.SetActive(false);
            }

            GetComponent<SpriteRenderer>().sprite = _damagedSprite;
            _isDamaged = true;
        }
    }
}