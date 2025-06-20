using System;
using System.Runtime.InteropServices;

namespace GPUI
{
    public static class UiCursor
    {
        [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        static extern IntPtr SetCursor(IntPtr  hCursor);

        [DllImport("user32.dll")]
        static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll")]
        static extern IntPtr LoadImage(
            IntPtr hInstance, 
            string lpImageName,
            uint uType,
            int cxDesired,
            int cyDesired,
            uint fuLoad
        );

        public enum CursorType
        {
            Arrow,
            Hand
        }
        
        public static void ChangeCursor(CursorType cursorType)
        {

            switch (cursorType)
            {

                case CursorType.Arrow:
                    SetCursor(LoadCursor(IntPtr.Zero ,32512));
                    break;
                case CursorType.Hand:
                    SetCursor(LoadCursor(IntPtr.Zero ,32649));
                    break;
                
            }

        }
        
    }
}