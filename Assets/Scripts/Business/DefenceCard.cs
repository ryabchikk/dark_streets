using TMPro;
using UnityEngine;
using static UIAnimations;
namespace Business
{
    public class DefenceCard: BusinessCardBase
    {
        [SerializeField] private TextMeshProUGUI knucklesText;
        [SerializeField] private TextMeshProUGUI handgunText;
        [SerializeField] private TextMeshProUGUI machinegunText;
        [SerializeField] private TextMeshProUGUI defendersCount;
        
        protected override void ActivateBusinessCardImpl()
        {
            UpdateStrings();
            AnimateDefenceCard();
        }

        public void Defend()
        {
            gameObject.SetActive(false);
        }

        public void AddFighter(int typeNum)
        {
            _currentBusiness.TryAddDefenders((FighterType)typeNum, 1);
            UpdateStrings();
        }
        
        public void RemoveFighter(int typeNum)
        {
            _currentBusiness.TryRemoveDefenders((FighterType)typeNum, 1);
            UpdateStrings();
        }

        private void UpdateStrings()
        {
            knucklesText.text = _currentBusiness.GetDefendersCount(FighterType.Knuckles).ToString();
            handgunText.text = _currentBusiness.GetDefendersCount(FighterType.Handgun).ToString();
            machinegunText.text = _currentBusiness.GetDefendersCount(FighterType.Machinegun).ToString();
            defendersCount.text = _currentBusiness.DefendersCount.ToString();
        }

        private void AnimateDefenceCard()
        {
            YUIMover(gameObject,  -Screen.height);
        }
    }
}