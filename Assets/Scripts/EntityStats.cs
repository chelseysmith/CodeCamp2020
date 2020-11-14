using UnityEngine;
using UnityEngine.AI;

public class EntityStats : MonoBehaviour
{
    [SerializeField] int _life;
    public Enums.Fire Fire { get; set; } = Enums.Fire.na;
    public int TimeOnFire { get; set; }
    Renderer _renderer;
    public bool Dead { get; set; }

    public void Tick()
    {
        _life -= (int)Fire;
        TimeOnFire++;

        if (_life <= 0)
        {
            Dead = true;

            var obsticle = GetComponent<NavMeshObstacle>();
            obsticle.carving = true;
        }
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    public void CatchFire(Enums.Fire fire, Material material, GameObject fireEffect)
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material = material;
        Fire = fire;

        var obstacle = gameObject.AddComponent(typeof(NavMeshObstacle)) as NavMeshObstacle;
        obstacle.carving = true;

        transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);

        var effect = GameObject.Instantiate(fireEffect, new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z), Quaternion.identity);
        effect.transform.parent = transform;
    }
}