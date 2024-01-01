using System.Collections.Generic;

namespace YusamPackage
{
    public interface IGameInputScene
    {
        public void DoEnter();
        public void DoExit();
        public void DoUpdate();
        public List<string> AvailableLayerStringList();
        public void StoreLayerEditorChanged();
        public void DoEditorChangeLayerIndex(int index);
    }
}