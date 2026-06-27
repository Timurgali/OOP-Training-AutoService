namespace AutoService
{
    public class Casse
    {
        public Casse(int money)
        {
            Money = money;
        }

        public float Money { get; private set; }

        public void ReceivePayment(float payment)
        {
            if (payment > 0)
                Money += payment;
        }

        public void Pay(float price)
        {
            if (Money >= price)
                Money -= price;
        }
    }
}
