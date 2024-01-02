using System.Runtime.InteropServices;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    [Guid("AF1A570B-807B-7142-B5C4-966656512275")]
    public class FormSkill : ActiveSkillBase,IFormSkill
    {
        [field:S(16)]public byte Type { get; set; }
        [field:S(16)]public byte State { get; set; }

        protected override void SetHideContent(Transform transform)
        {
            transform.AddFormSkillInfo(this);
            base.SetHideContent(transform);
        }
    }
}