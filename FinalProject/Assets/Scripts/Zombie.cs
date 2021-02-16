using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    public int zombieHealth;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "Zombie";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int dmg)
    {
        zombieHealth -= dmg;
    }

}
