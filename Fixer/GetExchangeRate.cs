using System.Activities;
using System.ComponentModel;
using FixerSharp;

namespace FixerIO.Workflow.Activities
{
    public class GetExchangeRate : CodeActivity
    {
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

        [Category("Output")]
        [Description("The conversion rate between the two specified currencies.")]
        public OutArgument<double> Rate { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var aPIKey = APIKey.Get(context);
            Fixer.SetApiKey(aPIKey);
            var inputCurrency = InputCurrency.Get(context);
            var outputCurrency = OutputCurrency.Get(context);
            var rate = Fixer.Rate(inputCurrency, outputCurrency);
            double dblrate = rate.Convert(1);
            Rate.Set(context, dblrate);
        }
    }
}