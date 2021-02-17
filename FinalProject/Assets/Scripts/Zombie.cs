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
        if (zombieHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(int dmg)
    {
        zombieHealth -= dmg;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "knife")
        {
            zombieHealth = 0;
        }
    }

}
