using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeBusiness
{
    //Alcohol, Weapon, Casino, Tobacco, Tote
    Алкоголь, Оружие, Казино, Табак, Тотализатор
}
public enum SizeBusiness
{
    //Small, Medium, Big
    Малый, Средний, Большой
}
public class BusinessClass
{   
    public int price { get; private set; }
    public int pumpingPrice { get; private set; }    
    public int passiveIncome { get; private set; }
    public int priceForCellPass { get; private set; }
    public SizeBusiness size { get; private set; }
    public TypeBusiness type { get; private set; }
    public string name { get; private set; }
    public int lvl { get; private set; }

    public BusinessClass(int price, string name, SizeBusiness size, TypeBusiness type)
    {
        this.price = price;
        this.size = size;
        this.type = type;
        this.name = name;
        this.lvl = 0;

        SetPassiveIncome();
        SetPumpingPrice();
        SetPriceForCellPass();
    }

    public void SetPassiveIncome(){ passiveIncome = CalculatePassiveIncome(); }
    public void SetPumpingPrice() {  pumpingPrice = CalculatePumpingPrice();}
    public void SetPriceForCellPass() { priceForCellPass = CalculatePriceForCellPass(); }

    public void LvlUp()
    {
        if(lvl < 5) {
            lvl += 1;
        }
    }

    public void LvlDown()
    {
        if (lvl > 0 ) {
            lvl -= 1;
        }
    }
    
    private int CalculatePassiveIncome() => lvl != 0 ? (price / 10) + (100 * lvl) : 0;
    private int CalculatePumpingPrice() => (price / 10) * 2;
    private int CalculatePriceForCellPass() => passiveIncome / 2 + price / 2;
}
