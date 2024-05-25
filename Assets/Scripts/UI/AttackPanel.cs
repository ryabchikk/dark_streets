using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AttackPanel : MonoBehaviour
{
    [SerializeField] private GameObject payPanel;
    [SerializeField] private TextMeshProUGUI attackerKnucklesCountText;
    [SerializeField] private TextMeshProUGUI attackerHandgunCountText;
    [SerializeField] private TextMeshProUGUI attackerMachinegunCountText;
    [SerializeField] private TextMeshProUGUI defenderKnucklesCountText;
    [SerializeField] private TextMeshProUGUI defenderHandgunCountText;
    [SerializeField] private TextMeshProUGUI defenderMachinegunCountText;
    [SerializeField] private TextMeshProUGUI totalCount;
    [SerializeField] private TextMeshProUGUI attackerName;
    [SerializeField] private TextMeshProUGUI defenderName;

    private PlayerClass _attacker;
    private BusinessClass _business;
    private Action _callback;

    private Dictionary<FighterType, int> _fighters = new()
    {
        [FighterType.Knuckles] = 0,
        [FighterType.Handgun] = 0,
        [FighterType.Machinegun] = 0
    };

    public void Show(PlayerClass currentPlayer, BusinessClass business, Action callback)
    {
        _attacker = currentPlayer;
        _business = business;
        _callback = callback;
        
        defenderKnucklesCountText.text = business.GetDefendersCount(FighterType.Knuckles).ToString();
        defenderHandgunCountText.text = business.GetDefendersCount(FighterType.Handgun).ToString();
        defenderMachinegunCountText.text = business.GetDefendersCount(FighterType.Machinegun).ToString();
        attackerName.text = currentPlayer.Name;
        defenderName.text = business.Owner.Name;

        _fighters[FighterType.Knuckles] = 0;
        _fighters[FighterType.Handgun] = 0;
        _fighters[FighterType.Machinegun] = 0;

        UpdateStrings();
        gameObject.SetActive(true);
        payPanel.SetActive(false);
    }

    public void Increase(int fighterType)
    {
        if (_fighters.Values.Sum() >= 5)
        {
            return;
        }

        var type = (FighterType)fighterType;
        if (_attacker.GetFighterCount(type) < _fighters[type] + 1)
        {
            return;
        }

        _fighters[type]++;
        UpdateStrings();
    }

    public void Decrease(int fighterType)
    {
        var type = (FighterType)fighterType;
        if (_fighters[type] <= 0)
        {
            return;
        }

        _fighters[type]--;
        UpdateStrings();
    }

    public void Attack()
    {
        var captured = _business.Attack(_attacker, _fighters);
        gameObject.SetActive(false);
        if(!captured)
        {
            payPanel.SetActive(true);
        }
        else
        {
            _callback?.Invoke();
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
        payPanel.SetActive(true);
    }

    private void UpdateStrings()
    {
        attackerKnucklesCountText.text = _fighters[FighterType.Knuckles].ToString();
        attackerHandgunCountText.text = _fighters[FighterType.Handgun].ToString();
        attackerMachinegunCountText.text = _fighters[FighterType.Machinegun].ToString();
        totalCount.text = _fighters.Values.Sum().ToString();
    }
}
