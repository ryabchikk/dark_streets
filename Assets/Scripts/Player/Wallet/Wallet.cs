using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public int money { get; private set; }
    public event UnityAction AmountChanged;

    public void AddMoney(int amount)
    {
        if (amount <= 0) {
            return;
        }

        money += amount;

        AmountChanged?.Invoke();
    }

    public bool TrySpendMoney(int amount)
    {
        if (amount <= 0) {
            return false;
        }

        bool isEnough = money >= amount;
        
        if (money >= amount) {
            money -= amount;
            AmountChanged?.Invoke();
        }

        return isEnough;
    }
}
