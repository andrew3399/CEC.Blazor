﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace CEC.Blazor.Components.UIControls
{
    public partial class UICardDataColumn : UITDColumn
    {
        public bool Show { get; set; } = true;

        protected override string _Css => this.Card.IsNavigation ? $"{base._Css}{this.OverflowCss} cursor-hand" : $"{base._Css}{this.OverflowCss}";

        protected string Style => this.IsMaxColumn ? $"width: {this.UIOptions.MaxColumnPercent}%" : string.Empty;

        protected bool IsMaxColumn => this.UIOptions != null && this.UIOptions.MaxColumn == this.Column ;

        protected string OverflowCss => this.IsMaxColumn ? " td-max td-overflow" : " td-normal";

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            this._Tag = "td";
            if (this.Show)
            {
                int i = 0;
                builder.OpenElement(i, this.Tag);
                builder.AddAttribute(i++, "class", this._Css);
                builder.AddAttribute(i++, "scope", "col");
                if (!string.IsNullOrEmpty(this.Style)) builder.AddAttribute(i++, "style", this.Style);
                if (!string.IsNullOrEmpty(this.ComponentId)) builder.AddAttribute(i++, "id", this.ComponentId);
                builder.AddAttribute(i++, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, (e => this.Card.NavigateToView(this.RecordID))));
                if (this.IsMaxColumn)
                {
                    builder.OpenElement(i, "div");
                    builder.AddAttribute(i++, "class", "overflow");
                    builder.OpenElement(i, "div");
                    builder.AddAttribute(i++, "class", "overflow-inner");
                    if (this.ChildContent != null) builder.AddContent(i++, this.ChildContent);
                    builder.CloseElement();
                    builder.CloseElement();
                }
                else
                {
                    if (this.ChildContent != null) builder.AddContent(i++, this.ChildContent);
                }
                builder.CloseElement();
            }
        }
    }
}
