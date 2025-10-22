using UnityEngine;

[CreateAssetMenu(fileName = "UnitDefinition", menuName = "Game/UnitDefinition")]
public class UnitDefinition : ScriptableObject
{
    public string id;
    public GameObject enemyPrefab;
    public GameObject undeadPrefab;
    public GameObject deadPrefab;
    public float movementSpeed = 1;
    public int attackDamage;
    public int attackDamageVariance;
    public float chasingRange;
    public float attackRange;
    public Sprite icon;
    public int startingSoulCount;
}