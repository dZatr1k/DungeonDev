using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Heroes;

namespace Units.Enemies
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _body;
        [SerializeField] private float _speed;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Hero hero))
            {
                Stop();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Hero hero))
            {
                Move();
            }
        }

        private void OnValidate()
        {
            _body ??= GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            Move();
        }

        private IEnumerator StopCoroutine()
        {
            yield return new WaitForSeconds(0.1f);
            _body.velocity = Vector2.zero;
        }

        public void Move()
        {
            _body.velocity = Vector2.left * _speed;
        }

        public void Stop()
        {
            StartCoroutine(StopCoroutine());
        }
    }
}
