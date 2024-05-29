using System;
using System.Collections;

namespace laba3
{
    abstract class BaseList<T> : IEnumerable<T> where T : IComparable<T>
    {
        protected int quantity;
        public int Count { get { return quantity; } }

        // объявление делегата
        public delegate void listener();
        // объявление события
        public event listener active;
        // вызов события
        protected void check()
        {
            // ? - оператор проверки на null, ситуация возможна, если нет подписчиков события
            active?.Invoke();
        }
        public abstract void Add(T item);
        public abstract void Insert(T item, int index);
        public abstract void Delete(int index);
        public abstract void Clear();
        public abstract void Print();
        public abstract T this[int i] { set; get; }


        public void Assign(BaseList<T> source)
        {
            Clear();
            for (int i = 0; i < source.Count; i++)
            {
                Add(source[i]);
            }
        }

        public void AssignTo(BaseList<T> dest)
        {
            dest.Assign(this);
        }

        protected abstract BaseList<T> EmptyClone();
        public BaseList<T> Clone()
        {
            BaseList<T> clone = EmptyClone();
            clone.Assign(this);
            return clone;
        }
        public virtual void Sort()
        {
            int left = 0;
            int right = quantity - 1;

            while (left < right)
            {
                bool swap = false;
                for (int i = left; i < right; i++)
                {
                    if (this[i].CompareTo(this[i + 1]) > 0)
                    {
                        (this[i], this[i + 1]) = (this[i + 1], this[i]);
                        swap = true;
                    }
                }

                if (!swap)
                {
                    break;
                }

                right--;


                for (int i = right; i > left; i--)
                {
                    if (this[i - 1].CompareTo(this[i]) > 0)
                    {
                        (this[i - 1], this[i]) = (this[i], this[i - 1]);
                    }
                }
                left++;
            }
        }

        public virtual void DeleteRepeat()
        {
            if (quantity == 0)
            {
                return;
            }

            for (int i = 0; i < quantity; i++)
            {
                bool check = true;
                for (int j = i + 1; j < quantity; j++)
                {
                    if (this[i].CompareTo(this[j]) == 0)
                    {
                        Delete(j);
                        check = false;
                        j--;
                    }
                }
                if (!check)
                {
                    Delete(i);
                    i--;
                }
            }
        }

        public bool IsEqual(BaseList<T> list)
        {
            if (quantity != list.Count)
            {
                return false;
            }

            for (int i = 0; i < quantity; i++)
            {
                if (this[i].CompareTo(list[i]) != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public abstract string ToString();
        public void SaveToFile(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(this.ToString());
            }
        }
        public void loadToFile(string path)
        {
            this.Clear();
            using (StreamReader reader = new StreamReader(path))
            {
                try
                {
                    string str = reader.ReadToEnd();

                    string[] list = str.Split("\n");
                    for (int i = 0; i < list.Length; i++)
                    {
                        string num = list[i].Trim();
                        if (!string.IsNullOrEmpty(num))
                        {
                            T conventNum = (T)Convert.ChangeType(num, typeof(T));
                            this.Add(conventNum);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exceptions.BadFileException("Некорректный формат данных в файле");
                }
            }
        }

        public static BaseList<T> operator +(BaseList<T> list_1, BaseList<T> list_2)
        {
            BaseList<T> merged = list_1.Clone();
            for (int i = 0; i < list_2.Count; i++)
            {
                merged.Add(list_2[i]);
            }
            return merged;
        }
        public static bool operator ==(BaseList<T> list_1, BaseList<T> list_2)
        {
            if (list_1.IsEqual(list_2)) return true;
            else return false;
        }
        public static bool operator !=(BaseList<T> list_1, BaseList<T> list_2)
        {
            if (list_1.IsEqual(list_2)) return false;
            else return true;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new EnumList(this);
        }

        protected class EnumList : IEnumerator<T>
        {
            private BaseList<T> list;
            private int index;

            public EnumList(BaseList<T> list)
            {
                this.list = list;
                this.index = -1;
            }

            object IEnumerator.Current { get { return Current; } }
            public bool MoveNext()
            {
                if (index < list.quantity - 1)
                {
                    index++;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Reset() { index = -1; }

            public void Dispose() { }

            public T Current
            {
                get { return list[index]; }
            }
        }
    }
}