using System.Runtime.InteropServices;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    [Guid("368B3931-B2DE-1FC2-104B-B504A5B34419")]
    public class CoreSkill : ActiveSkillBase, ICoreSkill
    {
        [field: S(16)] public byte FullCode { get; set; }
        [field: S(16)] public byte MaxLayer { get; set; }

        protected override void SetHideContent(Transform transform)
        {
            transform.AddCoreSkillInfo(this);
            base.SetHideContent(transform);
        }
    }
}