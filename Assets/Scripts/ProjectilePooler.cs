using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
    public ProjectileScript prefab;
    public Stack<ProjectileScript> projectiles;
    public Stack<ProjectileScript> magazine;

    public ProjectileScript FireProjectile(Vector3 from, Vector3 to, float speed)
    {
        var projectile = (magazine != null && magazine.Count > 0) ? 
            magazine.Pop() : Instantiate<ProjectileScript>(prefab, transform);

        projectile.pool = this;
        projectile.transform.position = from;
        projectile.transform.rotation = Quaternion.LookRotation(to - from);
        projectile.gameObject.SetActive(true);
        projectile.rigidbody.velocity = (to - from).normalized * speed;
        return projectile;
    }

    public void ReloadProjectile(ProjectileScript projectile) {
        projectile.gameObject.SetActive(false);
        projectile.rigidbody.velocity = Vector3.zero;
        projectile.transform.position = transform.position;
    } 
}
