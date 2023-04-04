using UnityEngine;

namespace Level.CharacterShoot
{
    public class ShellCreator : MonoBehaviour
    {
        [SerializeField] private GameObject shellPrefab;
        [SerializeField] private Vector3 shellSpawnPosition;

        public Shell CreateShell()
        {
            var shellObject = Instantiate(shellPrefab, shellSpawnPosition, Quaternion.identity, transform);
            return shellObject.GetComponent<Shell>();
        }
    }
}