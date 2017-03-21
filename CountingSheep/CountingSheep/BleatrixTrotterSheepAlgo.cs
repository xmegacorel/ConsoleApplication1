namespace CountingSheep
{
    //https://code.google.com/codejam/contest/6254486/dashboard
    public class BleatrixTrotterSheepAlgo
    {
        private const int Max = 1022;
        private int _current = 0;
        private int _next = 0;

        public BleatrixTrotterSheepAlgo(int n)
        {
            int limit = 0, j = 2;

            StoreDigits(n);
            
            while (limit++ < 100 && _current != Max)
            {
                _next = n * j;
                StoreDigits(_next);
                j++;
            }

            _next = -1; //INSOMNIA
        }

        private void StoreDigits(int next)
        {
            
            while (true)
            {
                if (next < 10)
                {
                    _current = _current | 2 << (next - 1);
                    return;
                }

                int m = (next / 10);
                var t = next - m * 10;
                _current = _current | 2 << (t - 1);

                next = m;
            }
            
        }

        public int Result => _next;
    }
}
