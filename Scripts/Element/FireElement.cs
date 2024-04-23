using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElement : IElement
{

    public int CalculateDamage(IElement target)
    {
        int damage = 0;
        switch (target)
        {
            case WaterElement:
                damage -= 500;
                break;
            case ForestElement:
                damage += 500;
                break;
            case FireElement:
                damage = 0;
                break;
        }
        return damage;
    }

    public int SkillButtonInit(IElement target)
    {
        int num = 0;
        switch (target)
        {
            case WaterElement:
                num = 2;
                break;
            case ForestElement:
                num = 1;
                break;
            case FireElement:
                num = 0;
                break;
        }

        return num;
    }

    public int UISkillArrowInit(int id)
    {
        int num = 0;
        switch (id)
        {
            case 100:
                num = 2;
                break;
            case 101:
                num = 0;
                break;
            case 102:
                num = 1;
                break;
        }

        return num;
    }
}
