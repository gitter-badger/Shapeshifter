﻿using Shapeshifter.Core.Data;
using Shapeshifter.UserInterface.WindowsDesktop.Controls.Clipboard.ViewModels.Interfaces;

namespace Shapeshifter.UserInterface.WindowsDesktop.Controls.Clipboard.ViewModels
{
    abstract class ClipboardDataViewModel<TClipboardData> :
        IClipboardDataViewModel<TClipboardData>
        where TClipboardData : IClipboardData
    {
        protected ClipboardDataViewModel() { }

        public TClipboardData Data { get; set; }
    }
}