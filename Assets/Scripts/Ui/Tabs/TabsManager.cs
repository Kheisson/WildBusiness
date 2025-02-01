using System.Collections.Generic;
using Infra;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Tabs
{
    public class TabsManager : MonoBehaviour
    {
        [Header("Tabs Configuration")]
        [SerializeField] private List<Button> tabButtons;      
        [SerializeField] private List<GameObject> tabPanels;  
        [SerializeField] private Color activeColor = Color.white;   
        [SerializeField] private Color inactiveColor = Color.gray;
        [SerializeField] private int defaultTabIndex = 0;        

        private void Start()
        {
            InitializeTabs();
            SetDefaultTab();
        }

        private void InitializeTabs()
        {
            if (tabButtons.Count != tabPanels.Count)
            {
                LlamaLog.LogError("TabManager error: The number of buttons and panels must match!");
                return;
            }

            for (var i = 0; i < tabButtons.Count; i++)
            {
                var index = i;  
                tabButtons[index].onClick.AddListener(() => OnTabSelected(index));
            }
        }

        private void SetDefaultTab()
        {
            if (defaultTabIndex < 0 || defaultTabIndex >= tabPanels.Count)
            {
                LlamaLog.LogError("TabManager error: Default tab index is out of range!");
                return;
            }

            OnTabSelected(defaultTabIndex);
        }

        private void OnTabSelected(int index)
        {
            if (index < 0 || index >= tabPanels.Count)
            {
                LlamaLog.LogError("TabManager error: Tab index is out of range!");
                return;
            }

            for (var i = 0; i < tabPanels.Count; i++)
            {
                var isActive = i == index;
                tabPanels[i].SetActive(isActive);

                var buttonColor = tabButtons[i].GetComponent<Image>();
                buttonColor.color = isActive ? activeColor : inactiveColor;
            }
        }
    }
}
