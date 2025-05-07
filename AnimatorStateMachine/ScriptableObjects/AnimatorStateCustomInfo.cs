using UnityEngine;
using UTIRLib.Diagnostics;

namespace UTIRLib.AnimatorSystem
{
    [CreateAssetMenu(fileName = "AnimatorStateInfo", menuName = "Scriptable Objects/Animator State Info")]
    public class AnimatorStateCustomInfo : ScriptableObject
    {
        [Header("Name of state in Animator")]
        [SerializeField] private string stateName;
        [SerializeField] private string layerName;
        [SerializeField] private int layerIndex;

        public string StateName => stateName;
        public string LayerName => layerName;
        public int LayerIndex => layerIndex;
        public bool IsInitialzied => !string.IsNullOrEmpty(stateName);

        /// <remarks>Debug:
        /// <br/><see cref="AlreadyInitializedMessage"/>
        /// </remarks>
        public void Initialize(string stateName, string layerName, int layerIndex)
        {
            if (IsInitialzied) {
                Debug.LogError(new AlreadyInitializedMessage(this));
                return;
            }

            this.stateName = stateName;
            this.layerName = layerName;
            this.layerIndex = layerIndex;
        }
    }
}