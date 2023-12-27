using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YusamPackage.GameDebug
{
    public class GameDebugGridUi : MonoBehaviour
    {
        private static GameDebugGridUi Instance { get; set; }
        
        [SerializeField] private GameDebugCellTemplateSo gameDebugCellTemplateSo;
        [SerializeField] private int rows = 1;
        
        private GridLayoutGroup _gridLayoutGroup;
        private List<GameDebugCellTemplate> _gameDebugCellTemplates; 
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            
            _gameDebugCellTemplates = new List<GameDebugCellTemplate>();
            
            int c = _gridLayoutGroup.constraintCount * rows;
            for (int i = 0; i < c; i++)
            {
                GameDebugCellTemplate gameDebugCellTemplate = Instantiate(gameDebugCellTemplateSo.gameDebugCellTemplate, transform);
                gameDebugCellTemplate.SetCellText("");
                _gameDebugCellTemplates.Add(gameDebugCellTemplate);
            }
        }

        private void OnDestroy()
        {
            int c = _gameDebugCellTemplates.Count;
            for (int i = c-1; i >= 0; i--)
            {
                GameDebugCellTemplate gameDebugCellTemplate = _gameDebugCellTemplates[i];
                _gameDebugCellTemplates.RemoveAt(i);
                Destroy(gameDebugCellTemplate.gameObject);
            }
        }

        public int GetCountCells()
        {
            return _gameDebugCellTemplates.Count;
        }

        private GameDebugCellTemplate GetGameDebugCellTemplate(int index)
        {
            return _gameDebugCellTemplates[index];
        }

        public void SetCellText(int index, string text)
        {
            GetGameDebugCellTemplate(index).SetCellText(text);
        }
 
        public Image GetBackgroundImage(int index)
        {
            return GetGameDebugCellTemplate(index).GetBackgroundImage();
        }
        
        public TextMeshProUGUI GetCellText(int index)
        {
            return GetGameDebugCellTemplate(index).GetCellText();
        }
    }
}
