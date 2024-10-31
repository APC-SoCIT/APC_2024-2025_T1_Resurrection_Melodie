using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class NodeParser : MonoBehaviour
{
    public DialogueGraph graph;
    public Text speaker;
    public Text dialogue;
    public Image speakerImage;
    private Coroutine _parser;

    private void Start()
    {
        foreach (BaseNode b in graph.nodes)
        {
            if (b.GetString() == "Start")
            {
                graph.current = b;
                break;
            }
        }
        _parser = StartCoroutine(ParseNode());
    }

    IEnumerator ParseNode()
    {
        BaseNode b = graph.current;
        string data = b.GetString();
        string[] dataParts = data.Split('/');
        
        if (dataParts[0] == "Start")
        {
            NextNode("exit");
        }
        else if (dataParts[0] == "DialogueNode")
        {
            speaker.text = dataParts[1];
            dialogue.text = dataParts[2];
            speakerImage.sprite = b.GetSprite();

            // Trigger effects if the node is of type DialogueNode
            DialogueNode dialogueNode = b as DialogueNode;
            if (dialogueNode != null)
            {
                dialogueNode.TriggerEffects();
            }

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            NextNode("exit");
        }
    }

    public void NextNode(string fieldName)
    {
        if (_parser != null)
        {
            StopCoroutine(_parser);
            _parser = null;
        }

        foreach (NodePort p in graph.current.Ports)
        {
            if (p.fieldName == fieldName)
            {
                graph.current = p.Connection.node as BaseNode;
                break;
            }
        }
        _parser = StartCoroutine(ParseNode());
    }
}
