namespace Terraria
{
    using System;
    using System;

    /// <summary>
    /// Defines the <see cref="Program" />
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The Main
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/></param>
        private static void Main(string[] args)
        {
            using (Terraria.Main main = new Terraria.Main())
            {
                main.Run();
            }
        }
    }
}
