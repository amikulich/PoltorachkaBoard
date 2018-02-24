using System;

namespace Poltorachka.Domain
{
    public static class Assert
    {
        /// <summary>
        /// Asserts expression is true
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainAssertException"></exception>
        public static void That(bool condition, string message)
        {
            if (!condition)
            {
                throw new DomainAssertException(message);
            }
        }

        /// <summary>
        /// Throws exception if string is null or empty
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="argName"></param>
        public static void NotNullOrEmpty(string arg, string argName)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                throw new DomainAssertException($"{argName} cannot be empty");
            }
        }

        /// <summary>
        /// Throw exception if argument is null
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="argName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NotNull(object arg, string argName)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(argName);
            }
        }
    }
}
