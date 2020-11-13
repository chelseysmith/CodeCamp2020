using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public int Life { get; set; }
    public Enums.Fire Fire { get; set; } = Enums.Fire.na;
    float TimeOnFire = 0;
    float FireStartTime;

    [SerializeField] Material fire1;
    [SerializeField] Material fire2;
    [SerializeField] Material fire3;

    void Start()
    {
        
    }

    void Update()
    {
        Life -= (int)Fire;

        if(Life <= 0)
        {
            Destroy(gameObject);
        }

        if(Fire != Enums.Fire.na)
        {
            TimeOnFire = Time.time - FireStartTime;
        }
    }

    public void CatchFire(Enums.Fire fire)
    {
        FireStartTime = Time.time;
        Fire = fire;

        switch(Fire)
        {
            case Enums.Fire.level1:

                break;

            case Enums.Fire.level2:

                break;

            case Enums.Fire.level3:

                break;
        }
    }
}
