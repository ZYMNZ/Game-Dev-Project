using UnityEngine;

namespace Arsh.Scripts
{
    public class WandController : MonoBehaviour
    {
        private PlayerController _playerController;
        private bool _isAttackTriggered;

        public GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            _isAttackTriggered = player.GetComponent<PlayerController>().AttackTrigger;
            
        }

        private void OnTriggerEnter(Collider other)
        {
            
            // Debug.Log(_isAttackTriggered);
            // if (other.name == "Cube")
            // {
            //     Debug.Log("cube");
            // }
        }
    }
}
