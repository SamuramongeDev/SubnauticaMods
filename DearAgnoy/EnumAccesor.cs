namespace BaseLib
{
    using System;
    using System.Collections.Generic;

    public class EnumAccesor<T> where T : Enum
    {
        public readonly Dictionary<int, string> values = new Dictionary<int, string>();

        public int startingIndex;
        public int currentIndex = 1;

        public T this[int index]
        {
            get
            {
                return GetValue(index);
            }
        }

        public T this[string name]
        {
            get
            {
                return GetValue(name);
            }
        }

        public T AddValue(string name)
        {
            int index = startingIndex + currentIndex;
            values.Add(index, name);
            currentIndex++;
            return GetValue(name);
        }

        public void AddValue(T value)
        {
            int index = startingIndex + currentIndex;
            values.Add(Convert.ToInt32(value), value.ToString());
            currentIndex++;
        }

        public void RemoveValue(string name)
        {
            InternalRemoveValue(name);
        }

        public void RemoveValue(T value)
        {
            InternalRemoveValue(value);
        }

        public T GetValue(string name)
        {
            return InternalGetValue(name);
        }

        public T GetValue(int index)
        {
            return InternalGetValue(index);
        }

        public T GetValue(T value)
        {
            return InternalGetValue(value);
        }

        private T InternalGetValue(int index)
        {
            foreach (var keyValuePair in values)
            {
                int KVPIndex = keyValuePair.Key;
                if (KVPIndex == index)
                {
                    return (T)Enum.ToObject(typeof(T), KVPIndex);
                }
            }
            return default;
        }

        private T InternalGetValue(string name)
        {
            foreach (var keyValuePair in values)
            {
                string currentName = keyValuePair.Value;
                int currentKVPIndex = keyValuePair.Key;

                if (currentName == name)
                {
                    return (T)Enum.ToObject(typeof(T), currentKVPIndex);
                }
            }
            return default;
        }

        private T InternalGetValue(T value)
        {
            string name = value.ToString();
            foreach (var keyValuePair in values)
            {
                string currentName = keyValuePair.Value;
                int currentKVPIndex = keyValuePair.Key;

                if (currentName == name)
                {
                    return (T)Enum.ToObject(typeof(T), currentKVPIndex);
                }
            }
            return default;
        }

        private void InternalRemoveValue(string name)
        {
            foreach (var keyValuePair in values)
            {
                if (keyValuePair.Value == name)
                {
                    values.Remove(keyValuePair.Key);
                    break;
                }
            }
        }

        private void InternalRemoveValue(T value)
        {
            string name = value.ToString();
            foreach (var keyValuePair in values)
            {
                if (keyValuePair.Value == name)
                {
                    values.Remove(keyValuePair.Key);
                    break;
                }
            }
        }

        public EnumAccesor()
        {
            int lastMax = 0;
            List<int> ints = new List<int>();

            foreach (T value in Enum.GetValues(typeof(T)))
            {
                values.Add(Convert.ToInt32(value), value.ToString());
                ints.Add(Convert.ToInt32(value));
            }

            foreach (int value in ints)
            {
                if (value > lastMax)
                {
                    lastMax = value;
                }
            }
            startingIndex = lastMax;
        }
    }
}
