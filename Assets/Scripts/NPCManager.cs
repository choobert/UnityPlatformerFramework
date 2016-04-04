using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour {

    private static NPCManager _instance;
    private static GameManager _gm;

    public Dictionary<string, NPC> npcs = new Dictionary<string, NPC>();

    /*
	* Find/Create/Return our one and only Game Manager object
	* for the game
	**/
    public static NPCManager Instance
    {
        get
        {
            // if we do not have an instance already, lets look to see
            // if one has already been created for us
            if (_instance == null)
            {
                _instance = Object.FindObjectOfType<NPCManager>();
            }

            // If we still dont have an instance, it must not exist,
            // so lets create our own and add it to the GameManager object
            if (_instance == null)
            {

                // Find the GameManager then add the NPCManager to the same GameObject
                _gm = GameManager.Instance;
                _instance = _gm.gameObject.AddComponent<NPCManager>();
            }

            return _instance;
        }
    }

    public void addNPC(string npcId, NPC npc) {
        npcs.Add(npcId, npc);
    }

    public NPC GetNPCByID(string npcId){
		return npcs[npcId];
	}
}
