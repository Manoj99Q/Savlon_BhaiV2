using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject bloodshot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Onshot()
    {
        GameObject newObj = Instantiate(bloodshot,transform);
        Destroy(newObj,1);
    }
}
