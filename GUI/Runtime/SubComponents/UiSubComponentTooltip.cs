using System;
using UnityEngine;
using UnityEngine.Localization;

namespace GPUI.SubComponents
{
    public class UiSubComponentTooltip : UiSubComponentBase
    {
        
        public LocalizedString tooltipText;

        enum Direction
        {
            Left,
            Right,
            Top,
            Bottom
        }
        
        Direction direction = Direction.Top;

        private void Start()
        {
            
            OnInitialize();
            
        }

        protected override void OnInitialize()
        {

            this.Element.onEnter.AddListener(delegate
            {
                
                UiTooltip tooltip = FindAnyObjectByType<UiTooltip>();

                if (tooltip == null)
                    return;
                
                if(direction == Direction.Top)
                    tooltip.rectTransform.position = new Vector2(this.Element.rectTransform.position.x - this.Element.rectTransform.rect.width / 2, this.Element.rectTransform.position.y + this.Element.rectTransform.rect.height / 2);
                
                tooltip.tooltipTextObject?.OverrideText(tooltipText?.GetLocalizedString());
                tooltip.FadeElement(true);
                
            }); 
            
            this.Element.onExit.AddListener(delegate
            {
                
                UiTooltip tooltip = FindAnyObjectByType<UiTooltip>();
                
                if (tooltip == null)
                    return;    
                
                tooltip.FadeElement(false);
                
            }); 

        }
        
    }
}