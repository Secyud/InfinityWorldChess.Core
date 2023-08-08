﻿#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf.BasicComponents;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.LayoutComponents;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#endregion

namespace InfinityWorldChess.InteractionDomain
{
    public class RoleInteractionComponent : MonoBehaviour
    {
        [SerializeField] private AvatarEditor LeftFull;
        [SerializeField] private AvatarEditor RightFull;
        [SerializeField] private RectTransform SayingContent;
        [SerializeField] private LayoutGroupTrigger SelectContent;
        [SerializeField] private RawImage BackGround;

        public void SetInteraction(IInteractionUnit unit)
        {
            if (unit is null)
            {
                U.Factory.Application.DependencyManager.DestroyScope<InteractionScope>();
                return;
            }

            unit.OnStart();

            LeftFull.OnInitialize(InteractionScope.Instance.LeftRole.Basic);

            RightFull.OnInitialize(InteractionScope.Instance.RightRole.Basic);

            SetSaying(unit.Text, unit.Background?.Value);

            if (unit.Selections is null || !unit.Selections.Any())
            {
                SetOption(
                    new Tuple<string, UnityAction>[]
                    {
                        new(
                            "...",
                            () =>
                            {
                                unit.OnEnd();
                                U.Factory.Application.DependencyManager.DestroyScope<InteractionScope>();
                            }
                        )
                    }
                );
            }
            else
            {
                SetOption(
                    unit.Selections?.Select(
                        u =>
                            new Tuple<string, UnityAction>(
                                u.Item1,
                                () =>
                                {
                                    unit.OnEnd();
                                    SetInteraction(u.Item2);
                                }
                            )
                    ).ToArray()
                );
            }
        }

        public void OnLeftViewClick()
        {
            GameScope.OpenGameMenu();
        }

        public void OnRightViewClick()
        {
            GameScope.OpenGameMenu();
        }

        private void SetSaying(string text, Sprite sprite = null)
        {
            for (int i = 0; i < SayingContent.childCount; i++)
                SayingContent.GetChild(i).Destroy();
            SText t = SayingContent.AddParagraph(text);
            RectTransform rect = t.GetComponent<RectTransform>();
            rect.anchorMax = new Vector2(1, 1);
            rect.anchorMin = new Vector2(0, 0);
            rect.anchoredPosition = new Vector2(0, 0);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1);
            t.alignment = TextAlignmentOptions.Center;
            t.enableAutoSizing = true;
            if (sprite)
            {
                Texture2D texture = sprite.texture;
                BackGround.texture = texture;
                float height = (float)texture.width / texture.height / 3;
                float y = (1 - height) / 2;
                BackGround.uvRect = new Rect(0, y, 1, height);
            }
            else
            {
                BackGround.texture = null;
            }
        }

        private void SetOption([NotNull] Tuple<string, UnityAction>[] options)
        {
            RectTransform content = SelectContent.PrepareLayout();

            for (int index = 0; index < options.Length; index++)
            {
                Tuple<string, UnityAction> option = options[index];
                SelectOptionCell cell = IwcAb.Instance.SelectOptionCell.Value.Instantiate(content);
                cell.OnInitialize(index, option.Item2, option.Item1);
            }
        }
    }
}