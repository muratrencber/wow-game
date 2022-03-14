using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proceduralgen : MonoBehaviour
{

    public GameObject[] platforms;
    public GameObject[] platform_complexes;
    public GameObject player;

    public void Start()
    {

        player=GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(spawn_loop());


    }

    public IEnumerator spawn_loop()
    {
        yield return new WaitForSeconds(5);
        while (true)
        {
            yield return new WaitForSeconds(5);
            spawn_platform();
        }
    }

    public void spawn_platform()
    {

        int rnd=Random.Range(0,platforms.Length);

      GameObject sa=  Instantiate(platforms[rnd]);
      //x değeri playerın olduğu yere göre + şeklinde çıkıcak
      
        float y_random=Random.Range(-2f,2f);
        sa.transform.position=new Vector3(player.transform.position.x+5,y_random,0);
    }

}
