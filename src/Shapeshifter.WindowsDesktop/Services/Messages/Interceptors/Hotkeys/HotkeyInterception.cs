﻿namespace Shapeshifter.WindowsDesktop.Services.Messages.Interceptors.Hotkeys
{
    using System;
    using System.Threading;

    using Api;

    using Interfaces;

    class HotkeyInterception: IHotkeyInterception
    {
        static int interceptionId;

        public int InterceptionId { get; }

        public bool ControlNeeded { get; set; }

        public bool NoRepeat { get; set; }

        public int KeyCode { get; set; }

        public HotkeyInterception()
        {
            InterceptionId = Interlocked.Increment(ref interceptionId);
        }

        public void Start(IntPtr windowHandle)
        {
            UnregisterHotkey(windowHandle);

            var modifier = 0;
            if (ControlNeeded)
            {
                modifier |= KeyboardApi.MOD_CONTROL;
            }
            if (NoRepeat)
            {
                modifier |= KeyboardApi.MOD_NOREPEAT;
            }

            var registrationResult = KeyboardApi.RegisterHotKey(
                windowHandle,
                InterceptionId,
                modifier,
                KeyCode);
            if (!registrationResult)
            {
                throw new InvalidOperationException(
                    $"Couldn't install the hotkey interceptor for key {KeyCode}.");
            }
        }

        public void Stop(IntPtr windowHandle)
        {
            var registrationResult = UnregisterHotkey(windowHandle);
            if (!registrationResult)
            {
                throw new InvalidOperationException(
                    $"Couldn't uninstall the hotkey interceptor for key {KeyCode}.");
            }
        }

        bool UnregisterHotkey(IntPtr windowHandle)
        {
            return KeyboardApi.UnregisterHotKey(
                windowHandle,
                InterceptionId);
        }
    }
}