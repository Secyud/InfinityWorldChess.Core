#region

using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.TableComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.SkillDomain
{
    public abstract class SkillCell<TSkillCell, TSkillView> : TableCell
        where TSkillView : SkillViewBase<TSkillCell, TSkillView>
        where TSkillCell : SkillCell<TSkillCell, TSkillView>
    {
        [SerializeField] private ShownCell Cell;
        [SerializeField] private SButton Install;
        [SerializeField] private SButton Remove;
        [SerializeField] private TSkillView View;

        protected ISkill BindSkill;
        public TSkillView SkillView => View;
        
        protected virtual void Awake()
        {
            if (CellIndex == -1)
                CellIndex = transform.GetSiblingIndex();
            View.BindCell((TSkillCell)this);
        }

        public void SetInstallable(bool value)
        {
            Install.gameObject.SetActive(value);
            Remove.gameObject.SetActive(value);
        }

        public abstract void OnInstall();

        public abstract void OnRemove();

        public virtual void OnSelect()
        {
            if (BindSkill is not null)
            {
                GameScope.Instance.OpenPointPanel(BindSkill);
            }
        }

        public void Bind(ISkill skill)
        {
            BindSkill = skill;
            Cell.BindShowable(skill);
        }
    }
}