
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using UnityEngine;
using UnityEngine.UI;

namespace ATS
{
    public class ATS_HPBar : MonoBehaviour
    {
        public Image m_HP;

        public void UpdateHP(int hp, int maxHP)
        {
            UpdateHP(hp/(float)maxHP);
        }
        public void UpdateHP(float percentage)
        {
            m_HP.fillAmount = percentage;
        }
    }
}
