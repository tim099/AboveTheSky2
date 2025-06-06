using System;
using System.Linq;
using System.Text;

namespace HoneyFramework
{
    /*
     *  Helper class supporting time cycle within coroutine
     */
    class CoroutineHelper
    {
        static private DateTime start;

        /// <summary>
        /// Starts timer. It is important to call this function each time corotine enters new section (after: yield return null;)
        /// </summary>
        /// <returns></returns>
        static public void StartTimer()
        {
            start = DateTime.Now;
        }

        /// <summary>
        /// returns true if time from start have passed. Used to keep coroutines within specified time length
        /// </summary>
        /// <param name="miliseconds"></param>
        /// <returns></returns>
        static public bool CheckIfPassed(int miliseconds)
        {
            DateTime now = DateTime.Now;
            int ms = (now - start).Milliseconds;

            return ms >= miliseconds;
        }
    }
}
