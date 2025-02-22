using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{

    interface ITrade
    {
        double Value { get; }
        string ClientSector { get; }
        DateTime NextPaymentDate { get; }

        public bool IsPoliticallyExposed { get; }
        /* Descrevendo em 1 paragrafo o que precisei fazer para adaptar a nova categoria:
         * 
         * Criei a propriedade bool IsPoliticallyExposed na interface ITrade,na assinatura do Trade, na validação no TradeCategory          * 
         * Assumi que para a nova categoria, o texto também viria com true/false após a NextPaymentDate
         * assim verifico se é PEP antes das outras validações, aceitando 2 formatos de textos colado no console
         * 
         * COM PEP
         * 12/11/2020
           4
           2000000 Private 12/29/2025 true 
           400000 Public 07/01/2020 false
           5000000 Public 01/02/2024 false
           3000000 Public 10/26/2023 false

           SEM PEP 
           12/11/2020
           4
           2000000 Private 12/29/2025
           400000 Public 07/01/2020
           5000000 Public 01/02/2024
           3000000 Public 10/26/2023
         */
    }

    class Trade : ITrade
    {
        public double Value { get; }
        public string ClientSector { get; }
        public DateTime NextPaymentDate { get; }
        public bool IsPoliticallyExposed { get; }

        public Trade(double value, string clientSector, DateTime nextPaymentDate, bool isPoliticalExposed)
        {
            Value = value;
            ClientSector = clientSector;
            NextPaymentDate = nextPaymentDate;
            IsPoliticallyExposed = isPoliticalExposed;
        }
    }

    static string TradeCategory(Trade trade, DateTime referenceDate)
    {
        #region Infos
        /*
         * 
         *  [EXPIRED]
            Trades whose next payment date is late by more than 30 days based on a reference date which will be given.
            qtDayLate > 30 -> EXPIRED: 

            [HIGHRISK]: Trades with value greater than 1.000,000 and client from Private Sector.
            ClientSector == 'Private Sector' && TradeValue > 1.000,000 ==> HIGHRISK

            [MEDIUMRISK]: Trades with value greater than 1.000,000 and client from Public Sector.
            ClientSector == 'Private Sector' && TradeValue > 1.000,000 ==> HIGHRISK

        */
        #endregion

        string TradeRisk = "";

        if (trade.IsPoliticallyExposed)
            return "PEP";

        if ((referenceDate - trade.NextPaymentDate).Days > 30)
            return "EXPIRED";

        if (trade.Value > 1000000)
        {
            if (trade.ClientSector == "Private")
                TradeRisk = "HIGHRISK";

            if (trade.ClientSector == "Public")
                TradeRisk = "MEDIUMRISK";
        }

        return TradeRisk;

    }
    static void Main()
    {

        #region Core
        Console.WriteLine("Cole o texto no console e pressione Enter");

        DateTime referenceDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
        int n = int.Parse(Console.ReadLine());
        List<ITrade> trades = new List<ITrade>();

        for (int i = 0; i < n; i++)
        {
            string[] input = Console.ReadLine().Split(' ');
            double value = double.Parse(input[0]);
            string sector = input[1];
            DateTime paymentDate = DateTime.ParseExact(input[2], "MM/dd/yyyy", CultureInfo.InvariantCulture);
            bool isPoliticalExposed = false;
            if (input.Length.Equals(4))
                isPoliticalExposed = Convert.ToBoolean(input[3]);

            trades.Add(new Trade(value, sector, paymentDate, isPoliticalExposed));
        }

        string result = "";
        Console.Clear();
        foreach (Trade item in trades)
        {
            result = TradeCategory(item, referenceDate);
            Console.WriteLine(result);
        }
        Console.ReadKey();
        #endregion
    }


}
