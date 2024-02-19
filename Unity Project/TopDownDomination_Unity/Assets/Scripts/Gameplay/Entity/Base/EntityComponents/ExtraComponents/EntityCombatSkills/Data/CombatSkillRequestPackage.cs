﻿using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data
{
    public struct CombatSkillRequestPackage
    {
        public CombatSkillType SkillType;
        public Quaternion CastRotation;
        public Vector3 CastDirection;
        public Vector3 CastPosition;
    }
}