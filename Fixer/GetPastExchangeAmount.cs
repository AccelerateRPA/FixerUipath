using System;
using System.Activities;
using System.ComponentModel;
using FixerSharp;

namespace FixerIO.Workflow.Activities
{
    public class GetPastExchangedAmount : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [Description("The amount you wish to convert into a different currency")]
        public InArgument<double> InputAmount { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("The original currency you wish to convert. Please enter as a string of the currency code, e.g. $=USD. A full list can be found here: https://fixer.io/symbols")]
        public InArgument<string> InputCurrency { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("The currency to be converted to. Please enter as a string of the currency code, e.g. $=USD. A full list can be found here: https://fixer.io/symbols")]
        public InArgument<string> OutputCurrency { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Your Fixer.IO Account APIKey.  This can be found in your Fixer.IO dashboard: https://fixer.io/dashboard")]
        public InArgument<string> APIKey { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("The date you wish to use currency conversion from.")]
        public InArgument<DateTime> PastDate { get; set; }

        [Category("Output")]
        [Description("The amount after conversion.")]
        public OutArgument<double> OutputAmount { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var aPIKey = APIKey.Get(context);
            Fixer.SetApiKey(aPIKey);
            var inputAmount = InputAmount.Get(context);
            var inputCurrency = InputCurrency.Get(context);
            var outputCurrency = OutputCurrency.Get(context);
            var pastDate = PastDate.Get(context);
            double outputAmount = Fixer.Convert(inputCurrency, outputCurrency, inputAmount, pastDate);
            OutputAmount.Set(context, outputAmount);
        }
    }
}