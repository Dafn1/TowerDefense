using UnityEngine;

namespace SpaceShooter
{
    public class Rotation : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        private float ZRotation;

        private void Start()
        {
            // Zufällige Rotationsrichtung nur auf der Z-Achse
            ZRotation = Random.Range(-1f, 1f);
        }

        private void Update()
        {
            // Asteroiden langsam um die Z-Achse rotieren
            transform.Rotate(0, 0, ZRotation * rotationSpeed * Time.deltaTime);
        }
    }
}