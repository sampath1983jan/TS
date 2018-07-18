using System;
using System.Collections.Generic;
using System.Data;

namespace TechSharpy.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //cFactoryA cFactorya = new cFactoryA();
            //cFactoryB cFactoryb = new cFactoryB();
            //Client c = new Client(cFactorya);
            //Console.WriteLine(c.abcProductB.GetType().ToString());
            //Console.WriteLine(c.abcProductB.getProductPrice ());

            //Client c1 = new Client(cFactoryb);
            //Console.WriteLine(c1.abcProductB.GetType().ToString());
            //Console.WriteLine(c1.abcProductA.getProductPrice());
            //FactorySQL factorySQL = new FactorySQL();
            //   SQL s = new SQL(factorySQL);

            //FactoryMySQL factorySQL1 = new FactoryMySQL();
            //SQL s1 = new SQL(factorySQL1);


            // Setup Chain of Responsibility

            string roman = "MCMXXVIII";
            Context context = new Context(roman);
            context.CopyTo();
            // Build the 'parse tree'

            List<Expression> tree = new List<Expression>();
            tree.Add(new ThousandExpression());
            tree.Add(new HundredExpression());
            tree.Add(new TenExpression());
            tree.Add(new OneExpression());

            // Interpret

            foreach (Expression exp in tree)
            {
                exp.Interpret(context);
            }

            Console.WriteLine("{0} = {1}",
              roman, context.Output);
            // Wait for user

            Console.ReadKey();

            Console.ReadLine();
        }


    }

    /// <summary>

    /// The 'Context' class

    /// </summary>
    [Serializable]
    class Context

    {
        private string _input;
        private int _output;

        // Constructor

        public Context(string input)
        {
            this._input = input;
           
        }

        public void CopyTo() {
            Context c = new Context("My copy function added");
            
           // c.CopyTo<Context>(this);

        }

        // Gets or sets input

        public string Input
        {
            get { return _input; }
            set { _input = value; }
        }

        // Gets or sets output

        public int Output
        {
            get { return _output; }
            set { _output = value; }
        }
    }

    /// <summary>

    /// The 'AbstractExpression' class

    /// </summary>

    abstract class Expression

    {
        public void Interpret(Context context)
        {
            if (context.Input.Length == 0)
                return;

            if (context.Input.StartsWith(Nine()))
            {
                context.Output += (9 * Multiplier());
                context.Input = context.Input.Substring(2);
            }
            else if (context.Input.StartsWith(Four()))
            {
                context.Output += (4 * Multiplier());
                context.Input = context.Input.Substring(2);
            }
            else if (context.Input.StartsWith(Five()))
            {
                context.Output += (5 * Multiplier());
                context.Input = context.Input.Substring(1);
            }

            while (context.Input.StartsWith(One()))
            {
                context.Output += (1 * Multiplier());
                context.Input = context.Input.Substring(1);
            }
        }

        public abstract string One();
        public abstract string Four();
        public abstract string Five();
        public abstract string Nine();
        public abstract int Multiplier();
    }

    /// <summary>

    /// A 'TerminalExpression' class

    /// <remarks>

    /// Thousand checks for the Roman Numeral M 

    /// </remarks>

    /// </summary>

    class ThousandExpression : Expression

    {
        public override string One() { return "M"; }
        public override string Four() { return " "; }
        public override string Five() { return " "; }
        public override string Nine() { return " "; }
        public override int Multiplier() { return 1000; }
    }

    /// <summary>

    /// A 'TerminalExpression' class

    /// <remarks>

    /// Hundred checks C, CD, D or CM

    /// </remarks>

    /// </summary>

    class HundredExpression : Expression

    {
        public override string One() { return "C"; }
        public override string Four() { return "CD"; }
        public override string Five() { return "D"; }
        public override string Nine() { return "CM"; }
        public override int Multiplier() { return 100; }
    }

    /// <summary>

    /// A 'TerminalExpression' class

    /// <remarks>

    /// Ten checks for X, XL, L and XC

    /// </remarks>

    /// </summary>

    class TenExpression : Expression

    {
        public override string One() { return "X"; }
        public override string Four() { return "XL"; }
        public override string Five() { return "L"; }
        public override string Nine() { return "XC"; }
        public override int Multiplier() { return 10; }
    }

    /// <summary>

    /// A 'TerminalExpression' class

    /// <remarks>

    /// One checks for I, II, III, IV, V, VI, VI, VII, VIII, IX

    /// </remarks>

    /// </summary>

    class OneExpression : Expression

    {
        public override string One() { return "I"; }
        public override string Four() { return "IV"; }
        public override string Five() { return "V"; }
        public override string Nine() { return "IX"; }
        public override int Multiplier() { return 1; }
    }

    //public class SQL {
    //   // public SQLCommend mysqlcm;
    //   public SQLCommend sqlcm1;
    //    public SQL(absSQLFactory af) {                      
    //        sqlcm1 = af.CreateSQLInstance();
    //        Console.WriteLine(sqlcm1.ExecuteCommend());
    //        Console.WriteLine(sqlcm1.GetData().Rows.Count);
    //    }
    //}

    //public abstract class SQLCommend {
    //    public abstract System.Data.DataTable GetData();
    //    public abstract int ExecuteCommend();
    //}

    //public class MySQL : SQLCommend
    //{
    //    public override int ExecuteCommend()
    //    {
    //        return 20;
    //    //    throw new NotImplementedException();
    //    }

    //    public override DataTable GetData()
    //    {
    //        return new DataTable();
    //      //  throw new NotImplementedException();
    //    }
    //}

    //public class SQLServer : SQLCommend
    //{
    //    public override int ExecuteCommend()
    //    {
    //        return 10;
    //       // throw new NotImplementedException();
    //    }

    //    public override DataTable GetData()
    //    {
    //        return new DataTable();
    //     //   throw new NotImplementedException();
    //    }
    //}

    //public abstract class absSQLFactory {
    //    public abstract SQLCommend CreateSQLInstance();

    //}

    //public class FactorySQL : absSQLFactory
    //{
    //    public override SQLCommend CreateSQLInstance()
    //    {
    //        //throw new NotImplementedException();
    //        return new SQLServer();
    //    }
    //}

    //public class FactoryMySQL : absSQLFactory
    //{
    //    public override SQLCommend CreateSQLInstance()
    //    {
    //        return new MySQL();
    //        //throw new NotImplementedException();
    //    }
    //}


    //public class Client {
    //    public  AbcProductA abcProductA;
    //   public AbcProductB abcProductB;


    //    public Client(AbsFactory absFactory)
    //    {
    //        abcProductA= absFactory.CreateAbcProductA();
    //        abcProductB = absFactory.CreateAbcProductB();

    //    }
    //}
    //// Abstract Class for product
    //public abstract class AbcProductA {
    //    public abstract int getProductPrice();
    //}
    //public abstract class AbcProductB {
    //    public abstract int getProductPrice();
    //}
    //// product class with inheritence
    //public class ProductA1 : AbcProductA
    //{
    //    public override int getProductPrice()
    //    {
    //        return 1 * 1;
    //     //   throw new NotImplementedException();
    //    }
    //}
    //public class ProductB1 : AbcProductB
    //{
    //    public override int getProductPrice()
    //    {
    //        return 1 * 2;
    //        //throw new NotImplementedException();
    //    }
    //}

    //public class ProductA2 : AbcProductA
    //{
    //    public override int getProductPrice()
    //    {
    //        return 1 * 3;
    //        //throw new NotImplementedException();
    //    }
    //}

    //public class ProductB2 : AbcProductB
    //{
    //    public override int getProductPrice()
    //    {
    //        return 1 * 4;
    //       // throw new NotImplementedException();
    //    }
    //}



    //public abstract class AbsFactory {
    //    public abstract AbcProductA CreateAbcProductA();
    //    public abstract AbcProductB CreateAbcProductB();
    //}


    //public class cFactoryA : AbsFactory
    //{
    //    public override AbcProductA CreateAbcProductA()
    //    {
    //        return new ProductA1(); 
    //    }

    //    public override AbcProductB CreateAbcProductB()
    //    {
    //        return new ProductB1(); 
    //        //throw new NotImplementedException();
    //    }
    //}
    //public class cFactoryB : AbsFactory
    //{
    //    public override AbcProductA CreateAbcProductA()
    //    {
    //        return new ProductA2();
    //   //     throw new NotImplementedException();
    //    }

    //    public override AbcProductB CreateAbcProductB()
    //    {
    //        return new ProductB2();
    //      //  throw new NotImplementedException();
    //    }
    //}

}
