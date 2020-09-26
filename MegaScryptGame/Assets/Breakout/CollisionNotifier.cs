using UnityEngine;

public class CollisionNotifier : MonoBehaviour
{
    private BreakoutGameController _controller;

    #region Unity Functions
    private void OnTriggerEnter(Collider other)
    {
        if (_controller == null)
        {
            Debug.Log("sfd");
        }

        _controller.CollisionTrigger(gameObject, other.gameObject);
    }

    #endregion

    #region External Functions

    public void SetController(BreakoutGameController controller) => _controller = controller;

    #endregion
}
