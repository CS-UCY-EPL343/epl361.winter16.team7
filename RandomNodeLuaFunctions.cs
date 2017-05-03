using UnityEngine;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;


/// <summary>
/// Registers 2 custom functions with Lua. 
/// (See http://pixelcrushers.com/dialogue_system/manual/html/lua_in_scripts.html#luaClassRegisterFunction)
/// The "Random" conversation uses these functions to show nodes in a random order.
/// </summary>
public class RandomNodeLuaFunctions : MonoBehaviour
{
	/// <summary>
	/// The lise of nodes.
	/// </summary>
    private List<int> nodes = new List<int>();

	/// <summary>
	/// The list of nodes to save them for final results
	/// so we can know the questions.
	/// </summary>
	public static  List<int> listNodes =new List<int>(); 

	/// <summary>
	/// The NUMBER of NODES (number of questions total).
	/// </summary>
	private const int NUMBER_OF_NODES=6;

	/// <summary>
	/// The number of question to select for the quiz.
	/// </summary>
	public const int NODES_TO_SELECT=5;


    void OnEnable()
    {
		listNodes.Clear ();
       // Lua.RegisterFunction("RandomNodeOrder", this, typeof(RandomNodeLuaFunctions).GetMethod("RandomNodeOrder"));
        Lua.RegisterFunction("GetNextNode", this, typeof(RandomNodeLuaFunctions).GetMethod("GetNextNode"));
		RandomNodeOrder (NUMBER_OF_NODES);

    }

    void OnDisable()
    {
      //  Lua.UnregisterFunction("RandomNodeOrder");
        Lua.UnregisterFunction("GetNextNode");
    }

    /// <summary>
    /// Add the nodes to the list, randomize them and saves them
    /// </summary>
    /// <param name="count">Number of nodes to be added</param>
    public void RandomNodeOrder(double count) // Lua uses doubles for all numbers.
    {
        // Create the list of node numbers:
        nodes.Clear();
        for (int i = 1; i <= count; i++)
        {
            nodes.Add(i);
        }

        // Then shuffle it:
        int n = nodes.Count;
        while (n > 1)
        {
            int k = (Random.Range(0, n) % n);
            n--;
            var value = nodes[k];
            nodes[k] = nodes[n];
            nodes[n] = value;
        }

		//eliminate nodes
		while (nodes.Count > NODES_TO_SELECT) {
			nodes.RemoveAt (0);
		}

		//save nodes
		for (int i=0;i<nodes.Count;i++){
			listNodes.Add (nodes [i]);
		}
		var savelist = "";

		for (int i=0;i<listNodes.Count;i++){
			savelist += listNodes [i] + " ";
		}

		if (DialogueLua.GetVariable("questionString").AsString.Equals("@@@")){
			
			DialogueLua.SetVariable ("questionString", savelist);}

		/*var attributes = savelist.Split (' ');
		for (int i = 0; i < attributes.Length; i++)
			Debug.Log (attributes[i]);*/
        // Log it:
        var s = "Random node order: ";
        for (int i = 0; i < nodes.Count; i++)
        {
            s += nodes[i] + " ";
        }
        Debug.Log(s);
    }

	/// <summary>
	/// Gets the next node.
	/// </summary>
	/// <returns>The next node.</returns>
    public double GetNextNode()
    {
        // Remove the next node number and return it:
        if (nodes.Count == 0) return -1;
        var next = nodes[0];
        nodes.RemoveAt(0);
		Debug.Log(next);
        return next;
    }

}
