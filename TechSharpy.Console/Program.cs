using System;

namespace TechSharpy.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            cFactoryA cFactorya = new cFactoryA();
            cFactoryB cFactoryb = new cFactoryB();
            Client c = new Client(cFactorya);
            Console.WriteLine(c.abcProductB.GetType().ToString());
            Console.WriteLine(c.abcProductB.getProductPrice ());

            Client c1 = new Client(cFactoryb);
            Console.WriteLine(c1.abcProductB.GetType().ToString());
            Console.WriteLine(c1.abcProductA.getProductPrice());

            Console.ReadLine();
        }

        
    }

    public class Client {
        public  AbcProductA abcProductA;
       public AbcProductB abcProductB;
              

        public Client(AbsFactory absFactory)
        {
            abcProductA= absFactory.CreateAbcProductA();
            abcProductB = absFactory.CreateAbcProductB();

        }
    }
    // Abstract Class for product
    public abstract class AbcProductA {
        public abstract int getProductPrice();
    }
    public abstract class AbcProductB {
        public abstract int getProductPrice();
    }
    // product class with inheritence
    public class ProductA1 : AbcProductA
    {
        public override int getProductPrice()
        {
            return 1 * 1;
         //   throw new NotImplementedException();
        }
    }
    public class ProductB1 : AbcProductB
    {
        public override int getProductPrice()
        {
            return 1 * 2;
            //throw new NotImplementedException();
        }
    }
    public class ProductA2 : AbcProductA
    {
        public override int getProductPrice()
        {
            return 1 * 3;
            //throw new NotImplementedException();
        }
    }
    public class ProductB2 : AbcProductB
    {
        public override int getProductPrice()
        {
            return 1 * 4;
           // throw new NotImplementedException();
        }
    }


    public abstract class AbsFactory {
        public abstract AbcProductA CreateAbcProductA();
        public abstract AbcProductB CreateAbcProductB();
    }


    public class cFactoryA : AbsFactory
    {
        public override AbcProductA CreateAbcProductA()
        {
            return new ProductA1(); 
        }

        public override AbcProductB CreateAbcProductB()
        {
            return new ProductB1(); 
            //throw new NotImplementedException();
        }
    }
    public class cFactoryB : AbsFactory
    {
        public override AbcProductA CreateAbcProductA()
        {
            return new ProductA2();
       //     throw new NotImplementedException();
        }

        public override AbcProductB CreateAbcProductB()
        {
            return new ProductB2();
          //  throw new NotImplementedException();
        }
    }

}
