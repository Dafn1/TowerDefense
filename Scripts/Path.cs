using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private CircleArea startAria;
        public CircleArea StartAria { get { return startAria; } }
        [SerializeField] private AIPointPatrol[] points;
        public int Length { get => points.Length; }
        public AIPointPatrol this [int i] { get => points[i]; }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            for (int i = 0; i < points.Length; i++)
            {
                // Draw the sphere
                Gizmos.DrawSphere(points[i].transform.position, points[i].Radius);

                // Draw a line to the next point if it exists
                if (i + 1 < points.Length)
                {
                    Gizmos.DrawLine(points[i].transform.position, points[i + 1].transform.position);
                }
            }
        }
    }
}