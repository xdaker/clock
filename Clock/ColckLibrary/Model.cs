using System;

namespace NetworkLibrary
{
    class Model
    {
        public int Second => DateTime.Now.Second;
        public int Year => DateTime.Now.Year;
        public int Minute => DateTime.Now.Minute;
        public int Hour => DateTime.Now.Hour;
        public int Day => DateTime.Now.Day;
        public int Month=>DateTime.Now.Month;
        public int Millisecond => DateTime.Now.Millisecond;
        public Model()
        {
           
        }


    }
}
