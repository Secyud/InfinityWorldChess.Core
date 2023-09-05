using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.PassiveSkills
{
    public class PropertyAdd : PassiveSkillTemplate
    {
        [field:S] public int D256 { get; set; }

        [field:S] public byte D257 { get; set; }

        public override string HideDescription =>
            $"自身{D256 switch {0 => "生", 1 => "杀", 2 => "灵", 3 => "御", _ => "未知"}}增加{D256}点。";

        public override void Equip(Role role)
        {
            if (D257 > 4) return;

            role.BodyPart[(BodyType)D257].MaxValue += D256;
        }

        public override void UnEquip(Role role)
        {
            if (D257 > 4) return;

            role.BodyPart[(BodyType)D257].MaxValue -= D256;
        }
    }
}