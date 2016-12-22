using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascertain.Athletics
{
    public class StatisticField
    {
        public StatisticField() { }
        public StatisticField(string name) { this.Name = name; }
        public StatisticField(string name, string value) { this.Name = name; this.Value = value; }

        public string Name { get; set; }

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if(value == _value)
                    return;
                _value = value;
                if(string.IsNullOrEmpty(_value))
                    return;
                string lv = _value.ToLower();
                foreach (char c in alphacharacters) { if (lv.Contains(c)) { this.IsAlphaBased = true; break; } }
            }
        }
        private string _value;

        public bool IsAggregateValue { get { return !string.IsNullOrEmpty(this.Value) && !this.IsAlphaBased; } }

        public decimal DecimalValue { get { return string.IsNullOrEmpty(this.Value) || this.IsAlphaBased ? 0 : Convert.ToDecimal(this.Value); } }

        private bool IsAlphaBased { get; set; }

        const string alphacharacters = @"abcdefghijklmnopqrstuvwxyz`=~!@#$%^&*()_+;':"",/<>?";
    }
}
