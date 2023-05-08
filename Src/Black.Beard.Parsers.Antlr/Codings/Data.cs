namespace Generate.ModelsScripts
{
    public class Data
    {
        public Data()
        {
            _datas = new Dictionary<string, object>();
        }

        public object GetData(string key)
        {

            if (_datas.TryGetValue(key, out var data))
                return data;

            return null;

        }

        public void SetData(string key, object value)
        {
            if (_datas.ContainsKey(key))
                _datas[key] = value;
            else
                _datas.Add(key, value);
        }

        public bool DataExist(string key)
        {
            return _datas.ContainsKey(key);
        }

        private Dictionary<string, object> _datas;

    }


}
