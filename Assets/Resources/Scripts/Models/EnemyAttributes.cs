using UnityEngine;

[CreateAssetMenu (menuName ="Enemy Attribute")]
public class EnemyAttributes : ScriptableObject
{
    public float health;
    public float speed;
    public float damage;
    public float shotCooldown;
    public Vector3 modelScale;
    public Vector3 colliderScale;
    public Vector3 colliderCenter;
    public Vector3[] cannonPositions;
    public GameObject model;
}