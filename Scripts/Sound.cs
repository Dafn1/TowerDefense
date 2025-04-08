
using UnityEngine;

namespace TowerDefense
{
    public enum Sound
    {
        BGM=0,
        ProjArrow=1,
        ISEnemyDamageArrow=2,
        projMagic=3,
        EnemyDie=4,
        EnemyWent=5,
        Win=6,
        Lose=7,
        WeveGo,
    }
    public static class SoundExtensions
    {
        public static void Play(this Sound sound)
        {
            SoundPlayer.Instance.Play(sound);
            
        }
    }
}
