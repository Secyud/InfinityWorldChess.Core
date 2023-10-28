using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.LayoutComponents;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain
{
    public class BattleFloatingBroadcast : MonoBehaviour
    {
        [SerializeField] private Transform FloatingTemplate;
        [SerializeField] private LayoutGroupTrigger Trigger;
        [SerializeField] private float ShowTime = 2;

        private float _timeRecord;

        private LinkedList<Tuple<Transform, float>> _transforms;

        private void Awake()
        {
            _transforms = new LinkedList<Tuple<Transform, float>>();
        }

        private void Update()
        {
            _timeRecord += Time.deltaTime;

            while (_transforms.Any())
            {
                LinkedListNode<Tuple<Transform, float>> item = _transforms.First;
                if (item.Value.Item2 >= _timeRecord)
                {
                    Destroy(item.Value.Item1.gameObject);
                    _transforms.RemoveFirst();
                }
                else
                {
                    break;
                }
            }
        }

        public void AddBroadcast(IActiveSkill skill)
        {
            Transform f = Instantiate(FloatingTemplate, Trigger.PrepareLayout());
            f.AddTitle1(skill.Name);
            _transforms.AddLast(new Tuple<Transform, float>(f, _timeRecord + ShowTime));
        }
    }
}