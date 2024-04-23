using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElement
{
    int CalculateDamage(IElement target);

    int SkillButtonInit(IElement target);

    int UISkillArrowInit(int id);
}
