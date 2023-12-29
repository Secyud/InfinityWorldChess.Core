#region

using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Ugf;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.BuffDomain;
using TMPro;
using UnityEngine;

#endregion

namespace InfinityWorldChess.Ugf
{
    /// <summary>
    /// 在结构下添加内容
    /// </summary>
    public static class IwcTransformExtension
    {
        private static IwcAssets _assetsLoader;
        private static IwcAssets AssetsLoader => _assetsLoader ??= U.Get<IwcAssets>();
        
        public static string Point(this string str)
        {
            return " · " + str;
        }

        public static string PointAfter(this string str)
        {
            return str + "·";
        }

        public static SText AddTitle1(this Transform transform, string text)
        {
            return AssetsLoader.TitleText1.Value.Create(transform, U.T[text]);
        }

        public static SText AddTitle2(this Transform transform, string text)
        {
            return AssetsLoader.TitleText2.Value.Create(transform, " " + U.T[text]);
        }

        public static SText AddTitle3(this Transform transform, string text)
        {
            return AssetsLoader.TitleText3.Value.Create(transform, "  " + U.T[text]);
        }

        public static SText AddParagraph(this Transform transform, string text)
        {
            return AssetsLoader.BodyFieldText.Value.Create(transform, U.T.Translate(text));
        }

        public static void AddSimpleShown(this Transform transform, IShowable shown)
        {
            transform.AddTitle2(shown.Name);
            if (shown.Description.IsNullOrEmpty())
                return;

            transform.AddParagraph(shown.Description.Point());
        }

        public static void AddListShown<TItem>(this Transform transform, string title, IEnumerable<TItem> items)
        {
            List<IShowable> result = items.TryFindCast<TItem, IShowable>();

            if (result is null || !result.Any())
                return;

            transform.AddTitle3(title);
            foreach (string str in result.Select(
                         item =>
                             $"<size=18><color=#202000ff><b>【{U.T.Translate(item.Name)}】</b> {U.T.Translate(item.Description)}</color></size>"
                     ))
            {
                AssetsLoader.BodyFieldText.Value.Create(transform, str.Point());
            }
        }

        public static void AddShapeProperty(this Transform transform, IAttachProperty property, string title = "形状")
        {
            transform.AddTitle3(title);
            PropertyRect e = AssetsLoader.PropertyRect.Instantiate(transform);
            e.OnInitialize(property);
        }

        // ReSharper disable StringLiteralTypo
        private static void AddSkillScoreInfo(this Transform content, int score)
        {
            string s = (score >> 28) switch
            {
                0 => "<color=#808080ff>[粗浅]</color>",
                1 => "<color=#ffffffff>[入门]</color>",
                2 => "<color=#00ff00ff>[基础]</color>",
                3 => "<color=#00ffffff>[进阶]</color>",
                4 => "<color=#0000ffff>[精深]</color>",
                5 => "<color=#ff00ffff>[奥妙]</color>",
                6 => "<color=#ff0000ff>[玄奇]</color>",
                7 => "<color=#ffff00ff>[绝世]</color>",
                _ => "<color=#000000ff>[未知]</color>"
            };
            content.AddParagraph($"[品质]: {s}".Point());
        }

        public static void AddItemHeader(this Transform content, IItem item)
        {
            content.AddSimpleShown(item);
            string s = (item.Score >> 28) switch
            {
                0 => "<color=#808080ff>[无用]</color>",
                1 => "<color=#ffffffff>[常见]</color>",
                2 => "<color=#00ff00ff>[普通]</color>",
                3 => "<color=#00ffffff>[珍贵]</color>",
                4 => "<color=#0000ffff>[稀有]</color>",
                5 => "<color=#ff00ffff>[奇珍]</color>",
                6 => "<color=#ff0000ff>[异宝]</color>",
                7 => "<color=#ffff00ff>[传世]</color>",
                _ => "<color=#000000ff>[未知]</color>"
            };
            content.AddParagraph($"[品质]: {s}".Point());
        }

        // ReSharper restore StringLiteralTypo

