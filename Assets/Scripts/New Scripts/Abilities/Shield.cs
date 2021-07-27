using UnityEngine;

public class Shield : MonoBehaviour
{
    

    [SerializeField] private LayerMask playerInShield;
    [SerializeField] private LayerMask playerOutShield;

    [SerializeField] private string setShieldWhenPlayerIn;
    [SerializeField] private string setShieldWhenPlayerOut;

    [SerializeField] private DamageblePlace shieldDamagePlace;
   

    private void OnDisable()
    {
        SetDefaultShieldLayer();
        SetDefaultPlayerLayer();
    }

    private void OnTriggerEnter(Collider other)
    {
      var player = other.GetComponentInParent<PlayerController>();
        if(player != null)
        {
            player.gameObject.layer = LayerMask.NameToLayer("InShield");
            shieldDamagePlace.gameObject.layer = LayerMask.NameToLayer(setShieldWhenPlayerIn);

            var playerDamagePlace = player.GetComponentInChildren<DamageblePlace>();
            if(playerDamagePlace != null)
            {
               playerDamagePlace.Layer = playerInShield;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
      var player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            SetDefaultShieldLayer();
            var playerDamagePlace = player.GetComponentInChildren<DamageblePlace>();
            if(playerDamagePlace != null)
            {
                SetDefaultPlayerLayer(playerDamagePlace, player);
            }
        }
    }


    private void SetDefaultPlayerLayer(DamageblePlace playerPlace, PlayerController player)
    {
        playerPlace.Layer = playerOutShield;
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void SetDefaultPlayerLayer()
    {
        var player = PlayerInformation.GetInstance().PlayerController;
        var playerDamagePlace = player.GetComponentInChildren<DamageblePlace>();
        if(player != null && playerDamagePlace != null)
        {
            player.gameObject.layer = LayerMask.NameToLayer("Player");
            playerDamagePlace.Layer = playerOutShield;
        }

    }

    private void SetDefaultShieldLayer()
    {
        shieldDamagePlace.gameObject.layer = LayerMask.NameToLayer(setShieldWhenPlayerOut);
    }
}
