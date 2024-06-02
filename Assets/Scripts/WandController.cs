using UnityEngine;

namespace Arsh.Scripts
{
    public class WandController : MonoBehaviour
    {
        private PlayerController _playerController;
        private bool _isAttackTriggered;

        public int damageAmount = 20;

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
            /*
            if (other.tag == "enemy") {
                //transform.parent = other.transform;
                other.GetComponent<EnemyHealthBar>().TakeDamage(damageAmount);
            }
            */
             if (_isAttackTriggered && other.tag == "enemy")
            {
                other.GetComponent<EnemyHealthBar>().TakeDamage(damageAmount);
            }
            
            // Debug.Log(_isAttackTriggered);
            // if (other.name == "Cube")
            // {
            //     Debug.Log("cube");
            // }
        }
    }
}
