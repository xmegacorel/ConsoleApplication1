using System;
using System.Collections.Generic;

namespace CruiseControlHorse
{
    class AnneHorseSolver
    {
        private readonly double _overallDistanse;
        private readonly List<HorseInfo> _info;
        private double _epsilon = 0.0001;
        private double _maxSpeed = 10000000.0;
        private double _result;

        public AnneHorseSolver(double overallDistanse, List<HorseInfo> info)
        {
            _overallDistanse = overallDistanse;
            _info = info ?? throw new System.ArgumentNullException(nameof(info));

            if (info.Count == 1)
            {
                _info.Add(new HorseInfo()
                {
                    Distanse = overallDistanse,
                    Speed = _maxSpeed
                });
            }

            info.ForEach(x => x.DepartureHour = (overallDistanse - x.Distanse) / x.Speed);

            _result = Solve();
        }

        public double Result => _result;

        private double Solve()
        {
            _info.Sort((x, y) => x.Distanse.CompareTo(y.Distanse));

            int indexOne = 0, indexTwo = 1;
            double currentSpeed = 0;
            while(true)
            {
                var info1 = _info[indexOne];
                var info2 = _info[indexTwo];
                var t = CalcMeetingTime(info1, info2);
                if (t <= 0) // не догонит
                {
                    var overalTime = (_overallDistanse - info1.Distanse) / info1.Speed;
                    currentSpeed = _overallDistanse / overalTime;

                    indexTwo++;
                    if (indexTwo >= _info.Count)
                    {
                        break;
                    }
                    continue;
                }
                else
                {
                    //1) посчитать до встречи друг с другом
                    //2) с ограничением скорости
                    var distanseToFinish = _overallDistanse - info2.Distanse - info2.Speed * t;
                    var overalTime = distanseToFinish / info2.Speed + t;
                    currentSpeed = _overallDistanse / overalTime;
                }
                indexOne++;
                indexTwo++;
                if (indexTwo >= _info.Count)
                {
                    break;
                }
            }

            return currentSpeed;
        }

        private double CalcMeetingTime(HorseInfo horseInfo1, HorseInfo horseInfo2)
        {
            if (Math.Abs(horseInfo1.Speed - horseInfo2.Speed) < _epsilon)
            {
                return 0;
            }
                
            var result = (horseInfo2.Distanse - horseInfo1.Distanse) / (horseInfo1.Speed - horseInfo2.Speed);

            return result;
        }
    }
}
