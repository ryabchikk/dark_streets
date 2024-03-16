using TMPro;
using UnityEngine;

namespace Business
{
    public class DefenceCard: BusinessCardBase
    {
        [SerializeField] private TextMeshProUGUI knucklesText;
        [SerializeField] private TextMeshProUGUI handgunText;
        [SerializeField] private TextMeshProUGUI machinegunText;
        
        protected override void ActivateBusinessCardImpl()
        {
            knucklesText.text = _currentBusiness.GetDefendersCount(FighterType.Knuckles).ToString();
            handgunText.text = _currentBusiness.GetDefendersCount(FighterType.Handgun).ToString();
            machinegunText.text = _currentBusiness.GetDefendersCount(FighterType.Machinegun).ToString();
        }
    }
}