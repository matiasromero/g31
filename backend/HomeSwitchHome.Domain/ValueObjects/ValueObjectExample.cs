using System.Collections.Generic;
using HomeSwitchHome.Domain.Infrastructure;

namespace HomeSwitchHome.Domain.ValueObjects
{
    public class ValueObjectExample : ValueObject
    {
        public ValueObjectExample(int val1, string val2) : base()
        {
            Val1 = val1;
            Val2 = val2;
        }

        public int Val1 { get; private set; }
        public string Val2 { get; private set; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Val1;
            yield return Val2;
        }
    }
}