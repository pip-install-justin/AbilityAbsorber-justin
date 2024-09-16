using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> connectedNodes; // List of Nodes that this Node is connected to.
    public bool activated = false; // Is this Node activated?
    
    // This method attempts to activate the Node.
    public void Activate()
    {
        if (!activated)
        {
            activated = true;
            // Activate next node if current node is active
            foreach (Node node in connectedNodes)
            {
                if (!node.activated)
                {
                    node.Activate();
                    break;
                }
            }
        }
    }
}
