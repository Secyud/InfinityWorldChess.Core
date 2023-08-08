#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.TableComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.SkillDomain
{
    public abstract class SkillCell<TSkillCell, TSkillView> : TableCell
        where TSkillView : SkillView<TSkillCell, TSkillView>
        where TSkillCell : SkillCell<TSkillCell, TSkillView>
    {
        [SerializeField] private ShownCell Cell;
        [SerializeField] private SButton Install;
        [SerializeField] private SButton Remove;
        [SerializeField] private TSkillView View;

        public TSkillView SkillView => View;
        
        protected virtual void Awake()
        {
            View.SetCell(this);
        }

        public void SetInstallable(bool value)
        {
            Install.gameObject.SetActive(value);
            Remove.gameObject.SetActive(value);
        }

        public abstract void OnInstall();

        public abstract void OnRemove();

        public void Bind(ISkill skill)
        {
            Cell.BindShowable(skill);
        }
    }
}