using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascertain.Athletics
{
    public abstract class Statistic : IList<StatisticField>, IComparable<Statistic>
    {
        public Statistic(Player player)
        {
            this.fields = new List<StatisticField>();
            this.Player = player;
            this.SetValue("Name", player.Name);
            this.DefaultSortFieldName = "Name";
        }

        #region Public Interface 

        public Player Player
        {
            get;
            set;
        }

        public string DefaultSortFieldName
        {
            get;
            set;
        }

        #endregion

        #region collection implementation

        public StatisticField GetValue(string name)
        {
            StatisticField fld = this.fields.Where(x => x.Name == name).FirstOrDefault();
            if (null == fld)
            {
                fld = new StatisticField(name);
                this.fields.Add(fld);
            }
            return fld;
        }

        public void SetValue(string name, string value)
        {
            StatisticField fld = this.GetValue(name);
            fld.Value = value;
        }

        #endregion

        #region IList implementation 

        internal List<StatisticField> fields;

        public int IndexOf(StatisticField item)
        {
            return fields.IndexOf(item);
        }

        public void Insert(int index, StatisticField item)
        {
            //exists? just update
            if (this.fields.Any(x => x.Name == item.Name))
                this.SetValue(item.Name, item.Value);
            this.fields.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.fields.RemoveAt(index);
        }

        public StatisticField this[int index]
        {
            get
            {
                return this.fields[index];
            }
            set
            {
                this.fields[index] = value;
            }
        }

        public void Add(StatisticField item)
        {
            this.SetValue(item.Name, item.Value);
        }

        public void Clear()
        {
            this.fields.Clear();
        }

        public bool Contains(StatisticField item)
        {
            return this.fields.Contains(item);
        }

        public void CopyTo(StatisticField[] array, int arrayIndex)
        {
            this.fields.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.fields.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(StatisticField item)
        {
            return this.fields.Remove(item);
        }

        public IEnumerator<StatisticField> GetEnumerator()
        {
            return this.fields.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
        
        #region Comparison 

        public static Comparison<Statistic> SortByField(string field)
        {
            return new Comparison<Statistic>((x, y) => x.GetValue(field).Value.CompareTo(y.GetValue(field).Value));
        }

        /// <summary>
        /// IComparable implementation
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Statistic other)
        {
            return this.GetValue(this.DefaultSortFieldName).Value.CompareTo(other.GetValue(other.DefaultSortFieldName).Value);
        }

        #endregion
    }
}
