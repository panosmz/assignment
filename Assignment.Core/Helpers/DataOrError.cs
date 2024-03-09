namespace Assignment.Core.Helpers
{
    public class DataOrError<TData>
    {
        private readonly TData? _data;
        private readonly string _error;

        public DataOrError(TData data)
        {
            _data = data;
            _error = string.Empty;
        }

        public DataOrError(string error)
        {
            _data = default(TData);
            _error = error;
        }

        public bool IsData => _error == string.Empty;

        public bool IsError => _error != string.Empty;

        public TData Data
        {
            get
            {
                if (IsData)
                    return _data!;
                throw new InvalidOperationException("No data available, check IsData before accessing Data.");
            }
        }

        public string Error
        {
            get
            {
                if (IsError)
                    return _error;
                throw new InvalidOperationException("No error available, check IsError before accessing Error.");
            }
        }
    }
}
