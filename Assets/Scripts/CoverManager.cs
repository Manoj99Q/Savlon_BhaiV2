using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverManager : MonoBehaviour
{
    // Start is called before the first frame update

   [SerializeField] private Transform[] coverpoints;
    [SerializeField] float changeTime;
    private bool idle;
    private int idx;
    private float timer;
    [SerializeField] float  moveSpeed ;
    [SerializeField] private float threshold;
     private Animator anim;
    private GameObject deerChild;

    private void Awake()
    {
        timer = changeTime + 1;
        idle = true;
        deerChild = transform.GetChild(0).gameObject;
        anim = deerChild.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (idle)
        {
            anim.SetBool("running", false);
            timer += Time.deltaTime;

            if (timer > changeTime)
            {
                idle = false;
                idx = Random.Range(0, coverpoints.Length);
                timer = 0;
            }
        }

        else
        {
            anim.SetBool("running", true);
            Vector3 coverPos = coverpoints[idx].position;

            Vector3 direction = coverPos - transform.position;
            if(direction.x >0)
            {
                Vector3 scale = deerChild.transform.localScale;
                deerChild.transform.localScale = new Vector3(1 * Mathf.Abs(scale.x), scale.y, scale.z);
            }
            else
            {
                Vector3 scale = deerChild.transform.localScale;
                deerChild.transform.localScale = new Vector3(-1 * Mathf.Abs(scale.x), scale.y, scale.z);
            }
            // Normalize the direction to get a unit vector (length of 1).
            direction.Normalize();

            transform.Translate(direction * moveSpeed * Time.deltaTime);

             // Adjust this based on your needs.
            if (Vector3.Distance(transform.position, coverPos) < threshold)
            {
                idle = true;
            }
        }
        

       


    }
}
