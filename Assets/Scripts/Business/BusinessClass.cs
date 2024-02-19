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
    public SizeBusiness sizeBusiness { get; private set; }
    public TypeBusiness typeBusiness { get; private set; }
    public int businessLvl { get; private set; }

    public BusinessClass(int price, SizeBusiness sizeBusiness, TypeBusiness typeBusiness)
    {
        this.price = price;
        this.sizeBusiness = sizeBusiness;
        this.typeBusiness = typeBusiness;
        this.businessLvl = 0;

        SetPassiveIncome();
        SetPumpingPrice();
        SetPriceForCellPass();
    }

    public void SetPassiveIncome(){ passiveIncome = CalculatePassiveIncome(); }
    public void SetPumpingPrice() {  pumpingPrice = CalculatePumpingPrice();}
    public void SetPriceForCellPass() { priceForCellPass = CalculatePriceForCellPass(); }

    public void BusinessLvlUp()
    {
        if(businessLvl < 5) {
            businessLvl += 1;
        }
    }

    public void BusinessLvlDown()
    {
        if (businessLvl > 0 ) {
            businessLvl -= 1;
        }
    }
    
    private int CalculatePassiveIncome() => businessLvl != 0 ? (price / 10) + (100 * businessLvl) : 0;
    private int CalculatePumpingPrice() => (price / 10) * 2;
    private int CalculatePriceForCellPass() => passiveIncome / 2 + price / 2;
}
