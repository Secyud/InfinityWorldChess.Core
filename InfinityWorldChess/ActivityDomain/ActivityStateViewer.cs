using Secyud.Ugf.BasicComponents;
using UnityEngine;

namespace InfinityWorldChess.ActivityDomain
{
    public class ActivityStateViewer : MonoBehaviour
    {
        [SerializeField] private SImage Graph;
        [SerializeField] private Sprite Finished;
        [SerializeField] private Sprite Received;
        [SerializeField] private Sprite Failed;

        public void SetState(ActivityState state)
        {
            Graph.Sprite = state switch
            {
                ActivityState.NotReceived => null,
                ActivityState.Failed      => Failed,
                ActivityState.Received    => Finished,
                ActivityState.Finished    => Received,
                _                         => null
            };
        }
    }
}