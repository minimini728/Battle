using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    public int id;
    public string name;
    public int hp;
    public int damage; //�⺻ ������
    public int element_id; //�Ӽ� id
    public string prefab_name;
    public string sprite_name;
    public int skill_damage; //��ų ������

    public CharacterData Clone()
    {
        return new CharacterData
        {
            id = this.id,
            name = this.name,
            hp = this.hp,
            damage = this.damage,
            element_id = this.element_id,
            prefab_name = this.prefab_name,
            sprite_name = this.sprite_name,
            skill_damage = this.skill_damage

        };
    }
}
