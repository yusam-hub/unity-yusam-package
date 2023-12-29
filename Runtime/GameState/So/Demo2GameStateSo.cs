using UnityEngine;

namespace YusamPackage.GameState.So
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game State/Demo/Demo 2")]
    public class Demo2GameStateSo : GameStateSo
    {
        public override void Update()
        {
            DoSomething();
        }

        private void DoSomething()
        {
        }
    }
}
