using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class MouseClicker
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

    private const uint MOUSEEVENTF_RIGHTDOWN = 0x08; // Sağ tık basılı tutma
    private const uint MOUSEEVENTF_RIGHTUP = 0x10;   // Sağ tık bırakma

    public static void RightClickAtCursor()
    {
        // Mevcut fare konumunda sağ tıklama işlemi yapar
        mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0); // Sağ tık bas
        mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);   // Sağ tık bırak
    }
}