        public static void AddItemFoot(this Transform content, IOverloadedItem item)
        {
            SText text = content.AddParagraph($"数量: {item.Quantity}");
            text.alignment = TextAlignmentOptions.Right;
        }

        public static void AddEquipmentProperty(this Transform transform, IEquipment equipment)
        {
            transform.AddTitle3("详情");

            string text = ("位置: " +
                           (equipment.Location % 4) switch
                           {
                               0 => "生", 1 => "杀", 2 => "灵", 3 => "御", _ => ""
                           }).Point();

            transform.AddParagraph(text);

            text = "基础:".Point();
            text += (equipment.TypeCode & 0b10000000) > 0 ? " 短基" : " 长基";
            text += (equipment.TypeCode & 0b1000000) > 0 ? " 一般" : " 加长";
            transform.AddParagraph(text);

            text = "刃型".Point();
            text += (equipment.TypeCode & 0b10000) > 0 ? " 有" : " 无";
            text += (equipment.TypeCode & 0b10000) > 0 ? " 轻" : " 重";
            text += (equipment.TypeCode & 0b1000) > 0 ? " 软" : " 硬";
            text += (equipment.TypeCode & 0b100) > 0 ? " 双" : " 单";
            transform.AddParagraph(text);

            text = "类型".Point();
            text += (equipment.TypeCode & 0b10) > 0 ? " 远程" : " 近战";
            text += (equipment.TypeCode & 0b1) > 0 ? " 变体" : " 普通";
            transform.AddParagraph(text);
        }

        public static void AddCoreSkillInfo(this Transform content, ICoreSkill coreSkill)
        {
            content.AddSkillScoreInfo(coreSkill.Score);

            content.AddTitle3("兵器限制");
            string s = "";

            for (int i = 0; i < 8; i++)
            {
                uint posCode = 1u << i;
                if ((coreSkill.ConditionMask & posCode) > 0)
                {
                    bool b = (coreSkill.ConditionCode & posCode) > 0;
                    switch (i)
                    {
                        case 0:
                            s += b ? " 长基" : " 短基";
                            break;
                        case 1:
                            s += b ? " 强化" : " 一般";
                            break;
                        case 2:
                            s += b ? " 有刃" : " 无刃";
                            break;
                        case 3:
                            s += b ? " 轻刃" : " 重刃";
                            break;
                        case 4:
                            s += b ? " 软刃" : " 硬刃";
                            break;
                        case 5:
                            s += b ? " 双刃" : " 单刃";
                            break;
                        case 6:
                            s += b ? " 远程" : " 近战";
                            break;
                        case 7:
                            s += b ? " 变体" : " 普通";
                            break;
                    }
                }
            }

            if (s.IsNullOrEmpty())
                s = " 无";

            content.AddParagraph(" ·" + s);

            content.AddTitle3("招式顺序");
            s = "";
            uint code = coreSkill.FullCode;
            for (int i = 0; i < coreSkill.MaxLayer + 1; i++)
            {
                s += (code & 1) > 0 ? "阳" : "阴";
                code >>= 1;
            }

            content.AddParagraph(s.Point());
        }

        public static void AddFormSkillInfo(this Transform content, IFormSkill formSkill)
        {
            content.AddSkillScoreInfo(formSkill.Score);
            string state = formSkill.State switch
            {
                0 => "固守",
                1 => "挪移",
                2 => "神行",
                _ => throw new ArgumentOutOfRangeException()
            };
            string type = formSkill.Type switch
            {
                0 => "金刚",
                1 => "游步",
                2 => "御空",
                _ => throw new ArgumentOutOfRangeException()
            };

            content.AddTitle3("类型");
            content.AddParagraph($"[{state}] [{type}]".Point());
        }

        public static void AddPassiveSkillInfo(this Transform content, IPassiveSkill passiveSkill)
        {
            content.AddSkillScoreInfo(passiveSkill.Score);
            content.AddParagraph($"[生]: {passiveSkill.Living,-5} [杀]: {passiveSkill.Kiling,-5}".Point());
            content.AddParagraph($"[灵]: {passiveSkill.Nimble,-5} [御]: {passiveSkill.Defend,-5}".Point());
        }
    }
}