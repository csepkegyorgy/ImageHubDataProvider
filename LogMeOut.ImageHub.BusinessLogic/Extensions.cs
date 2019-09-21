namespace LogMeOut.ImageHub.BusinessLogic
{
    using System;
    using System.Collections.Generic;

    public static class Extensions
    {
        public static T GetRandomItem<T>(this List<T> collection, Random random)
        {
            return collection[random.Next(0, collection.Count)];
        }
    }
}
