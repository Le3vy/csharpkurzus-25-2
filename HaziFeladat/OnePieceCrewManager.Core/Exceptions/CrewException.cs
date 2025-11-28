namespace OnePieceCrewManager.Core.Exceptions
{
    public class CrewException : Exception
    {
        public CrewException(string message) : base(message) { }

        public CrewException(string message, Exception inner) : base(message, inner) { }
    }
}
