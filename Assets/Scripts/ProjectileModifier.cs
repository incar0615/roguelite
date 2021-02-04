using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    public enum ProjectileModifiers
    {
        None = 0,
        SplitShot,
        Split3,
        Boomerang,
        FireTrail,
    }

    public static class ProjectileExtension
    {
        /// <summary>
        /// 멀티샷
        /// </summary>
        /// <param name="projectile"></param>
        /// <param name="splitCnt"></param>
        /// <param name="intervalAngle"></param>
        /// <param name="maxTravelDist"></param>
        public static void SpiltShot(this ProjectileBehaviour projectile, int splitCnt, float intervalAngle, float maxTravelDist = -1)
        {
            float startAngle = (float)((splitCnt / 2) * intervalAngle * (splitCnt % 2 == 0 ? -0.5f : -1.0f));

            for (int i = 0; i < splitCnt; i++)
            {
                ProjectileBehaviour bullet = PoolManager.Instance.GetObjectFromPool("ProjectileBehaviour", projectile.transform.position, 
                    projectile.transform.rotation).GetComponent<ProjectileBehaviour>();

                Vector3 dir = projectile.Direction.V3Rotate(startAngle + (intervalAngle * i));

                bullet.Shoot(dir.normalized, projectile.Magic, maxTravelDist, projectile.Depth + 1);
            }
        }

        /// <summary>
        /// 방향 변경하는 투사체
        /// </summary>
        /// <param name="projectile"></param>
        /// <param name="angle"></param>
        /// <param name="maxTravelDist"></param>
        public static void ChangeDirShot(this ProjectileBehaviour projectile, float angle, float maxTravelDist = -1)
        {
            Vector3 dir = projectile.Direction.V3Rotate(angle);

            ProjectileBehaviour bullet = PoolManager.Instance.GetObjectFromPool("ProjectileBehaviour", projectile.transform.position,
                    projectile.transform.rotation).GetComponent<ProjectileBehaviour>();

            bullet.Shoot(dir.normalized, projectile.Magic, maxTravelDist, projectile.Depth + 1);
        }

        /// <summary>
        /// 경로를 따라 자취를 남기는 투사체
        /// </summary>
        /// <param name="projectile"></param>
        /// <param name="element"></param>
        /// <param name="period"></param>
        /// <param name="duration"></param>
        /// <param name="maxTravelDist"></param>
        public static void TrailShot(this ProjectileBehaviour projectile, GameObjects.Element element, float period, float duration, float maxTravelDist = -1)
        {
            ProjectileBehaviour bullet = PoolManager.Instance.GetObjectFromPool("ProjectileBehaviour", projectile.transform.position,
                    projectile.transform.rotation).GetComponent<ProjectileBehaviour>();
            bullet.transform.position = projectile.transform.position;

            TrailSpawner trailSpawner = PoolManager.Instance.GetObjectFromPool("TrailSpawner", projectile.transform.position,
                    projectile.transform.rotation).GetComponent<TrailSpawner>();
            trailSpawner.Element = element;
            trailSpawner.Period = period;
            trailSpawner.Duration = duration;
            trailSpawner.transform.SetParent(bullet.transform);
            trailSpawner.transform.position = bullet.transform.position;
            
            bullet.Shoot(projectile.Direction, projectile.Magic, maxTravelDist, projectile.Depth + 1);
        }
    }
}