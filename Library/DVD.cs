using System;

namespace Library
{
    public class DVD : LibraryItem
    {
        private string _director;
        private TimeSpan _duration;

        public string Director
        {
            get => _director;
            set => _director = value;
        }

        public TimeSpan Duration
        {
            get => _duration;
            set => _duration = value;
        }

        public override string GetDetails()
        {
            return $"{base.GetDetails()}, Director: {_director}, Duration: {_duration}";
        }
    }
}