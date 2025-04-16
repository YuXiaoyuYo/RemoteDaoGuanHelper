using RemoteDaoGuanHelper.Forms;
using System.Runtime.InteropServices;

namespace RemoteDaoGuanHelper.Services;

public static class WindowService
{
    public static void ApplySecuritySettings(SecureForm secureForm)
    {
        SetWindowDisplayAffinity(secureForm.Handle, WdaExcludeFromCapture);

        // 设置窗口扩展样式
        var currentExStyle = GetWindowLong(secureForm.Handle, GwlExStyle);
        SetWindowLong(secureForm.Handle, GwlExStyle, currentExStyle | WsExLayered | WsExTransparent | WsExTopmost);
    }

    [DllImport("user32.dll")]
    private static extern bool SetWindowDisplayAffinity(IntPtr hWnd, uint dwAffinity);

    // 常量定义
    private const uint WdaExcludeFromCapture = 0x00000011;

    private const int WsExTransparent = 0x00000020;
    private const int WsExLayered = 0x00080000;
    private const int WsExTopmost = 0x00000008;

    // 其他Windows API函数
    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    private const int GwlExStyle = -20;
}
