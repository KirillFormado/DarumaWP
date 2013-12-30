namespace DarumaBLLPortable.Domain
{
    public enum DarumaStatus
    {
        Empty,
        MakedWish,
        ExecutedWish,
        /// <summary>
        ///   After year since makes a wish, Daruma will burned
        /// </summary>
        TimeExpired
    }
}
