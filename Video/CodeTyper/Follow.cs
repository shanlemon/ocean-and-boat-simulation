public class FollowXZ : MonoBehaviour
{

  [SerializeField] private Transform target;
  void Update()
  {
    Vector3 position = new Vector3(target.position.x, transform.position.y, target.position.z);
    transform.position = position;
  }
}
