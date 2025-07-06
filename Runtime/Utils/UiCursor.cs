using System;
using System.Collections.Generic;
using UnityEngine;

namespace GPUI
{
    public static class UiCursor
    {
        
        public static List<Sprite> cursorSprites => GameObject.FindAnyObjectByType<UiManager>()?.currentPalette.cursorSprites;

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
                    SetCursor(0);
                    break;
                case CursorType.Hand:
                    SetCursor(1);
                    break;
                
            }

        }

        static void SetCursor(int index)
        {
            
            if(index < 0 || index >= cursorSprites.Count)
                return;
            
            Cursor.SetCursor(cursorSprites[index].texture, Vector2.zero, CursorMode.Auto);
            
        }
        
    }
}