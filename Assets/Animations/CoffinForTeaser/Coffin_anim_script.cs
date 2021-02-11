
using UnityEngine;

public class Coffin_anim_script : MonoBehaviour
{
    private Animator _anim;

    private void Awake() => _anim = GetComponent<Animator>();
    

    public void NextAnim() => _anim.SetBool("NextAnim", true);

}
