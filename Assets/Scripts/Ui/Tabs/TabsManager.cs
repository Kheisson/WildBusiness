using UnityEngine;

namespace Ui.Tabs
{
    public class TabManager : MonoBehaviour
    {
        public GameObject careerPanel;
        public GameObject skillsPanel;
        public GameObject assetsPanel;

        public void ShowCareerTab()
        {
            careerPanel.SetActive(true);
            skillsPanel.SetActive(false);
            assetsPanel.SetActive(false);
        }

        public void ShowSkillsTab()
        {
            careerPanel.SetActive(false);
            skillsPanel.SetActive(true);
            assetsPanel.SetActive(false);
        }

        public void ShowAssetsTab()
        {
            careerPanel.SetActive(false);
            skillsPanel.SetActive(false);
            assetsPanel.SetActive(true);
        }
    }
}