using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Price
{

    
    public class StockPriceChangeEvent : EventArgs
    {
        public double OldPrice { get; }
        public double CurrentPrice { get; }
        public double Defrance { get; }

        public StockPriceChangeEvent(double Old, double New)
        {
            this.OldPrice = Old;
            this.CurrentPrice = New;
            this.Defrance = New - Old;
        }

    }

    public class StokePrice
    {
        public event EventHandler<StockPriceChangeEvent> StockPriceChanged;

        private double OldPrice;
        private double CurrentPrice;

        private void OnStokeChange(double OldPrice, double NewPrice)
        {
            OnStokeChange(new StockPriceChangeEvent(OldPrice, NewPrice));
        }

        protected virtual void OnStokeChange(StockPriceChangeEvent e)
        {
            StockPriceChanged?.Invoke(this, e);
        }

        public void SetPrice(double NewPrice)
        {
            if (NewPrice != CurrentPrice)
            {
                OldPrice = CurrentPrice;
                CurrentPrice = NewPrice;

                OnStokeChange(OldPrice, CurrentPrice);
            }


        }


    }

    public class Display
    {
        public void Subscribe(StokePrice price)
        {
            price.StockPriceChanged += PrintResult;
        }

        public void PrintResult( object Sender,StockPriceChangeEvent e)
        {
            Console.WriteLine("Price Change:\n");
            Console.WriteLine("Old Price :" + e.OldPrice);
            Console.WriteLine("New Price :" + e.CurrentPrice);
            Console.WriteLine("Def Between Prices :" + e.Defrance);

        }


    }


    internal class Program
    {
        static void Main(string[] args)
        {

            StokePrice stokePrice = new StokePrice();
            Display display = new Display();

            display.Subscribe(stokePrice);

            stokePrice.SetPrice(100);

            stokePrice.SetPrice(250);

            Console.Read();


        }
    }
}
