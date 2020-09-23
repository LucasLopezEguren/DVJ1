using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] levelChunks;
    public int amountChunks;
    // Start is called before the first frame update
    void Start()
    {
        int amountChunksTypes = levelChunks.Length;
        for (int i = 0; i <= amountChunks; i++) {
            GameObject toInitiatie = levelChunks[Mathf.FloorToInt(UnityEngine.Random.Range(0f, Mathf.Round(amountChunksTypes)))];
            Debug.Log(toInitiatie.GetComponentInChildren<Collider2D>().bounds.size);
            Debug.Log(i*10);
            Instantiate(toInitiatie, new Vector3(i*50, 0, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
