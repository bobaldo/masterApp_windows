using System;
using System.Collections.Generic;
using System.Text;

namespace GameBlock
{
    public static class ManageImage
    {
        private static int oldValue;
        private static int countCapture;

        public static int OldValue
        {
            get { return oldValue; }
        }

        public static int CountCapture
        {
            get { return countCapture; }
        }

        public static void resetCountCapture()
        {
            oldValue = countCapture;
            countCapture = -1;
        }

        public static void roolbackCountCapture()
        {
            countCapture = oldValue;
        }

        public static void addCountCapture()
        {
            countCapture = countCapture + 1;
        }
    }
}