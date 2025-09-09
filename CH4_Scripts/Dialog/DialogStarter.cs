using cherrydev;
using UnityEngine;

public class DialogStarter : MonoBehaviour
{
    [SerializeField] private DialogBehaviour DialogBehaviour;
    [SerializeField] private DialogNodeGraph dialogGraph;

    private void Start()
    {
        DialogBehaviour.StartDialog(dialogGraph);
    }
}
