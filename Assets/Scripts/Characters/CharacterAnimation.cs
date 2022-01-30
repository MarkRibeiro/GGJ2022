using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Character))]
public class CharacterAnimation : MonoBehaviour
{
    Animator anim;
    Character character;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        character = GetComponent<Character>();
        anim.SetFloat("offset", Random.Range(0f, 1f));
        anim.SetFloat("speed", 1f);
    }
    private void Update()
    {
        if (character.currHP < character.maxHP / 2f)
        {
            anim.SetFloat("speed", 2.5f);

        }
    }
}
