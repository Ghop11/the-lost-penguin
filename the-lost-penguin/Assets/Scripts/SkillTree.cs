using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public static SkillTree instance;
    
    public SkillNode[] skillsNodes;
    public List<SkillUI> skillList;
    public GameObject skillHolder; //contains all the data for the skills
    public List<GameObject> connectorList;
    public GameObject connectorHolder;

    //use current level to decide when skills will be available
    void Start()
    {
        const int Limit = 6;
        skillsNodes = new SkillNode[Limit];

        skillList = new List<SkillUI>();

        for(int i = 0; i < Limit; i++)
        {
            skillsNodes[i].PlayerLevelRequirement = i + 1;
            skillsNodes[i].Title = $"Placeholder Title {i + 1}";
            skillsNodes[i].Description = $"Place Holder Description {i + 1}";
            skillsNodes[i].Cost = 1;
            skillsNodes[i].TreeLevel = 0;
        }

        // put all skills and image connectors from game UI into list
        foreach(var skill in skillHolder.GetComponentsInChildren<SkillUI>())
            skillList.Add(skill);
        
        foreach(var connector in connectorHolder.GetComponentsInChildren<RectTransform>())
            connectorList.Add(connector.gameObject);

        //assign tree ids to the skills list
        for(int i = 0; i < skillList.Count; i++)
            skillList[i].idIndex = i;

        AssignSkillTreePath();

        UpdateAllSkillUI();
    }

    public void UpdateAllSkillUI()
    {
        //call all instances of the skillUI method
        foreach(var skill in skillList)
            skill.UpdateSkillTreeUI();
    }

    //TODO: THIS IS NOT WORKING AS EXPECTED, FIX LATER
    void AssignSkillTreePath()
    {
        //the numbers by the prefab copies names in the hiearchy is the
        //connected skill levels
        
        skillList[0].connectedSkills = new int[]{1, 2}; //first upgrade branches to two upgrades, 
        skillList[1].connectedSkills = new int[]{3}; //bottom branch has one extending node
        skillList[2].connectedSkills = new int[]{4}; //same as top
        skillList[3].connectedSkills = new int[]{5}; //both 3 and 4 extend to 5
        skillList[4].connectedSkills = new int[]{5};

        //TODO: Fix display issues with image tree paths
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        instance = this;
    }
}
