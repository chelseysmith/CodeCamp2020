﻿using UnityEngine;

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
            Dead = true;
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    public void CatchFire(Enums.Fire fire, Material material)
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material = material;
        Fire = fire;
    }
}
