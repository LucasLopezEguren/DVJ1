using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeUI : MonoBehaviour
{
    private SkillTree skillTree;

    // Start is called before the first frame update
    void Start()
    {
        skillTree = (SkillTree)GameObject.Find("SkillTree").GetComponent("SkillTree");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
